using System;
using System.Collections.Generic;
using System.Linq;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.VehicleInGarage;

namespace Ex03.ConsoleUI
{
    internal class GarageUI
    {
        private readonly GarageManager r_GarageManager = new GarageManager();

        public void Run()
        {
            bool exitRequested = false;

            while (!exitRequested)
            {
                printMainMenu();
                string userChoice = Console.ReadLine();
                Console.Clear();

                switch (userChoice)
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

                if (exitRequested)
                {
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private static void printMainMenu()
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
            bool succeeded = false;

            string licensePlate = null;
            string selectedType = null;
            string modelName = null;
            float energyAmount = 0;
            string ownerName = null;
            string ownerPhone = null;
            Vehicle vehicle = null;

            try
            {
                if (checkVehicleNotExists(out licensePlate) && isLicensePlateValid(licensePlate))
                {
                    selectedType = getSelectedVehicleType();

                    if (selectedType != null)
                    {
                        modelName = getModelName();
                        energyAmount = getEnergyAmount();

                        vehicle = createAndInitializeVehicle(selectedType, licensePlate, modelName, energyAmount);
                        setWheels(vehicle.Wheels);

                        ownerName = getOwnerName();
                        ownerPhone = getOwnerPhone();

                        handleAdditionalQuestions(vehicle);
                        addVehicleToGarage(vehicle, ownerName, ownerPhone, licensePlate);

                        succeeded = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to insert vehicle: {ex.Message}");
            }

            if (succeeded)
            {
                Console.WriteLine("Vehicle inserted successfully.");
            }
        }

        private static string getSelectedVehicleType()
        {
            printVehicleTypes();

            return chooseVehicleTypeToInsert();
        }

        private static string getModelName()
        {
            Console.Write("Enter Model name: ");

            return Console.ReadLine();
        }

        private float getEnergyAmount()
        {
            Console.Write("Energy Amount: ");
            string input = Console.ReadLine();

            return float.Parse(input); // Let exception bubble up
        }

        private string getOwnerName()
        {
            Console.Write("Enter owner's name: ");

            return Console.ReadLine();
        }

        private string getOwnerPhone()
        {
            Console.Write("Enter owner's phone number: ");

            return Console.ReadLine();
        }

        private Vehicle createAndInitializeVehicle(string i_Type, string i_License, string i_Model, float i_Energy)
        {
            Vehicle vehicle = VehicleCreator.CreateVehicle(i_Type, i_License, i_Model);
            vehicle.Engine.CurrentEnergyAmount = i_Energy;
            vehicle.Engine.EnergyPercentage = vehicle.Engine.ConvertAmountToPercentage(i_Energy);
            return vehicle;
        }

        private void addVehicleToGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhone, string i_License)
        {
            VehicleInGarage vehicleInGarage = new VehicleInGarage(i_OwnerName, i_OwnerPhone, eGarageVehicleStatus.InRepair, i_Vehicle);
            r_GarageManager.m_VehiclesInGarage.Add(i_License, vehicleInGarage);
        }

        private void handleAdditionalQuestions(Vehicle i_Vehicle)
        {
            List<(string Question, string[] options)> questionsAndOptions = i_Vehicle.GetAddAdditionalQuestionsAndAnswerOptions();
            string[] answers = new string[questionsAndOptions.Count];
            int index = 0;

            foreach ((string question, string[] options) in questionsAndOptions)
            {
                Console.WriteLine(question);

                if (options != null)
                {
                    for (int i = 0; i < options.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {options[i]}");
                    }
                }

                answers[index] = Console.ReadLine();

                try
                {
                    i_Vehicle.ValidateAnswersAndSetValues(answers, index);
                }
                catch (Exception ex)
                {
                    throw new FormatException($"Error setting additional info: {ex.Message}");
                }

                index++;
            }
        }

        private static string chooseVehicleTypeToInsert()
        {
            List<string> supportedTypes = VehicleCreator.SupportedTypes;
            string selectedType = null;

            if (!int.TryParse(Console.ReadLine(), out int typeIndex) || typeIndex < 1 || typeIndex > supportedTypes.Count)
            {
                Console.WriteLine("Invalid vehicle type selection.");
            }
            else
            {
                selectedType = supportedTypes[typeIndex - 1];
            }

            return selectedType;
        }

        private static void printVehicleTypes()
        {
            Console.WriteLine("Select vehicle type:");
            List<string> supportedTypes = VehicleCreator.SupportedTypes;

            for (int i = 0; i < supportedTypes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {supportedTypes[i]}");
            }
        }

        private void setWheels(List<Wheel> i_IoWheels)
        {
            string choice = getUserChoiceForWheelUpdate();

            if (choice == null)
            {
                throw new ArgumentNullException("choice", "User input cannot be null. Wheels were not updated.");
            }

            if (choice != "Y" && choice != "N")
            {
                throw new ArgumentException("Invalid choice. Please enter 'Y' or 'N'. Wheels were not updated.");
            }

            switch (choice)
            {
                case "Y":
                    updateAllWheelsTogether(i_IoWheels);
                    Console.WriteLine("All wheels were updated successfully.");
                    break;
                case "N":
                    updateWheelsIndividually(i_IoWheels);
                    Console.WriteLine("Wheels were updated individually successfully.");
                    break;
            }

            Console.WriteLine();
        }

        private static string getUserChoiceForWheelUpdate()
        {
            Console.Write("Would you like to set all the wheels at once? (Y/N): ");
            string input = Console.ReadLine();

            string result = input == null ? null : input.Trim().ToUpper();

            return result;
        }

        private void updateAllWheelsTogether(List<Wheel> i_IoWheels)
        {
            getManufacturerAndCurrentAirPressureFromUser(out string manufacturer, out float pressure);

            foreach (Wheel wheel in i_IoWheels)
            {
                wheel.Manufacturer = manufacturer;
                wheel.CurrentAirPressure = pressure;
            }
        }

        private void updateWheelsIndividually(List<Wheel> i_IoWheels)
        {
            for (int i = 0; i < i_IoWheels.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"=== Wheel #{i + 1} ===");

                getManufacturerAndCurrentAirPressureFromUser(out string manufacturer, out float pressure);

                i_IoWheels[i].Manufacturer = manufacturer;
                i_IoWheels[i].CurrentAirPressure = pressure;
            }

            Console.WriteLine();
        }

        private static void getManufacturerAndCurrentAirPressureFromUser(out string i_OManufacturer, out float i_OCurrentAirPressure)
        {
            Console.Write("Enter wheel manufacturer name: ");
            i_OManufacturer = Console.ReadLine();

            while (true)
            {
                Console.Write("Enter current air pressure: ");
                string input = Console.ReadLine();

                if (float.TryParse(input, out i_OCurrentAirPressure))
                {
                    break;
                }

                Console.WriteLine("Invalid input. Please enter a numeric value.");
            }
        }

        private static bool isLicensePlateValid(string i_LicensePlateRaw)
        {
            bool isValid = false;

            if (!string.IsNullOrWhiteSpace(i_LicensePlateRaw))
            {
                string[] parts = i_LicensePlateRaw.Split('-');

                if (parts.Length == 3)
                {
                    bool allDigits = true;

                    foreach (string part in parts)
                    {
                        foreach (char c in part)
                        {
                            if (!char.IsDigit(c))
                            {
                                allDigits = false;
                                break;
                            }
                        }

                        if (!allDigits)
                        {
                            break;
                        }
                    }

                    if (allDigits)
                    {
                        if ((parts[0].Length == 2 && parts[1].Length == 3 && parts[2].Length == 2) ||
                            (parts[0].Length == 3 && parts[1].Length == 2 && parts[2].Length == 3))
                        {
                            isValid = true;
                        }
                    }
                }
            }

            if (!isValid)
            {
                Console.WriteLine("Invalid license plate format");
            }

            return isValid;
        }

        private void showLicenseNumbers()
        {
            if (r_GarageManager.m_VehiclesInGarage.Count == 0)
            {
                Console.WriteLine("Garage is empty.");
                return;
            }

            Console.WriteLine("License plates currently in garage:");

            foreach (string licensePlate in r_GarageManager.m_VehiclesInGarage.Keys)
            {
                Console.WriteLine($" - {licensePlate}");
            }
        }

        private void changeVehicleStatus()
        {
            string message;

            if (checkVehicleExists(out VehicleInGarage vehicleInGarage))
            {
                string licensePlate = vehicleInGarage.Vehicle.LicenseNumber;
                Console.WriteLine("Select new status:");

                foreach (eGarageVehicleStatus status in Enum.GetValues(typeof(eGarageVehicleStatus)))
                {
                    Console.WriteLine($"{(int)status}. {status}");
                }

                if (!int.TryParse(Console.ReadLine(), out int statusIndex) || !Enum.IsDefined(typeof(eGarageVehicleStatus), statusIndex))
                {
                    Console.WriteLine("Invalid status selection.");
                }
                else
                {
                    r_GarageManager.m_VehiclesInGarage[licensePlate].Status = (eGarageVehicleStatus)statusIndex;
                    Console.WriteLine("Vehicle status updated successfully.");
                }
            }
        }

        private void inflateWheelsToMax()
        {
            if (checkVehicleExists(out VehicleInGarage vehicleInGarage))
            {
                foreach (Wheel wheel in vehicleInGarage.Vehicle.Wheels)
                {
                    wheel.InflateToMax();
                }

                Console.WriteLine("All wheels inflated to maximum successfully.");
            }
        }

        private void refuelVehicle()
        {
            if (checkVehicleExists(out VehicleInGarage vehicleInGarage))
            {
                string message;
                Vehicle vehicle = vehicleInGarage.Vehicle;

                if (vehicle.Engine.EngineType != Engine.eEngineType.Fuel)
                {
                    message = "This vehicle is not powered by fuel.";
                }
                else
                {
                    // User chooses fuel type
                    Console.WriteLine("Select fuel type:");
                    foreach (FuelEngine.eFuelType type in Enum.GetValues(typeof(FuelEngine.eFuelType)))
                    {
                        Console.WriteLine($"{(int)type}. {type}");
                    }

                    if (!int.TryParse(Console.ReadLine(), out int fuelTypeIndex) || !Enum.IsDefined(typeof(FuelEngine.eFuelType), fuelTypeIndex))
                    {
                        message = "Invalid fuel type selection.";
                    }
                    else
                    {
                        try
                        {
                            // Check if the vehicle's fuel type matches the selected type
                            FuelEngine fuelEngine = vehicle.Engine as FuelEngine;

                            if ((FuelEngine.eFuelType)fuelTypeIndex != fuelEngine.FuelType)
                            {
                                throw new ArgumentException("Selected fuel type does not match vehicle's fuel type.");
                            }

                            // User enters amount of fuel to add
                            Console.Write("Enter amount of fuel to add (in liters): ");
                            if (!float.TryParse(Console.ReadLine(), out float amountToAdd))
                            {
                                message = "Invalid fuel amount.";
                            }

                            vehicleInGarage.Vehicle.AddEnergy(amountToAdd);
                            message = "Vehicle refueled successfully.";
                        }
                        catch (Exception ex)
                        {
                            message = $"Failed to refuel vehicle: {ex.Message}";
                        }
                    }
                }

                Console.WriteLine(message);
            }
        }

        private void chargeVehicle()
        {
            if (checkVehicleExists(out VehicleInGarage vehicleInGarage))
            {
                string message;
                Vehicle vehicle = vehicleInGarage.Vehicle;

                // Check if the vehicle is rechargeable
                if (vehicle.Engine.EngineType != Engine.eEngineType.Electric)
                {
                    message = "This vehicle is not powered by Electric.";
                }
                else
                {
                    // User enters number of hours to charge
                    Console.Write("Enter number of hours to charge (can be decimal): ");
                    if (!float.TryParse(Console.ReadLine(), out float hoursToCharge))
                    {
                        message = "Invalid number of hours.";
                    }
                    else
                    {
                        try
                        {
                            vehicle.AddEnergy(hoursToCharge);
                            message = "Vehicle charged successfully.";
                        }
                        catch (Exception ex)
                        {
                            message = $"Failed to charge vehicle: {ex.Message}";
                        }
                    }
                }

                Console.WriteLine(message);
            }
        }

        private bool checkVehicleExists(out VehicleInGarage i_OVehicleInGarage)
        {
            bool exist = true;

            Console.Write("Enter license plate (XX-XXX-XX) or (XXX-XX-XXX): ");
            string licensePlate = Console.ReadLine();

            if (!r_GarageManager.m_VehiclesInGarage.TryGetValue(licensePlate, out i_OVehicleInGarage))
            {
                Console.WriteLine("No such vehicle found in the garage.");
                exist = false;
            }

            return exist;
        }

        private bool checkVehicleNotExists(out string i_OLicensePlate)
        {
            bool exist = true;

            Console.Write("Enter license plate (XX-XXX-XX) or (XXX-XX-XXX): ");
            i_OLicensePlate = Console.ReadLine();

            if (r_GarageManager.m_VehiclesInGarage.ContainsKey(i_OLicensePlate))
            {
                Console.WriteLine("Vehicle already exist in the garage.");
                exist = false;
            }

            return exist;
        }

        private void showVehicleDetails()
        {
            if (!checkVehicleExists(out VehicleInGarage vehicleInGarage))
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine("=== Vehicle Details ===");
            Console.WriteLine(vehicleInGarage);
        }
    }
}
