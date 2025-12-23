using MauiApp1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.API
{
    public class APISpeaker
    {
        public async Task<List<PokemonDTO>> GetAllPokemon(int firstPokemon, int lastPokemon)
        {
            List<PokemonDTO> pokemons = new List<PokemonDTO>();

            for (int i = firstPokemon; i < lastPokemon +1;)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                for (int j = 0; j < 20; j++) //only call 20 pokemon at a time -> don't DDOS the API
                {
                    var fullString = await client.GetStringAsync($"pokemon/{i}/"); //get everything for single pokemon
                    var pokedexString = await client.GetStringAsync($"pokemon-species/{i}/");

                    if (fullString is null || pokedexString is null) { return pokemons; }

                    var pokemon = new PokemonDTO();

                    //POKEMON NAME
                    var name = JObject.Parse(fullString)["name"].ToString();
                    pokemon.Name = ApiCorrections.CapitalizeFirstLetter(name);
                    //POKEMON ID
                    pokemon.Id = Int32.Parse(JObject.Parse(fullString)["id"].ToString());

                    pokemon = GetPokemonTypes(fullString, pokemon); //GETTING TYPES
                    pokemon = GetPokemonStats(fullString, pokemon); //GETTING THE STATS
                    pokemon = GetPokemonAbilities(fullString, pokemon); //GETTING ABILITIES
                    pokemon = GetPokemonPicture(fullString, pokemon); //GETTING PICTURE
                    pokemon = GetPokedexDescriptions(pokedexString, pokemon); //GETTING DESCRIPTIONS

                    pokemons.Add(pokemon);

                    i++;
                    if (i == lastPokemon)
                    {
                        j = 20; //if we hit the "full limit" mid-operation, abort operation
                    }
                }
                client.Dispose();//CLOSE API -> then reopen it for another 20 pokemon
            }
            return pokemons;
        }
        private PokemonDTO GetPokemonTypes(string fullString, PokemonDTO pokemon)
        {
            var types = JObject.Parse(fullString)["types"];
            var forCount = types.ToString();
            var split = forCount.Split("name");
            var fusedTyping = new List<string>();
            for (int p = 0; p < split.Length - 1; p++)
            {
                var slots = types[p];
                var type = slots["type"];
                var typeName = type["name"].ToString();
                fusedTyping.Add(ApiCorrections.CapitalizeFirstLetter(typeName));
            }
            pokemon.Typing = ApiCorrections.FuseTypesIntoString(fusedTyping);
            return pokemon;
        }
        private PokemonDTO GetPokemonStats(string fullString, PokemonDTO pokemon)
        {
            var stats = JObject.Parse(fullString)["stats"];
            for (int p = 0; p < 6; p++)
            {
                var baseStat = stats[p];
                var intNr = baseStat["base_stat"];
                switch (p)
                {
                    case 0:
                        { pokemon.HP = Int32.Parse(intNr.ToString()); }
                        break;
                    case 1:
                        { pokemon.ATK = Int32.Parse(intNr.ToString()); }
                        break;
                    case 2:
                        { pokemon.DEF = Int32.Parse(intNr.ToString()); }
                        break;
                    case 3:
                        { pokemon.SpATK = Int32.Parse(intNr.ToString()); }
                        break;
                    case 4:
                        { pokemon.SpDEF = Int32.Parse(intNr.ToString()); }
                        break;
                    case 5:
                        { pokemon.Speed = Int32.Parse(intNr.ToString()); }
                        break;
                }
            }
            return pokemon;
        }
        private PokemonDTO GetPokemonAbilities(string fullString, PokemonDTO pokemon)
        {
            var abilities = JObject.Parse(fullString)["abilities"];
            var forCount = abilities.ToString();
            var split = forCount.Split("name"); //making sure we don't miss any
            for (int p = 0; p < split.Length - 1; p++)
            {
                var slots = abilities[p];
                var ability = slots["ability"];
                var abilityName = ability["name"].ToString();
                pokemon.Abilities.Add(ApiCorrections.CapitalizeFirstLetter(abilityName));
            }
            return pokemon;
        }
        private PokemonDTO GetPokemonPicture(string fullString, PokemonDTO pokemon)
        {
            var allSprites = JObject.Parse(fullString)["sprites"]; //going deep into the the J-Token
            var otherSprites = allSprites["other"];
            var official = otherSprites["official-artwork"];
            pokemon.Image = official["front_default"].ToString();
            return pokemon;
        }

        private PokemonDTO GetPokedexDescriptions(string pokedexString, PokemonDTO pokemon)
        {
            var gen = JObject.Parse(pokedexString)["generation"];
            pokemon.Generation = gen["name"].ToString(); //wrong capitalization -> fix maybe

            var dex = JObject.Parse(pokedexString)["flavor_text_entries"];            
            var forCount = dex.ToString();
            var split = forCount.Split("flavor_text"); //making sure we don't miss any
            for (int p = 0; p < split.Length - 1; p++)
            {                
                var entry = dex[p];
                var languageEntry = entry["language"];
                var language = languageEntry["name"].ToString();
                if(language == "en") //only include english entries
                {
                    var dexEntry = new PokedexEntry();

                    var gameEntry = entry["version"]; //get the Game
                    var gameName = gameEntry["name"].ToString();
                    dexEntry.Game=ApiCorrections.CapitalizeFirstLetter(gameName);

                    var flavorText = entry["flavor_text"].ToString(); //get the Text
                    dexEntry.Description = ApiCorrections.RemoveRowChange(flavorText);
                    pokemon.Descriptions.Add(dexEntry);
                }
            }
            return pokemon;
        }
    }
}
