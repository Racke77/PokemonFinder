using MauiApp1.API;
using MauiApp1.Models;

namespace MauiApp1.Services
{
    public class PokemonService
    {
        APISpeaker test = new APISpeaker();
        public List<PokemonDTO> pokemons = new List<PokemonDTO>();
        public PokemonService()
        {
        }
        public async Task GetPokemon(int startPokemon, int stopPokemon)
        {
            if (startPokemon == 0) { startPokemon++; } //pokemon ID starts at "1"
            if (startPokemon > 1025) {  startPokemon=1025; } //don't let people ask for things that don't exist
            if(stopPokemon>1025) { stopPokemon=1025; } //1349 is the last pokemon ID, but API starts counting in 10,000 from here
            if (startPokemon > stopPokemon || stopPokemon==0) 
                { stopPokemon = startPokemon; } //gives only the pokemon with start-ID
            pokemons = await test.GetAllPokemon(startPokemon, stopPokemon); //set low for testing-purposes
        }
    }
}
