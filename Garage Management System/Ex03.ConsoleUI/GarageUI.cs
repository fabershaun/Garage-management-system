using System;
using System.Collections.Generic;
using System.Text;
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

        //ADD TRY AND CATCH
        private void insertNewVehicle()
        {
            string message;

            Console.Write("Enter license plate: ");
            string licensePlateInput = Console.ReadLine();
            string licensePlate;

            try
            {
                licensePlate = formatLicensePlate(licensePlateInput);
            }
            catch (FormatException ex)
            {
                message = "Invalid license plate format: " + ex.Message;
                Console.WriteLine(message);
                return;
            }

            if (r_GarageManager.m_VehiclesInGarage.ContainsKey(licensePlate))
            {
                message = "Vehicle already exists in the garage.";
            }
            else
            {
                Console.WriteLine("Select vehicle type:");
                List<string> supportedTypes = VehicleCreator.SupportedTypes;

                for (int i = 0; i < supportedTypes.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {supportedTypes[i]}");
                }

                if (!int.TryParse(Console.ReadLine(), out int typeIndex) || typeIndex < 1 || typeIndex > supportedTypes.Count)
                {
                    message = "Invalid vehicle type selection.";
                }
                else
                {
                    string selectedType = supportedTypes[typeIndex - 1];

                    Console.Write("Enter Model name: ");
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
                        float energyAmount = float.Parse(answers[1]);
                        string wheelManufacturer = answers[2];
                        float currentAirPressure = float.Parse(answers[3]);

                        vehicle.SetEnergyPercentage(energyAmount);
                        vehicle.SetEnergyAmountByAmount(energyAmount);

                        foreach (Wheel wheel in vehicle.Wheels)
                        {
                            wheel.Manufacturer = wheelManufacturer;
                            wheel.CurrentAirPressure = currentAirPressure;
                        }

                        vehicle.SetAdditionalInfo(answers[4], answers[5]);

                        Console.Write("Enter owner's name: ");
                        string ownerName = Console.ReadLine();
                        Console.Write("Enter owner's phone number: ");
                        string ownerPhone = Console.ReadLine();

                        VehicleInGarage vehicleInGarage = new VehicleInGarage(ownerName, ownerPhone, Utils.eGarageVehicleStatus.InRepair, vehicle);
                        r_GarageManager.m_VehiclesInGarage.Add(licensePlate, vehicleInGarage);

                        message = "Vehicle inserted successfully.";
                    }
                    catch (Exception ex)
                    {
                        message = $"Failed to insert vehicle: {ex.Message}";
                    }
                }
            }

            Console.WriteLine(message);
        }
        /*        private void insertNewVehicle()
                {
                    string message;

                    Console.Write("Enter license plate: ");
                    string licensePlateInput = Console.ReadLine();
                    string licensePlate;

                    try
                    {
                        licensePlate = formatLicensePlate(licensePlateInput);
                    }
                    catch (FormatException ex)
                    {
                        message = "Invalid license plate format: " + ex.Message;
                        Console.WriteLine(message);
                        return;
                    }

                    if (r_GarageManager.m_VehiclesInGarage.ContainsKey(licensePlate))
                    {
                        message = "Vehicle already exists in the garage.";
                    }
                    else
                    {
                        Console.WriteLine("Select vehicle type:");
                        List<string> supportedTypes = VehicleCreator.SupportedTypes;

                        for (int i = 0; i < supportedTypes.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {supportedTypes[i]}");
                        }

                        if (!int.TryParse(Console.ReadLine(), out int typeIndex) || typeIndex < 1 || typeIndex > supportedTypes.Count)
                        {
                            message = "Invalid vehicle type selection.";
                        }
                        else
                        {
                            string selectedType = supportedTypes[typeIndex - 1];

                            Console.Write("Enter Model name: ");
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
                                float energyAmount = float.Parse(answers[1]);
                                string wheelManufacturer = answers[2];
                                float currentAirPressure = float.Parse(answers[3]);

                                vehicle.SetEnergyPercentage(energyAmount);
                                vehicle.SetEnergyAmountByAmount(energyAmount);

                                foreach (Wheel wheel in vehicle.Wheels)
                                {
                                    wheel.Manufacturer = wheelManufacturer;
                                    wheel.CurrentAirPressure = currentAirPressure;
                                }

                                vehicle.SetAdditionalInfo(answers[4], answers[5]);

                                Console.Write("Enter owner's name: ");
                                string ownerName = Console.ReadLine();
                                Console.Write("Enter owner's phone number: ");
                                string ownerPhone = Console.ReadLine();

                                VehicleInGarage vehicleInGarage = new VehicleInGarage(ownerName, ownerPhone, Utils.eGarageVehicleStatus.InRepair, vehicle);
                                r_GarageManager.m_VehiclesInGarage.Add(licensePlate, vehicleInGarage);

                                message = "Vehicle inserted successfully.";
                            }
                            catch (Exception ex)
                            {
                                message = $"Failed to insert vehicle: {ex.Message}";
                            }
                        }
                    }

                    Console.WriteLine(message);
                }*/

        private static string formatLicensePlate(string i_LicensePlateRaw)
        {
            string formattedPlate;

            bool isValid = true;
            foreach(char c in i_LicensePlateRaw)
            {
                if (!char.IsDigit(c) && c != '-')
                {
                    isValid = false;
                    break;
                }
            }

            if (!isValid)
            {
                formattedPlate = null;
            }
            else if (i_LicensePlateRaw.Contains("-") && (i_LicensePlateRaw.Length == 9 || i_LicensePlateRaw.Length == 10))
            {
                formattedPlate = i_LicensePlateRaw;
            }
            else
            {
                StringBuilder cleanedBuilder = new StringBuilder();
                foreach(char c in i_LicensePlateRaw)
                {
                    if (char.IsDigit(c))
                    {
                        cleanedBuilder.Append(c);
                    }
                }

                string cleaned = cleanedBuilder.ToString();

                switch(cleaned.Length)
                {
                    case 7:
                        formattedPlate = cleaned.Substring(0, 2) + "-" + cleaned.Substring(2, 3) + "-" + cleaned.Substring(5, 2);
                        break;
                    case 8:
                        formattedPlate = cleaned.Substring(0, 3) + "-" + cleaned.Substring(3, 3) + "-" + cleaned.Substring(6, 2);
                        break;
                    default:
                        formattedPlate = null;
                        break;
                }
            }

            if (formattedPlate == null)
            {
                throw new FormatException("License plate is invalid. Use 7 or 8 digits, digits and hyphens only.");
            }

            return formattedPlate;
        }

        private List<string> getQuestionsForType(Vehicle i_Vehicle)
        {
            List<string> questionForType = new List<string>
            {
                "Model name",
                "Energy percentage",
                "Wheel manufacturer",
                "Current wheel air pressure"
            };
            i_Vehicle.AddAdditionalQuestions(questionForType);

            return questionForType;
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

            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            if (!r_GarageManager.m_VehiclesInGarage.TryGetValue(licensePlate, out VehicleInGarage vehicleInGarage))
            {
                message = "No such vehicle found in the garage.";
            }
            else
            {
                if (!(vehicleInGarage.Vehicle is IFuelable fuelableVehicle))
                {
                    message = "This vehicle is not powered by fuel.";
                }
                else
                {
                    Console.WriteLine("Select fuel type:");
                    foreach (Utils.eFuelType type in Enum.GetValues(typeof(Utils.eFuelType)))
                    {
                        Console.WriteLine($"{(int)type}. {type}");
                    }

                    if (!int.TryParse(Console.ReadLine(), out int fuelTypeIndex) || !Enum.IsDefined(typeof(Utils.eFuelType), fuelTypeIndex))
                    {
                        message = "Invalid fuel type selection.";
                    }
                    else
                    {
                        Console.Write("Enter amount of fuel to add (in liters): ");
                        if (!float.TryParse(Console.ReadLine(), out float amountToAdd))
                        {
                            message = "Invalid fuel amount.";
                        }
                        else
                        {
                            try
                            {
                                fuelableVehicle.Refuel((Utils.eFuelType)fuelTypeIndex, amountToAdd);
                                message = "Vehicle refueled successfully.";
                            }
                            catch (Exception ex)
                            {
                                message = $"Failed to refuel vehicle: {ex.Message}";
                            }
                        }
                    }
                }
            }

            Console.WriteLine(message);
        }

        private void chargeVehicle()
        {
            string message;

            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            if (!r_GarageManager.m_VehiclesInGarage.TryGetValue(licensePlate, out VehicleInGarage vehicleInGarage))
            {
                message = "No such vehicle found in the garage.";
            }
            else
            {
                if (!(vehicleInGarage.Vehicle is IRechargeable rechargeableVehicle))
                {
                    message = "This vehicle is not electric.";
                }
                else
                {
                    Console.Write("Enter number of hours to charge (can be decimal): ");
                    if (!float.TryParse(Console.ReadLine(), out float hoursToCharge))
                    {
                        message = "Invalid number of hours.";
                    }
                    else
                    {
                        try
                        {
                            rechargeableVehicle.Recharge(hoursToCharge);
                            message = "Vehicle charged successfully.";
                        }
                        catch (Exception ex)
                        {
                            message = $"Failed to charge vehicle: {ex.Message}";
                        }
                    }
                }
            }

            Console.WriteLine(message);
        }

        private void showVehicleDetails()
        {
            Console.Write("Enter license plate (XX-XXX-XX): ");
            string licensePlate = Console.ReadLine();

            if (!r_GarageManager.m_VehiclesInGarage.TryGetValue(licensePlate, out VehicleInGarage vehicleInGarage))
            {
                Console.WriteLine("No such vehicle found in the garage.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("=== Vehicle Details ===");
            Console.WriteLine(vehicleInGarage);
        }
    }
}
