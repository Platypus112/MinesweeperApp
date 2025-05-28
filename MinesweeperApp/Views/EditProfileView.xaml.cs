using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class EditProfileView : ContentPage
{
	public EditProfileView(EditProfileViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        //THe code below is a workarround for a bug in the Image control in MAUI
        //https://github.com/dotnet/maui/issues/18656
        base.OnAppearing();
        var bc = theImageBug.BindingContext;
        theImageBug.BindingContext = null;
        theImageBug.BindingContext = bc;

        ((EditProfileViewModel)this.BindingContext).RefreshPage();

    }
}