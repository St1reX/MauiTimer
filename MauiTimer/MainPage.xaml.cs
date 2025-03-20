namespace MauiTimer
{
    public partial class MainPage : ContentPage
    {
        string timeString = "00:00:00";
        public MainPage()
        {
            InitializeComponent();

            Thread timeThread = new Thread(timer);

            timeThread.Start();
        }

        void timer()
        {
            while (true)
            {
                DateTime time = DateTime.Now;
                string hourString = time.Hour.ToString().Length == 1 ? "0" + time.Hour.ToString() : time.Hour.ToString();
                string minuteString = time.Minute.ToString().Length == 1 ? "0" + time.Minute.ToString() : time.Minute.ToString();
                string secondString = time.Second.ToString().Length == 1 ? "0" + time.Second.ToString() : time.Second.ToString();
                timeString = hourString + ":" + minuteString + ":" + secondString;

                Dispatcher.Dispatch(new Action(() => { label.Text = timeString; }));

                Dispatcher.Dispatch(new Action(() => { hourLine.Rotation = ((time.Hour%12) * 30) + (time.Minute * 0.5) + (time.Second / 120.0); }));
                Dispatcher.Dispatch(new Action(() => { minuteLine.Rotation = (time.Minute * 6) + (time.Second / 10.0); }));
                Dispatcher.Dispatch(new Action(() => { secondLine.Rotation = (time.Second * 6); }));

                Thread.Sleep(100);                
            }
        }
    }
}
