using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class GameView : ContentPage
{
	public GameView(GameViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
		ArranageGrid();
	}
	public void ArranageGrid()
	{
		for(int i=0; i < ((GameViewModel)this.BindingContext).Width; i++)
		{
			GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
		}
		for (int i = 0; i < ((GameViewModel)this.BindingContext).Height; i++)
		{
			GameGrid.RowDefinitions.Add(new RowDefinition());
		}

	}
}