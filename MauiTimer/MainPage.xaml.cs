using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;
using System.Drawing;
using System.Text.RegularExpressions;

namespace MauiTimer
{
    public partial class MainPage : ContentPage
    {
        //XAML ELEMENTS
        private Line hourLine = default!;
        private Line minuteLine = default!;
        private Line secondLine = default!;

        //THREAD
        Thread clockThread = default!;
        bool stopThread = false;

        //TIME STRING
        string formattedTimeString = "00:00:00";

        public MainPage()
        {
            InitializeComponent();
            App.Current!.UserAppTheme = AppTheme.Light;


            RenderClock();


            clockThread = new Thread(RefreshClock);
            clockThread.Start();
        }

        void RefreshClock()
        {
            while (true)
            {
                if (stopThread)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    DateTime time = DateTime.Now;
                    string hours = time.Hour.ToString("D2");
                    string minutes = time.Minute.ToString("D2");
                    string seconds = time.Second.ToString("D2");
                    string milliseconds = time.Millisecond.ToString("D3");


                    formattedTimeString = hours + " : " + minutes + " : " + seconds + " : " + milliseconds;
                    Dispatcher.Dispatch(new Action(() => { digital_clock.Text = formattedTimeString; }));

                    Dispatcher.Dispatch(new Action(() => { hourLine.Rotation = ((time.Hour % 12) * 30) + (time.Minute / 2) + (time.Second / 120.0); }));
                    Dispatcher.Dispatch(new Action(() => { minuteLine.Rotation = (time.Minute * 6) + (time.Second / 10.0); }));
                    Dispatcher.Dispatch(new Action(() => { secondLine.Rotation = (time.Second * 6) + (time.Millisecond / 166.67); }));

                    Thread.Sleep(15);
                }
            }
        }

        void RenderClock()
        {
            //VARIABLES
            var clockFaceContainer = clock_face_container;
            var clockFaceContainerWidth = clockFaceContainer.WidthRequest;
            var clockFaceContainerHeight = clockFaceContainer.HeightRequest;

            var clockFace = clock_face;
            clockFace.WidthRequest = clockFaceContainerWidth;
            clockFace.HeightRequest = clockFaceContainerHeight;


            //DELETING UNNECESSARY LINES
            {
                var childrenToRemove = clockFaceContainer.Children
                              .OfType<Line>()
                              .ToList();

                foreach (var child in childrenToRemove)
                {
                    clockFaceContainer.Children.Remove(child);
                }
            }

            //GRADUATION LINES
            for (int i = 0; i < 60; i++)
            {
                var graduationLine = new Line
                {
                    Stroke = Microsoft.Maui.Controls.Brush.Black,
                    StrokeThickness = (i % 5 == 0) ? 4 : 2,

                    X1 = clockFaceContainerWidth/2,
                    Y1 = 0,
                    X2 = clockFaceContainerWidth / 2,
                    Y2 = (i % 5 == 0) ? 15 : 10,

                    WidthRequest = clockFaceContainerWidth,
                    HeightRequest = clockFaceContainerHeight,

                    AnchorX = 0.5,
                    AnchorY = 0.5,

                    Rotation = i * 6
                };

                AbsoluteLayout.SetLayoutBounds(graduationLine, new Rect(0.5, 0.5, -1, -1));
                AbsoluteLayout.SetLayoutFlags(graduationLine, AbsoluteLayoutFlags.PositionProportional);
                clockFaceContainer.Children.Add(graduationLine);
            }

            //TIME LINES
            {
                hourLine = new Line
                {
                    Stroke = Microsoft.Maui.Controls.Brush.Black,
                    StrokeThickness = 4,

                    X1 = 0,
                    X2 = 0,
                    Y1 = clockFaceContainerHeight * 0.3,
                    Y2 = 0,

                    TranslationY = -(clockFaceContainerHeight * 0.3) / 2,

                    AnchorX = 0.5,
                    AnchorY = 1,

                    Rotation = 6
                };

                AbsoluteLayout.SetLayoutBounds(hourLine, new Rect(0.5, 0.5, -1, -1));
                AbsoluteLayout.SetLayoutFlags(hourLine, AbsoluteLayoutFlags.PositionProportional);
                clockFaceContainer.Children.Add(hourLine);

                minuteLine = new Line
                {
                    Stroke = Microsoft.Maui.Controls.Brush.Black,
                    StrokeThickness = 2,

                    X1 = 0,
                    X2 = 0,
                    Y1 = clockFaceContainerHeight * 0.35,
                    Y2 = 0,

                    TranslationY = -(clockFaceContainerHeight * 0.35) / 2,

                    AnchorX = 0.5,
                    AnchorY = 1,

                    Rotation = 90

                };

                AbsoluteLayout.SetLayoutBounds(minuteLine, new Rect(0.5, 0.5, -1, -1));
                AbsoluteLayout.SetLayoutFlags(minuteLine, AbsoluteLayoutFlags.PositionProportional);
                clockFaceContainer.Children.Add(minuteLine);

                secondLine = new Line
                {
                    Stroke = Microsoft.Maui.Controls.Brush.Red,
                    StrokeThickness = 1,

                    X1 = 0,
                    X2 = 0,
                    Y1 = clockFaceContainerHeight * 0.42,
                    Y2 = 0,

                    TranslationY = -(clockFaceContainerHeight * 0.42) / 2,

                    AnchorX = 0.5,
                    AnchorY = 1,

                    Rotation = 90

                };

                AbsoluteLayout.SetLayoutBounds(secondLine, new Rect(0.5, 0.5, -1, -1));
                AbsoluteLayout.SetLayoutFlags(secondLine, AbsoluteLayoutFlags.PositionProportional);
                clockFaceContainer.Children.Add(secondLine);
            }

            //ANCHORAGE POINT
            {
                var anchoragePoint = new Ellipse
                {
                    Fill = Microsoft.Maui.Controls.Brush.Black,
                    WidthRequest = 10,
                    HeightRequest = 10
                };

                AbsoluteLayout.SetLayoutBounds(anchoragePoint, new Rect(0.5, 0.5, -1, -1));
                AbsoluteLayout.SetLayoutFlags(anchoragePoint, AbsoluteLayoutFlags.PositionProportional);
                clockFaceContainer.Children.Add(anchoragePoint);
            }
        }

        private void Change_Clock_Size(object sender, EventArgs e)
        {
            stopThread = true;

            var clockSize = clock_size_input.Text;
            Regex clockSizeRegex = new Regex(@"^\d+$");

            if (clockSizeRegex.IsMatch(clockSize))
            {
                clock_face_container.WidthRequest = Convert.ToInt32(clockSize);
                clock_face_container.HeightRequest = Convert.ToInt32(clockSize);

                RenderClock();

                stopThread = false;
            }
            else
            {
                clock_size_input.Text = "Invalid input";

                stopThread = false;
            }
        }
    }
}
