using MinesweeperApp.Services;
using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class LogoutView : ContentPage
{
	public LogoutView(Service service_)
	{
		this.BindingContext = new LogoutViewModel(service_);
		InitializeComponent();
	}
}