using  static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


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
    private static List<Vehicle> cars = new List<Vehicle>();

    public static void Main()
    {
        while (true)
        {
            ShowMainMenu();
            
        }
    }

    public static void ShowMainMenu()
    {
        Clear();
        ForegroundColor = ConsoleColor.DarkRed;
        WriteCentered("********** Rent - A - Wreck * *********");
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteCentered("1. Registrera fordon");
        WriteCentered("2. Lista fordon");
        WriteCentered("3. Sök fordon");
        WriteCentered("4. Avsluta");
        ResetColor();

        //var choice = Console.ReadLine();

        switch (Console.ReadLine())
        {
            case "1":
                RegisterCar();
                break;

            case "2":
                ListCar();
                break;
            case "3":
                SearchCar();
                break;
            case "4":
                ExitProgram();
                break;
            

        }
    }


    private static void WriteCentered(string message)
    {
        int screenWidth = WindowWidth;
        int stringWidth = message.Length;
        int spaces = (screenWidth / 2) - (stringWidth / 2);

        WriteLine(message.PadLeft(message.Length + spaces));
    }

    static void RegisterCar()
    {
        Clear();
        ForegroundColor = ConsoleColor.Cyan;
        WriteCentered("Märke: ");
        var brand = ReadLine();
        WriteCentered("Model: ");
        var model = ReadLine();
        WriteCentered("Typ: ");
        var type = ReadLine();
        WriteCentered("Registreringnummer: ");
        var registreringNummer = ReadLine();


        var vehicle = new Vehicle(brand, model, type, registreringNummer);
        cars.Add(vehicle);

        WriteCentered("Fordonet är registrerat!");
        Thread.Sleep(2000);

    }

    static void ListCar()
    {
       Clear();
       WriteCentered("Märke    Modell      Typ     RegistreringsNummer");
       WriteCentered("=================================================");

        foreach (var vehicle in cars)
        {
            WriteCentered($"{vehicle.Brand.PadRight(10)}     {vehicle.Model.PadRight(10)}     {vehicle.Type.PadRight(10)}      {vehicle.RegistrationNumber}");
        }

        ReadLine();
    }

    static void SearchCar()
    {
        Clear();
        WriteCentered("Sök registreringsnummer!");
        var registreringsNummer = ReadLine();
        var carFound = cars.Find(vehicle =>
            string.Equals(vehicle.RegistrationNumber, registreringsNummer, StringComparison.OrdinalIgnoreCase));

        if (carFound != null)
        {
            WriteCentered($"Merke {carFound.Brand}");
            WriteCentered($"Modell {carFound.Model}");
            WriteCentered($"Typ {carFound.Type}");
            WriteCentered($"Registreringsnummer {carFound.RegistrationNumber}");
        }
        else
        {
            WriteCentered("Fordon hittades inte!");

        }
        ReadLine();
    }


    static void ExitProgram()
    {
        Clear();
        WriteCentered("Rent-A-Wreck avslutas nu!");
        Thread.Sleep(3000);
        Environment.Exit(0);       

    }

}

