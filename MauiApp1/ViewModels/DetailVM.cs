using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp1.Models;

namespace MauiApp1.ViewModels
{
    [QueryProperty("Pokemon","Pokemon")]
    public partial class DetailVM:BaseViewModel
    {
        [ObservableProperty]
        PokemonDTO pokemon;
    }
}
