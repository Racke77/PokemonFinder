using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels
{
    public partial class MainVM : BaseViewModel
    {
        private readonly PokemonService pokemonService;

        [ObservableProperty]
        ObservableCollection<PokemonDTO> pokemons = new ObservableCollection<PokemonDTO>();

        [ObservableProperty]
        private int startPokemon;
        [ObservableProperty]
        private int stopPokemon;

        public MainVM(PokemonService pokemonService)
        {
            this.pokemonService = pokemonService;
        }


        [RelayCommand]
        async void Get()
        {
            Pokemons.Clear();            
            await this.pokemonService.GetPokemon(StartPokemon, StopPokemon);
            foreach (var pokemon in this.pokemonService.pokemons)
            {
                Pokemons.Add(pokemon);
            }
        }

        [RelayCommand]
        async Task GoToDetail(PokemonDTO pokemon)
        {
            if (pokemon == null) { return; }
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}",
                true,
                new Dictionary<string, object>
                {
                    { "Pokemon" ,pokemon}
                });
        }
    }
}
