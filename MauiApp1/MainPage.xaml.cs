using MauiApp1.ViewModels;
namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainVM vm)
        {
            InitializeComponent();
            BindingContext = vm;
            vm.Title = "THE POKEMON APP";
        }

    }
}
