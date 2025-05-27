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
            string licensePlateInput = Console.ReadLine();
            string licensePlate;

            try
            {
                licensePlate = formatLicensePlate(licensePlateInput);
            }
            catch(FormatException ex)
            {
                Console.WriteLine("Invalid license plate format: " + ex.Message);
                return;
            }
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
                float energyAmount = float.Parse(answers[1]);
                string wheelManufacturer = answers[2];
                float currentAirPressure = float.Parse(answers[3]);

                vehicle.SetEnergyPercentage(energyAmount);
                vehicle.SetEnergyAmountByAmount(energyAmount);

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

        private string formatLicensePlate(string i_LicensePlateRaw)
        {
            // Check for invalid characters
            foreach (char c in i_LicensePlateRaw)
            {
                if (!char.IsDigit(c) && c != '-')
                {
                    throw new FormatException("License plate contains invalid characters. Only digits and hyphens are allowed.");
                }
            }

            // If input already contains hyphens and is of acceptable length, assume it's valid
            if (i_LicensePlateRaw.Contains("-") && (i_LicensePlateRaw.Length == 9 || i_LicensePlateRaw.Length == 10))
            {
                return i_LicensePlateRaw;
            }

            // Remove all non-digit characters manually
            StringBuilder cleanedBuilder = new StringBuilder();
            foreach (char c in i_LicensePlateRaw)
            {
                if (char.IsDigit(c))
                {
                    cleanedBuilder.Append(c);
                }
            }
            string cleaned = cleanedBuilder.ToString();

            if (cleaned.Length == 7)
            {
                return cleaned.Substring(0, 2) + "-" + cleaned.Substring(2, 3) + "-" + cleaned.Substring(5, 2);
            }
            else if (cleaned.Length == 8)
            {
                return cleaned.Substring(0, 3) + "-" + cleaned.Substring(3, 3) + "-" + cleaned.Substring(6, 2);
            }
            else
            {
                throw new FormatException("License plate must contain 7 or 8 digits.");
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

            if (!r_GarageManager.m_VehiclesInGarage.ContainsKey(licensePlate))
            {
                Console.WriteLine("No such vehicle found in the garage.");
                return;
            }

            Console.WriteLine("Select new status:");
            foreach (Utils.eGarageVehicleStatus status in Enum.GetValues(typeof(Utils.eGarageVehicleStatus)))
            {
                Console.WriteLine($"{(int)status}. {status}");
            }

            if (!int.TryParse(Console.ReadLine(), out int statusIndex) || !Enum.IsDefined(typeof(Utils.eGarageVehicleStatus), statusIndex))
            {
                Console.WriteLine("Invalid status selection.");
                return;
            }

            r_GarageManager.m_VehiclesInGarage[licensePlate].Status = (Utils.eGarageVehicleStatus)statusIndex;
            Console.WriteLine("Vehicle status updated successfully.");
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
            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            if (!r_GarageManager.m_VehiclesInGarage.TryGetValue(licensePlate, out VehicleInGarage vehicleInGarage))
            {
                Console.WriteLine("No such vehicle found in the garage.");
                return;
            }

            IFuelable fuelableVehicle = vehicleInGarage.Vehicle as IFuelable;

            if (fuelableVehicle == null)
            {
                Console.WriteLine("This vehicle is not powered by fuel.");
                return;
            }

            Console.WriteLine("Select fuel type:");

            foreach (Utils.eFuelType type in Enum.GetValues(typeof(Utils.eFuelType)))
            {
                Console.WriteLine($"{(int)type}. {type}");
            }

            if (!int.TryParse(Console.ReadLine(), out int fuelTypeIndex) || !Enum.IsDefined(typeof(Utils.eFuelType), fuelTypeIndex))
            {
                Console.WriteLine("Invalid fuel type selection.");
                return;
            }

            Console.Write("Enter amount of fuel to add (in liters): ");
            if (!float.TryParse(Console.ReadLine(), out float amountToAdd))
            {
                Console.WriteLine("Invalid fuel amount.");
                return;
            }

            try
            {
                fuelableVehicle.Refuel((Utils.eFuelType)fuelTypeIndex, amountToAdd);
                Console.WriteLine("Vehicle refueled successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to refuel vehicle: {ex.Message}");
            }
        }

        private void chargeVehicle()
        {
            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            if (!r_GarageManager.m_VehiclesInGarage.TryGetValue(licensePlate, out VehicleInGarage vehicleInGarage))
            {
                Console.WriteLine("No such vehicle found in the garage.");
                return;
            }

            IRechargeable rechargeableVehicle = vehicleInGarage.Vehicle as IRechargeable;

            if (rechargeableVehicle == null)
            {
                Console.WriteLine("This vehicle is not electric.");
                return;
            }

            Console.Write("Enter number of hours to charge (can be decimal): ");
            if (!float.TryParse(Console.ReadLine(), out float hoursToCharge))
            {
                Console.WriteLine("Invalid number of hours.");
                return;
            }

            try
            {
                rechargeableVehicle.Recharge(hoursToCharge);
                Console.WriteLine("Vehicle charged successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to charge vehicle: {ex.Message}");
            }
        }

        private void showVehicleDetails()
        {
            Console.Write("Enter license plate: ");
            string licensePlate = Console.ReadLine();

            if (!r_GarageManager.m_VehiclesInGarage.TryGetValue(licensePlate, out VehicleInGarage vehicleInGarage))
            {
                Console.WriteLine("No such vehicle found in the garage.");
                return;
            }

            Console.WriteLine("\n=== Vehicle Details ===");
            Console.WriteLine(vehicleInGarage);
        }
    }
}
