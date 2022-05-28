using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Spectre.Console;

namespace CarFactory.Classes {
    public static class Factory {
        private static readonly List<string> Models = new List<string>() {
            "model x", "model y", "model 3"
        };
        
        public static void Menu() {
            string input = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("Select an option")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more options)")
                .AddChoices(new[] 
                    {"1) Model X", 
                        "2) Model S", 
                        "3) Model 3",
                        "4) Show last car",
                        "5) Show current car",
                        "6) Exit"}));
            Console.WriteLine(input);
            switch (input) {
                case "1) Model X":
                    Car c = MakeCar("Model X");
                    CreatePage(c);
                    ShowPage("negr.html");
                    break;
                case "2) Model S":
                    break;
                case "3) Model 3":
                    break;
                case "4) Show last car":
                    break;
                case "5) Show current car":
                    break;
                case "6) Exit":
                    break;
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

            if (engineType.ToLower() != "electric" || engineType.ToLower() != "hybrid") {
                Console.WriteLine($"{engineType}: Invalid engine!\nPlease select another one instead\nAvailable types are: Hybrid, Electric\n");
                engineType = HelperClass.Input<string>("Select engine type: ");
            }
            
            return new Car {
                EngineType = engineType,
                Model = model,
                Price = price,
                SeatCount = seatCount,
                Year = year
            };
        }

        public static void CreatePage(Car car) {
            string html = $@"
<html>
<head>
    <title>{car.Model}</title>
</head>
<body>
<h1>{car.EngineType}</h1>
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