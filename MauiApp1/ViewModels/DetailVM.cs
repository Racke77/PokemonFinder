using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Models;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels
{
    [QueryProperty("Pokemon","Pokemon")]
    public partial class DetailVM:BaseViewModel
    {
        [ObservableProperty]
        PokemonDTO pokemon;

    }
}
