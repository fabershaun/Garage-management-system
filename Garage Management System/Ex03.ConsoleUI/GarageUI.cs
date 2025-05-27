using System;
using System.Collections.Generic;
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

                if(exitRequested)
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
            if(checkVehicleNotExists(out string licensePlate))  // Check if vehicle already exists and if not exist so continue
            {
                if(isLicensePlateValid(licensePlate))           // Check if license plate is valid
                {
                    printVehicleTypes(); // Print vehicle types list to choose from
                    string selectedType = chooseVehicleTypeToInsert();

                    if(selectedType != null)
                    {
                        Console.Write("Enter Model name: ");
                        string modelName = Console.ReadLine();


                        Console.Write("Energy Amount: ");
                        string energyAmountStr = Console.ReadLine();
                        try
                        {
                            float energyAmount = float.Parse(energyAmountStr);

                            Vehicle vehicle = VehicleCreator.CreateVehicle(selectedType, licensePlate, modelName);

                            vehicle.Engine.CurrentEnergyAmount = energyAmount;
                            vehicle.Engine.EnergyPercentage = vehicle.Engine.ConvertAmountToPercentage(energyAmount);
                            setWheels(vehicle.Wheels);
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
                }
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

        private void setWheels(List<Wheel> io_Wheels)
        {
            string message;
            string choice = getUserChoiceForWheelUpdate();

            if (choice == null)
            {
                message = "No input received. Wheels were not updated.";
            }
            else if (choice == "Y")
            {
                updateAllWheelsTogether(io_Wheels);
                message = "All wheels were updated successfully.";
            }
            else if (choice == "N")
            {
                updateWheelsIndividually(io_Wheels);
                message = "Wheels were updated individually.";
            }
            else
            {
                message = "Invalid choice. Please enter 'Y' or 'N'. Wheels were not updated.";
            }

            Console.WriteLine(message);
        }

        private string getUserChoiceForWheelUpdate()
        {
            string result;

            Console.Write("Would you like to set all the wheels at once? (Y/N): ");
            string input = Console.ReadLine();

            if (input == null)
            {
                result = null;
            }
            else
            {
                result = input.Trim().ToUpper();
            }

            return result;
        }

        private void updateAllWheelsTogether(List<Wheel> io_Wheels)
        {
            string manufacturer;
            float pressure;

            getManufacturerAndCurrentAirPressureFromUser(out manufacturer, out pressure);

            foreach (Wheel wheel in io_Wheels)
            {
                wheel.Manufacturer = manufacturer;
                wheel.CurrentAirPressure = pressure;
            }
        }

        private void updateWheelsIndividually(List<Wheel> io_Wheels)
        {
            for (int i = 0; i < io_Wheels.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"=== Wheel #{i + 1} ===");

                string manufacturer;
                float pressure;

                getManufacturerAndCurrentAirPressureFromUser(out manufacturer, out pressure);

                io_Wheels[i].Manufacturer = manufacturer;
                io_Wheels[i].CurrentAirPressure = pressure;
            }
        }

        private void getManufacturerAndCurrentAirPressureFromUser(out string o_Manufacturer, out float o_CurrentAirPressure)
        {
            Console.Write("Enter wheel manufacturer name: ");
            o_Manufacturer = Console.ReadLine();

            while (true)
            {
                Console.Write("Enter current air pressure: ");
                string input = Console.ReadLine();

                if (float.TryParse(input, out o_CurrentAirPressure))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a numeric value.");
                }
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

            if(!isValid)
            {
                Console.WriteLine("Invalid license plate format");
            }
            
            return isValid;
        }

        private void showLicenseNumbers()
        {
            Console.WriteLine("License plates currently in garage:");

            foreach (string licensePlate in r_GarageManager.m_VehiclesInGarage.Keys)
            {
                Console.WriteLine($" - {licensePlate}");
            }
        }

        private void changeVehicleStatus()
        {
            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            string message;

            if (!r_GarageManager.m_VehiclesInGarage.ContainsKey(licensePlate))
            {
                message = "No such vehicle found in the garage.";
            }
            else
            {
                Console.WriteLine("Select new status:");
                foreach (Utils.eGarageVehicleStatus status in Enum.GetValues(typeof(Utils.eGarageVehicleStatus)))
                {
                    Console.WriteLine($"{(int)status}. {status}");
                }

                if (!int.TryParse(Console.ReadLine(), out int statusIndex) || !Enum.IsDefined(typeof(Utils.eGarageVehicleStatus), statusIndex))
                {
                    message = "Invalid status selection.";
                }
                else
                {
                    r_GarageManager.m_VehiclesInGarage[licensePlate].Status = (Utils.eGarageVehicleStatus)statusIndex;
                    message = "Vehicle status updated successfully.";
                }
            }

            Console.WriteLine(message);
        }

        private void inflateWheelsToMax()
        {
            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            if (!r_GarageManager.m_VehiclesInGarage.TryGetValue(licensePlate, out VehicleInGarage vehicleInGarage))
            {
                Console.WriteLine("No such vehicle found in the garage.");
                return;
            }

            foreach (Wheel wheel in vehicleInGarage.Vehicle.Wheels)
            {
                wheel.InflateToMax();
            }

            Console.WriteLine("All wheels inflated to maximum.");
        }

        private void refuelVehicle()
        {
            string message;

            if(checkVehicleExists(out VehicleInGarage vehicleInGarage))
            {
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
            string message;

            if(checkVehicleExists(out VehicleInGarage vehicleInGarage))
            {
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

        private bool checkVehicleExists(out VehicleInGarage o_vehicleInGarage)
        {
            bool exist = true;

            Console.Write("Enter license plate (XX-XXX-XX) or (XXX-XX-XXX): ");
            string licensePlate = Console.ReadLine();

            if (!r_GarageManager.m_VehiclesInGarage.TryGetValue(licensePlate, out o_vehicleInGarage))
            {
                Console.WriteLine("No such vehicle found in the garage.");
                exist = false;
            }

            return exist;
        }

        private bool checkVehicleNotExists(out string o_licensePlate)
        {
            bool exist = true;

            Console.Write("Enter license plate (XX-XXX-XX) or (XXX-XX-XXX): ");
            o_licensePlate = Console.ReadLine();

            if (r_GarageManager.m_VehiclesInGarage.ContainsKey(o_licensePlate))
            {
                Console.WriteLine("Vehicle already exist in the garage.");
                exist = false;
            }

            return exist;
        }

        private void showVehicleDetails()
        {
            if(!checkVehicleExists(out VehicleInGarage vehicleInGarage))
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine("=== Vehicle Details ===");
            Console.WriteLine(vehicleInGarage);
        }
    }
}
