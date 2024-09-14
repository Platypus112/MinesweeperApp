using MinesweeperApp.ViewModels;
using DataGrid2DLibrary;

namespace MinesweeperApp.Views;

public partial class GameView : ContentPage
{
	public GameView(GameViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();		
	}
}