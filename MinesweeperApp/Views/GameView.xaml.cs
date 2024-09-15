using MinesweeperApp.ViewModels;
using DataGrid2DLibrary;

namespace MinesweeperApp.Views;

public partial class GameView : ContentPage
{
	public GameView(GameViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
		ArranageGrid(vm);
	}
	public void ArranageGrid(GameViewModel vm)
	{
		for(int i=0; i < vm.Board.GetLength(0); i++)
		{
			GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
		}
		for (int i = 0; i < vm.Board.GetLength(1); i++)
		{
			GameGrid.RowDefinitions.Add(new RowDefinition());
		}

	}
}