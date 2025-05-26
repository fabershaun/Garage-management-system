using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class GarageUI
    {
        private readonly GarageManager r_GarageManager = new GarageManager();

        public void Run()
        {
            bool exitRequested = false;

            while(!exitRequested)
            {
                printMainMenu();
                string userChoice = Console.ReadLine();
                Console.Clear();

                switch(userChoice)
                {
                    case "1":
                        loadVehiclesFromFile();
                        break;
                    case "2":
                        insertNewVehicle();
                        break;
                    case "3":
                        showLicenseNumbers();
                        break;
                    case "4":
                        changeVehicleStatus();
                        break;
                    case "5":
                        inflateWheelsToMax();
                        break;
                    case "6":
                        refuelVehicle();
                        break;
                    case "7":
                        chargeVehicle();
                        break;
                    case "8":
                        showVehicleDetails();
                        break;
                    case "9":
                        exitRequested = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please choose a valid option.");
                        break;
                }

                if(!exitRequested)
                {
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        private void printMainMenu()
        {
            Console.WriteLine("=== Garage Management System ===");
            Console.WriteLine();
            Console.WriteLine("1. Load vehicles from file");
            Console.WriteLine("2. Insert new vehicle");
            Console.WriteLine("3. Show license numbers");
            Console.WriteLine("4. Change vehicle status");
            Console.WriteLine("5. Inflate wheels to max");
            Console.WriteLine("6. Refuel vehicle");
            Console.WriteLine("7. Charge electric vehicle");
            Console.WriteLine("8. Show full vehicle details");
            Console.WriteLine("9. Exit");
            Console.WriteLine();
            Console.Write("Choose an option: ");
        }

        private void loadVehiclesFromFile()
        {
            List<string> errors;

            try
            {
                errors = r_GarageManager.LoadVehiclesFromFile("Vehicles.db");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading vehicles: {ex.Message}");
                return;
            }

            if (errors.Count == 0)
            {
                Console.WriteLine("Vehicles loaded successfully from file.");
            }
            else
            {
                Console.WriteLine("Some vehicles were not loaded due to errors:");
                foreach (string error in errors)
                {
                    Console.WriteLine(" - " + error);
                }
            }
        }

        private void insertNewVehicle()
        {
            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            if (r_GarageManager.m_VehiclesInGarage.ContainsKey(licensePlate))
            {
                Console.WriteLine("Vehicle already exists in the garage.");
                return;
            }

            Console.WriteLine("Select vehicle type:");
            List<string> supportedTypes = VehicleCreator.SupportedTypes;

            for (int i = 0; i < supportedTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {supportedTypes[i]}");
            }

            int typeIndex = int.Parse(Console.ReadLine()) - 1;      // Get the user choice for vehicle type
            string selectedType = supportedTypes[typeIndex];

            Console.WriteLine("Enter Model name:");
            string modelName = Console.ReadLine();

            Vehicle vehicle = VehicleCreator.CreateVehicle(selectedType, licensePlate, modelName);
            
            List<string> questionsForType = getQuestionsForType(vehicle);
            string[] answers = new string[questionsForType.Count];

            for (int i = 0; i < questionsForType.Count; i++)
            {
                Console.Write(questionsForType[i] + ": ");
                answers[i] = Console.ReadLine();
            }

            try
            {
                // Parse and assign values from answers array
                float energyPercentage = float.Parse(answers[1]);
                string wheelManufacturer = answers[2];
                float currentAirPressure = float.Parse(answers[3]);

                vehicle.SetEnergyPercentage(energyPercentage);

                for (int i = 0; i < vehicle.Wheels.Count; i++)
                {
                    vehicle.Wheels[i].Manufacturer = wheelManufacturer;
                    vehicle.Wheels[i].CurrentAirPressure = currentAirPressure;
                }

                vehicle.SetAdditionalInfo(answers[4], answers[5]);
                Console.Write("Enter owner's name: ");
                string ownerName = Console.ReadLine();
                Console.Write("Enter owner's phone number: ");
                string ownerPhone = Console.ReadLine();

                VehicleInGarage vehicleInGarage = new VehicleInGarage(ownerName, ownerPhone, Utils.eGarageVehicleStatus.InRepair, vehicle);
                r_GarageManager.m_VehiclesInGarage.Add(licensePlate, vehicleInGarage);

                Console.WriteLine("Vehicle inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to insert vehicle: {ex.Message}");
            }
        }

        private List<string> getQuestionsForType(Vehicle i_Vehicle)
        {
            List<string> questionForType = new List<string>();

            questionForType.Add("Model name");
            questionForType.Add("Energy percentage");
            questionForType.Add("Wheel manufacturer");
            questionForType.Add("Current wheel air pressure");
            i_Vehicle.AddAdditionalQuestions(questionForType);

            return questionForType;
        }
    }
}
