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
            if(stopPokemon>1349) { stopPokemon=1349; } //1349 is the last pokemon ID
            if (startPokemon > stopPokemon || startPokemon==stopPokemon) 
                { stopPokemon = startPokemon; } //gives only the pokemon with start-ID
            pokemons = await test.GetAllPokemon(startPokemon, stopPokemon); //set low for testing-purposes
        }
    }
}
