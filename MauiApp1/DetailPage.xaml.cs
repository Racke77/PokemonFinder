using MauiApp1.ViewModels;

namespace MauiApp1;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailVM detailVM)
	{
		InitializeComponent();
		BindingContext = detailVM;
	}
}