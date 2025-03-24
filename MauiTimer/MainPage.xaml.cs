using Microsoft.Maui.Controls.Shapes;
using System.Drawing;

namespace MauiTimer
{
    public partial class MainPage : ContentPage
    {
        string timeString = "00:00:00";
        public MainPage()
        {
            InitializeComponent();
            RenderGraduation();

            App.Current!.UserAppTheme = AppTheme.Light;

            //Thread timeThread = new Thread(timer);

            //timeThread.Start();
        }

        //void timer()
        //{
        //    while (true)
        //    {
        //        DateTime time = DateTime.Now;
        //        string hourString = time.Hour.ToString().Length == 1 ? "0" + time.Hour.ToString() : time.Hour.ToString();
        //        string minuteString = time.Minute.ToString().Length == 1 ? "0" + time.Minute.ToString() : time.Minute.ToString();
        //        string secondString = time.Second.ToString().Length == 1 ? "0" + time.Second.ToString() : time.Second.ToString();
        //        //timeString = hourString + ":" + minuteString + ":" + secondString;

        //        //Dispatcher.Dispatch(new Action(() => { label.Text = timeString; }));

        //        Dispatcher.Dispatch(new Action(() => { hourLine.Rotation = ((time.Hour%12) * 30) + (time.Minute * 0.5) + (time.Second / 120.0); }));
        //        Dispatcher.Dispatch(new Action(() => { minuteLine.Rotation = (time.Minute * 6) + (time.Second / 10.0); }));
        //        Dispatcher.Dispatch(new Action(() => { secondLine.Rotation = (time.Second * 6); }));

        //        Thread.Sleep(100);                
        //    }
        //}

        void PlaceElements()
        {
            
        }


        void RenderGraduation()
        {
            var clockFaceBox = clock_face_container;
            //var clockFace = clock_face;

            for (int i = 0; i < 60; i++)
            {
                var line = new Line();
                line.Stroke = Microsoft.Maui.Controls.Brush.Black;
                line.X1 = 50;
                line.Y1 = 30;
                line.X2 = 50;
                line.Y2 = 40;
                line.Rotation = i * 6;
                line.TranslationX = 0.5;
                line.TranslationY = 0.5;
                line.AnchorX = 0;
                line.AnchorY = 0;
                clockFaceBox.Children.Add(line);
            }
        }


        //XAML COMMENTED OUT
//        <Ellipse
//    x:Name="clock_face"
//    WidthRequest="300"
//    HeightRequest="300"
//    Fill="Transparent"
//    StrokeThickness="2"
//    Stroke="Black"
//    TranslationY="30"
//    />

//<Line
//    Stroke = "Black"
//    X1="50"
//    Y1="30"
//    X2="50"
//    Y2="40"/>

// <Line
//    Stroke = "Black"
//    X1="100"
//    Y1="80"
//    X2="90"
//    Y2="80"/>

// <Line
//    Stroke = "Black"
//    X1="50"
//    Y1="130"
//    X2="50"
//    Y2="120"/>

//<Line
//    Stroke = "Black"
//    X1="0"
//    Y1="80"
//    X2="10"
//    Y2="80"/>

//<Rectangle
//    x:Name="secondLine"
//    Fill="Black"
//    AnchorX="0"
//    AnchorY="0"
//    AbsoluteLayout.LayoutBounds="50, 80, 2, 50"
//    />

//<Rectangle
//    x:Name="minuteLine"
//    Fill="Black"
//    AnchorX="0"
//    AnchorY="0"
//    AbsoluteLayout.LayoutBounds="50, 80, 2, 30"
//    />

//<Rectangle
//    x:Name="hourLine"
//    Fill="Red"
//    AnchorX="0"
//    AnchorY="0"
//    AbsoluteLayout.LayoutBounds="50, 80, 3, 20"
//    />

    }
}
