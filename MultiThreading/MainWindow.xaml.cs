using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MultiThreading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InitializeTimer();
        }

        int counter = 0;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            Tbx_Counter.Text = counter.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Btn_Slow_01.IsEnabled = false;

            Thread.Sleep(2000);
            Tbl_Slow_01.Text = "TASK FINISHED";

            Btn_Slow_01.IsEnabled = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Btn_Slow_02.IsEnabled = false;


            var task = Task.Run(() =>
            {
                Thread.Sleep(3000);
                return "TASK FINISHED";
            });

            task.ContinueWith(t =>
            {
                Dispatcher.Invoke(() =>
                {
                    Tbl_Slow_02.Text = t.Result;
                    Btn_Slow_02.IsEnabled = true;
                });
            });
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Btn_Slow_03.IsEnabled = false;

            var taskResult = await Task.Run(() =>
            {
                Thread.Sleep(3000);
                return "TASK FINISHED";
            });

            Tbl_Slow_03.Text = taskResult;
            Btn_Slow_03.IsEnabled = true;
        }



        #region Längeres Dispather-Beispiel

        private void Btn_AskForWeatherForecast_Click(object sender, RoutedEventArgs e)
        {
            //Initialisierung
            Btn_AskForWeatherForecast.IsEnabled = false;
            Btn_AskForWeatherForecast.Content = "Contacting Server";
            Lbl_WeatherForecast.Content = "";
            Pgb_WaitForWeatherForecast.IsIndeterminate = true;

            //Starte Abfrage
            Task.Run(FetchWeatherFromServer);

        }

        private async void FetchWeatherFromServer()
        {
            // Simuliere Verzögerung beim Netzwerkzugriff
            Thread.Sleep(2000);

            string wc, temp;

            using (WebClient client = new WebClient())
            {
                string json = await client.DownloadStringTaskAsync(new Uri("https://api.open-meteo.com/v1/forecast?latitude=52.5244&longitude=13.4105&daily=weathercode,temperature_2m_max&timezone=Europe%2FBerlin&forecast_days=1"));
                string[] parts = json.Split(',');

                wc = parts[parts.Length - 2];

                temp = parts[parts.Length - 1];
            }

            wc = wc.Substring(wc.IndexOf("[") + 1);
            wc = wc.Substring(0, wc.IndexOf("]"));
            int WC = int.Parse(wc);

            temp = temp.Substring(temp.IndexOf("[") + 1);
            temp = temp.Substring(0, temp.IndexOf("]")).Replace('.', ',');
            double TEMP = double.Parse(temp);

            String weather = CheckWeatherCode(WC);
            
            // Schedule the update function in the UI thread.
            await this.Dispatcher.BeginInvoke
                (
                    System.Windows.Threading.DispatcherPriority.Normal,
                    new Action<string, double>(UpdateUserInterface),
                    weather,
                    TEMP
                );
        }

        private string CheckWeatherCode(int code) => code switch
        {
            0 => "clear sky",
            1 => "Mainly clear",
            2 => "partly cloudy",
            3 => "overcast",
            45 => "Fog",
            48 => "depositing rime fog",
            51 => "light drizzle",
            53 => "moderate drizzle",
            55 => "dense drizzle",
            56 => "light freezing drizzle",
            57 => "dense freezing drizzle",
            61 => "light rain",
            63 => "moderate rain",
            65 => "dense rain",
            66 => "light freezing rain",
            67 => "heavy freezing rain",
            71 => "slight snow fall",
            73 => "moderate snow fall",
            75 => "heavy snow fall",
            77 => "Snow grains",
            80 => "slight rain showers",
            81 => "moderate rain showers",
            82 => "violent rain showers",
            85 => "slight snow showers",
            86 => "heavy snow showers",
            95 => "Thunderstorm: Slight or moderate",
            96 => "Thunderstorm with slight hail",
            99 => "Thunderstorm with heavy hail"
        };

        private void UpdateUserInterface(String weather, double temp)
        {
            //Show Forecast
            Lbl_WeatherForecast.Content = $"{weather} ({temp}°C)";

            //Stop progressbar animation
            Pgb_WaitForWeatherForecast.IsIndeterminate = false;

            //Update UI text
            Btn_AskForWeatherForecast.IsEnabled = true;
            Btn_AskForWeatherForecast.Content = "Fetch Forecast";
        }

        #endregion


        DispatcherTimer timer;

        int timerCounter = 0;

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();

            timer.Tick += (s, e) =>
            {
                timerCounter++;
                Tbl_Timer.Text = timerCounter.ToString();
            };

            timer.Interval = TimeSpan.FromSeconds(1);
        }

        private void Btn_StartTimer_Click(object sender, RoutedEventArgs e) => timer.Start();

        private void Btn_StopTimer_Click(object sender, RoutedEventArgs e) => timer.Stop();
    }
}
