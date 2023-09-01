using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using System.IO;

public class Vehicle
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string Type { get; set; }
    public string RegistrationNumber { get; set; }

    public Vehicle(string brand, string model, string type, string registrationNumber)
    {
        Brand = brand;
        Model = model;
        Type = type;
        RegistrationNumber = registrationNumber;
    }
}

public class Program
{
    private static List<Vehicle> Cars = new List<Vehicle>();

    public static void Main()
    {
        LoadCarsFromFile();
        while (true)
        {
            ShowMainMenu();
        }
    }

    private static void ShowMainMenu()
    {
        ClearScreenWithColors(ConsoleColor.DarkRed);
        DisplayMenuOptions();

        switch (ReadLine())
        {
            case "1":
                RegisterCar();
                break;
            case "2":
                ListCars();
                break;
            case "3":
                SearchCar();
                break;
            case "4":
                ExitProgram();
                break;
            default:
                WriteCentered("Välj ett giltigt alternativ!");
                break;
        }
    }

    private static void ClearScreenWithColors(ConsoleColor textColor)
    {
        Clear();
        ForegroundColor = textColor;
    }

    private static void AddSpaceLines(int numberOfLines)
    {
        for (int i = 0; i < numberOfLines; i++)
        {
            WriteLine();
        }
    }

    private static void DisplayMenuOptions()
    {
        WriteLine("=================================================================================================================================");
        WriteCentered("********** Rent - A - Wreck **********");
        WriteLine("=================================================================================================================================");

        AddSpaceLines(2);
        ForegroundColor = ConsoleColor.DarkYellow;

        int leftPadding = (WindowWidth / 2) - 20;

        WritePadded("1. Registrera fordon", leftPadding);
        AddSpaceLines(1);
        WritePadded("2. Lista fordon", leftPadding);
        AddSpaceLines(1);
        WritePadded("3. Sök fordon", leftPadding);
        AddSpaceLines(1);
        WritePadded("4. Avsluta", leftPadding);
        AddSpaceLines(1);

        ResetColor();
    }

    private static void WritePadded(string message, int leftPadding)
    {
        WriteLine(message.PadLeft(message.Length + leftPadding));
    }

    private static void WriteCentered(string message)
    {
        int screenWidth = WindowWidth;
        int stringWidth = message.Length;
        int spaces = (screenWidth / 2) - (stringWidth / 2);
        WriteLine(message.PadLeft(message.Length + spaces));
    }

    private static void RegisterCar()
    {
        Clear();
        ForegroundColor = ConsoleColor.Cyan;

        var brand = GetUserInput("Märke:");
        var model = GetUserInput("Model:");
        var type = GetUserInput("Typ:");
        var registrationNumber = GetUserInput("Registreringnummer:");

        var vehicle = new Vehicle(brand, model, type, registrationNumber);
        Cars.Add(vehicle);
        SaveCarsToFile();

        WriteCentered("Fordonet är registrerat!");
        PauseBeforeContinuing();
    }

    private static string GetUserInput(string prompt)
    {
        int screenWidth = WindowWidth;
        int stringWidth = prompt.Length;
        int spaces = (screenWidth / 2) - (stringWidth / 2);
        Write(prompt.PadLeft(prompt.Length + spaces));
        return ReadLine() ?? string.Empty;
    }

    private static void PauseBeforeContinuing()
    {
        Thread.Sleep(2000);
    }

    private static void ListCars()
    {
        ClearScreenWithColors(ConsoleColor.DarkYellow);
        WriteCentered("            Märke               Modell               Typ                RegistreringsNummer");
        WriteCentered("============================================================================================");

        foreach (var vehicle in Cars)
        {
            WriteCentered($"{vehicle.Brand.PadRight(20)}{vehicle.Model.PadRight(20)}{vehicle.Type.PadRight(20)}{vehicle.RegistrationNumber}");
        }
        ReadLine();
    }

    private static void SearchCar()
    {
        Clear();
        var registrationNumber = GetUserInput("Sök registreringsnummer:");
        var carFound = Cars.Find(vehicle => string.Equals(vehicle.RegistrationNumber, registrationNumber, StringComparison.OrdinalIgnoreCase));

        DisplaySearchResults(carFound);
        ReadLine();
    }

    private static void DisplaySearchResults(Vehicle? car)
    {
        if (car != null)
        {
            DisplayVehicleDetails(car);
        }
        else
        {
            WriteCentered("Fordon hittades inte!");
        }
    }

    private static void DisplayVehicleDetails(Vehicle car)
    {
        WriteLine($"Merke: {car.Brand}");
        AddSpaceLines(1);

        WriteLine($"Modell: {car.Model}");
        AddSpaceLines(1);

        WriteLine($"Typ: {car.Type}");
        AddSpaceLines(1);

        WriteLine($"Registreringsnummer: {car.RegistrationNumber}");
    }

    private const string StorageFile = "cars.json";

    private static void SaveCarsToFile()
    {
        var jsonData = JsonConvert.SerializeObject(Cars);
        File.WriteAllText(StorageFile, jsonData);
    }

    private static void LoadCarsFromFile()
    {
        if (File.Exists(StorageFile))
        {
            var jsonData = File.ReadAllText(StorageFile);
            Cars = JsonConvert.DeserializeObject<List<Vehicle>>(jsonData);
        }
    }

    private static void ExitProgram()
    {
        Clear();
        WriteCentered("Rent-A-Wreck avslutas nu!");
        PauseBeforeContinuing();
        Environment.Exit(0);
    }
}
