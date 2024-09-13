using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class GameView : ContentPage
{
	public GameView(GameViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}