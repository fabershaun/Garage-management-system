using System;
using System.Collections.Generic;
using System.IO;
using static Ex03.GarageLogic.VehicleInGarage;

namespace Ex03.GarageLogic
{
    public class GarageManager
    {
        public Dictionary<string, VehicleInGarage> m_VehiclesInGarage = new Dictionary<string, VehicleInGarage>();
        
        public List<string> LoadVehiclesFromFile(string i_FileName)
        {
            List<string> errorMessages = new List<string>();
            string[] lines = File.ReadAllLines(i_FileName);

            foreach (string line in lines)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    string[] partsOfLine = line.Split(',');

                    if (!VehicleCreator.SupportedTypes.Contains(partsOfLine[0]))
                    {
                        continue;
                    }

                    if (partsOfLine.Length < 10)
                    {
                        errorMessages.Add($"Too few fields in line: {line}");
                        continue;
                    }

                    string vehicleType = partsOfLine[0];
                    string licensePlate = partsOfLine[1];
                    string modelName = partsOfLine[2];
                    float energyPercentage = float.Parse(partsOfLine[3]);
                    string wheelManufacturer = partsOfLine[4];
                    float wheelAirPressure = float.Parse(partsOfLine[5]);
                    string ownerName = partsOfLine[6];
                    string ownerPhoneNumber = partsOfLine[7];
                    string additionalInfo1 = partsOfLine[8];
                    string additionalInfo2 = partsOfLine[9];

                    Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleType, licensePlate, modelName);

                    vehicle.Engine.EnergyPercentage = energyPercentage;
                    vehicle.Engine.CurrentEnergyAmount = vehicle.Engine.MaxEnergyAmount * (energyPercentage / 100);

                    foreach (Wheel wheel in vehicle.Wheels)
                    {
                        wheel.Manufacturer = wheelManufacturer;
                        wheel.CurrentAirPressure = wheelAirPressure;
                    }

                    VehicleInGarage vehicleInGarage = new VehicleInGarage(
                        ownerName,
                        ownerPhoneNumber,
                        eGarageVehicleStatus.InRepair,
                        vehicle);

                    if (m_VehiclesInGarage.ContainsKey(licensePlate))
                    {
                        errorMessages.Add($"License plate already exists: {licensePlate}");
                    }
                    
                    vehicle.SetAdditionalInfo(additionalInfo1, additionalInfo2);
                    m_VehiclesInGarage.Add(licensePlate, vehicleInGarage);      // Add the vehicle to the garage
                }
                catch(Exception ex)
                {
                    errorMessages.Add($"Error processing line: {line} => {ex.Message}");
                }
            }

            return errorMessages;
        }
    }
}
