using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Test3
{
    class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                string city;
                string response;
                Console.WriteLine("Тестовое задание 3");
                Console.WriteLine("Погода");
                Console.Write("Город: ");
                city = Console.ReadLine();
                string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&APPID=0845f312a00eefb63ce4a3f5b6b049a1";

                try
                {
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                    using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
                    {
                        response = streamReader.ReadToEnd();
                    }


                    WeatherResp weatherResp = JsonConvert.DeserializeObject<WeatherResp>(response);
                    DateTime sunriseDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(weatherResp.sys.Sunrise));
                    if (sunriseDate.Hour + 3 < 10) { }

                    DateTime sunsetDate = (new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(weatherResp.sys.Sunset));

                    string name = "Город: " + weatherResp.Name;
                    string temperature = "Температура: " + weatherResp.Main.Temp + "°C";
                    string humidity = "Влажность: " + weatherResp.Main.Humidity + "%";
                    string sunrise = "Восход: " + (sunriseDate.Hour + 3).ToString("D2") + ":" + sunriseDate.Minute.ToString("D2");
                    string sunset = "Закат: " + (sunsetDate.Hour + 3).ToString("D2") + ":" + sunsetDate.Minute.ToString("D2");

                    Console.WriteLine(temperature);
                    Console.WriteLine(humidity);
                    Console.WriteLine(sunrise);
                    Console.WriteLine(sunset);

                    DirectoryInfo directoryInfo = new DirectoryInfo("Logs");
                    if (!directoryInfo.Exists)
                    {
                        directoryInfo.Create();
                        Console.WriteLine("Директория 'Logs' успешно создана.");
                    }

                    string filename = DateTime.Now.ToShortDateString() + ".txt";
                    string filepath = Environment.CurrentDirectory + "/Logs/" + filename;

                    using (StreamWriter sw = new StreamWriter(filepath, true, Encoding.Default))
                    {
                        sw.WriteLine(name + Environment.NewLine +
                            temperature + Environment.NewLine +
                            humidity + Environment.NewLine +
                            sunrise + Environment.NewLine +
                            sunset + Environment.NewLine);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.ReadLine();
            }
        }
    }
}

