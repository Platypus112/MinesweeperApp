//using Android.Views;
using MinesweeperApp.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MinesweeperApp.Views;

public partial class GameView : ContentPage
{
    //private double panX;
    //private double panY;
    //private double currentScale = 1;
    //private double startScale = 1;
    //private double xOffset = 0;
    //private double yOffset = 0;

    public GameView(GameViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
        //ArranageGrid();
        //AddPanAndZoomToGrid();
        Task.Run(async () => { vm.InServerCall = true; await Task.Delay(10000); vm.GridHeight = GameGrid.Height; vm.InServerCall = false; });
        PinchGestureRecognizer pinchGesture = new PinchGestureRecognizer();
        pinchGesture.PinchUpdated += OnPinchUpdated;
        View.GestureRecognizers.Add(pinchGesture);
        // Set PanGestureRecognizer.TouchPoints to control the
        // number of touch points needed to pan
        PanGestureRecognizer panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += OnPanUpdated;
        View.GestureRecognizers.Add(panGesture);
    }

    public void ArranageGrid()
    {
        for (int i = 0; i < ((GameViewModel)this.BindingContext).Width; i++)
        {
            GameGrid.ColumnDefinitions.Add(new ColumnDefinition());
        }
        for (int i = 0; i < ((GameViewModel)this.BindingContext).Height; i++)
        {
            GameGrid.RowDefinitions.Add(new RowDefinition());
        }

    }
    double panX, panY;

    double currentScale = 1;
    double startScale = 1;
    double xOffset = 0;
    double yOffset = 0;
    public void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Running:
                // Translate and pan.
                double boundsX = GameGrid.Width;
                double boundsY = GameGrid.Height;
                GameGrid.TranslationX = Math.Clamp(panX + e.TotalX, -boundsX, boundsX);
                GameGrid.TranslationY = Math.Clamp(panY + e.TotalY, -boundsY, boundsY);
                break;

            case GestureStatus.Completed:
                // Store the translation applied during the pan
                panX = GameGrid.TranslationX;
                panY = GameGrid.TranslationY;
                break;
        }
    }
    public void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
    {
        if (e.Status == GestureStatus.Started)
        {
            // Store the current scale factor applied to the wrapped user interface element,
            // and zero the components for the center point of the translate transform.
            startScale = GameGrid.Scale;
            GameGrid.AnchorX = 0;
            GameGrid.AnchorY = 0;
        }
        if (e.Status == GestureStatus.Running)
        {
            // Calculate the scale factor to be applied.
            currentScale += (e.Scale - 1) * startScale;
            currentScale = Math.Max(1, currentScale);

            // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
            // so get the X pixel coordinate.
            double renderedX = GameGrid.X + xOffset;
            double deltaX = renderedX / Width;
            double deltaWidth = Width / (GameGrid.Width * startScale);
            double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

            // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
            // so get the Y pixel coordinate.
            double renderedY = GameGrid.Y + yOffset;
            double deltaY = renderedY / Height;
            double deltaHeight = Height / (GameGrid.Height * startScale);
            double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

            // Calculate the transformed element pixel coordinates.
            double targetX = xOffset - (originX * GameGrid.Width) * (currentScale - startScale);
            double targetY = yOffset - (originY * GameGrid.Height) * (currentScale - startScale);

            // Apply translation based on the change in origin.
            GameGrid.TranslationX = Math.Clamp(targetX, -GameGrid.Width * (currentScale - 1), 0);
            GameGrid.TranslationY = Math.Clamp(targetY, -GameGrid.Height * (currentScale - 1), 0);

            // Apply scale factor
            GameGrid.Scale = currentScale;
        }
        if (e.Status == GestureStatus.Completed)
        {
            // Store the translation delta's of the wrapped user interface element.
            xOffset = GameGrid.TranslationX;
            yOffset = GameGrid.TranslationY;
        }
        //public void AddPanAndZoomToGrid()
        //{
        //    PanGestureRecognizer panGesture = new PanGestureRecognizer();
        //    panGesture.PanUpdated += GestureView.OnPanUpdated;
        //    GameGrid.GestureRecognizers.Add(panGesture);
        //    PinchGestureRecognizer pinchGesture = new PinchGestureRecognizer();
        //    pinchGesture.PinchUpdated += GestureView.OnPinchUpdated;
        //    GameGrid.GestureRecognizers.Add(pinchGesture);
        //}
        //public void AddPanAndZoomToGrid()
        //{
        //    PanGestureRecognizer panGesture = new PanGestureRecognizer();
        //    panGesture.PanUpdated += OnPanUpdated;
        //    GameGrid.GestureRecognizers.Add(panGesture);
        //    PinchGestureRecognizer pinchGesture = new PinchGestureRecognizer();
        //    pinchGesture.PinchUpdated += OnPinchUpdated;
        //    GameGrid.GestureRecognizers.Add(pinchGesture);
        //}
        //async void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        //{
        //    if (e.Status == GestureStatus.Started)
        //    {
        //        // Store the current scale factor applied to the wrapped user interface element,
        //        // and zero the components for the center point of the translate transform.
        //        startScale = GridView.Content.Scale;

        //        GridView.Content.AnchorX = 0;
        //        GridView.Content.AnchorY = 0;
        //    }
        //    if (e.Status == GestureStatus.Running)
        //    {
        //        // Calculate the scale factor to be applied.
        //        currentScale += (e.Scale - 1) * startScale;
        //        currentScale = Math.Max(1, currentScale);

        //        // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
        //        // so get the X pixel coordinate.
        //        double renderedX = GridView.Content.X + xOffset;
        //        double deltaX = renderedX / Width;
        //        double deltaWidth = Width / (GridView.Content.Width * startScale);
        //        double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

        //        // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
        //        // so get the Y pixel coordinate.
        //        double renderedY = GridView.Content.Y + yOffset;
        //        double deltaY = renderedY / Height;
        //        double deltaHeight = Height / (GridView.Content.Height * startScale);
        //        double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

        //        // Calculate the transformed element pixel coordinates.
        //        double targetX = xOffset - (originX * GridView.Content.Width) * (currentScale - startScale);
        //        double targetY = yOffset - (originY * GridView.Content.Height) * (currentScale - startScale);

        //        // Apply translation based on the change in origin.
        //        GridView.Content.TranslationX = Math.Clamp(targetX, -GridView.Content.Width * (currentScale - 1), 0);
        //        GridView.Content.TranslationY = Math.Clamp(targetY, -GridView.Content.Height * (currentScale - 1), 0);

        //        // Apply scale factor
        //        GridView.Content.Scale = currentScale;
        //    }
        //    if (e.Status == GestureStatus.Completed)
        //    {
        //        // Store the translation delta's of the wrapped user interface element.
        //        xOffset = GridView.Content.TranslationX;
        //        yOffset = GridView.Content.TranslationY;
        //    }
        //}

        //async void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        //{
        //    switch (e.StatusType)
        //    {
        //        case GestureStatus.Running:
        //            // Translate and pan.
        //            double boundsX = GridView.Content.Width;
        //            double boundsY = GridView.Content.Height;
        //            GridView.Content.TranslationX = Math.Clamp(panX + e.TotalX, -boundsX, boundsX);
        //            GridView.Content.TranslationY = Math.Clamp(panY + e.TotalY, -boundsY, boundsY);
        //            break;

        //        case GestureStatus.Completed:
        //            // Store the translation applied during the pan
        //            panX = GridView.Content.TranslationX;
        //            panY = GridView.Content.TranslationY;
        //            break;
        //    }
        //}
    }
}