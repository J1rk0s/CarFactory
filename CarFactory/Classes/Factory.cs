using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Spectre.Console;

namespace CarFactory.Classes {
    public static class Factory {
        private static readonly List<string> Models = new List<string>() {
            "model x", "model s", "model 3"
        };
        
        public static void Menu() {
            while (true)
            {
                string input = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("Select an option")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more options)")
                    .AddChoices(new[]
                        {"1) Model X",
                        "2) Model S",
                        "3) Model 3",
                        "4) Show current car",
                        "5) Exit"}));
                Console.WriteLine(input);
                switch (input)
                {
                    case "1) Model X":
                        CreatePage(MakeCar("Model X"));
                        ShowPage("negr.html");
                        Console.Clear();
                        break;
                    case "2) Model S":
                        CreatePage(MakeCar("Model S"));
                        ShowPage("negr.html");
                        Console.Clear();
                        break;
                    case "3) Model 3":
                        CreatePage(MakeCar("Model 3"));
                        ShowPage("negr.html");
                        Console.Clear();
                        break;
                    case "4) Show current car":
                        ShowPage("negr.html");
                        Console.Clear();
                        break;
                    case "5) Exit":
                        Console.WriteLine("Exiting!\nGoodBye!");
                        Thread.Sleep(1000);
                        Console.Clear();
                        return;
                }
            }
        }

        private static Car MakeCar(string model) {
            Console.Clear();
            int seatCount = HelperClass.Input<int>("Enter seat count: ");
            int year = HelperClass.Input<int>("Enter year of production: ");
            string engineType = HelperClass.Input<string>("Select an engine type(Electric/Hybrid): ");
            int price = HelperClass.Input<int>("Enter a price: ");

            if (seatCount < 2 || seatCount > 6) {
                Console.WriteLine("Manufactury error!\n" +
                                  "Defaulting to 5 seats!\n");
                seatCount = 5;
            }

            if (!Models.Contains(model.ToLower())) {
                Console.WriteLine($"{model}: We do not have this model\n" +
                                  "Please select another one\n" +
                                  "Available models are: Model X, Model Y, Model 3\n");
                model = HelperClass.Input<string>("Enter model name: ");
            }

            if (engineType.ToLower().Trim() != "electric" && engineType.ToLower().Trim() != "hybrid") {
                Console.WriteLine($"{engineType}: Invalid engine!\nPlease select another one instead\nAvailable types are: Hybrid, Electric\n");
                engineType = HelperClass.Input<string>("Select engine type: ");
            }

            if (year < 1900 || year > new DateTime().Year) {
                Console.WriteLine("Invalid date! Try again!");
                year = HelperClass.Input<int>("Enter valid year: ");
            }

            if (price < 100000) {
                Console.WriteLine("Invalid price! Try again!");
                price = HelperClass.Input<int>("Enter valid price: ");
            }

            return new Car {
                EngineType = engineType,
                Model = model,
                Price = price,
                SeatCount = seatCount,
                Year = year
            };
        }

        private static void CreatePage(Car car) {
            string imgP = "";
            switch (car.Model)
            {
                case "Model X":
                    imgP = "../../Assets/Imgs/model_X.jpg";
                    break;
                case "Model S":
                    imgP = "../../Assets/Imgs/model_S.jpg";
                    break;
                case "Model 3":
                    imgP = "../../Assets/Imgs/model_3.jpeg";
                    break;
            }
            string html = $@"
<html>
<head>
    <title>Mega sus Car</title>
    <link rel='stylesheet' href='../../Assets/style.css'>
</head>
<body style='background-color:black;'>
<h1>Specs</h1>
<table>
  <tr style:'color:white;'>
    <th>Engine Type</th>
    <th>Seat Count</th>
    <th>Car model</th>
    <th>Year</th>
  </tr>
  <tr style:'color:white;'>
    <td>{car.EngineType}</td>
    <td>{car.SeatCount}</td>
    <td>{car.Model}</td>
    <td>{car.Year}</td>
  </tr>
</table>
<img src='{imgP}'></img>
<p>______________________________</p>
<b style:'color:white;'>price: {car.Price}$</b>
</body>
</html>";
            File.WriteAllText("negr.html", html);
        }

        private static void ShowPage(string location) {
            ProcessStartInfo process = new ProcessStartInfo {
                UseShellExecute = true,
                FileName = location
            };
            Process.Start(process);
        }
    }
}