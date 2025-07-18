﻿using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.FuelEngine;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const int k_MaxAirPressureOfTruck = 27;
        private const int k_NumOfTruckWheels = 12;
        private const int k_NumOfAdditionalOptions = 2;
        private const float k_MaxFuel = 135F;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private float m_CargoVolume;

        internal Truck(string i_LicenseNumber, string i_ModelName) : base(i_LicenseNumber, i_ModelName)
        {
            m_Engine = new FuelEngine(k_MaxFuel, k_FuelType);

            List<Wheel> wheels = new List<Wheel>();

            for (int i = 0; i < k_NumOfTruckWheels; i++)
            {
                wheels.Add(new Wheel(k_MaxAirPressureOfTruck));
            }

            Wheels = wheels;
        }

        internal enum eCarriesHazardousMaterials
        {
            Yes = 1,
            No,
        }

        internal bool CarriesHazardousMaterials { get; set; }

        internal float CargoVolume
        {
            get { return m_CargoVolume; }

            set
            {
                if (value < 0)
                {
                    throw new ValueRangeException(value, 0, float.MaxValue);
                }

                CargoVolume = value;
            }
        }

        public override void SetAdditionalInfo(string i_AdditionalInfo1, string i_AdditionalInfo2)
        {
            CarriesHazardousMaterials = bool.Parse(i_AdditionalInfo1);
            m_CargoVolume = float.Parse(i_AdditionalInfo2);
        }

        public override List<(string Question, string[] options)> GetAddAdditionalQuestionsAndAnswerOptions()
        {
            return new List<(string, string[])>
                       {
                           ("Enter if carries hazardous materials:", Enum.GetNames(typeof(eCarriesHazardousMaterials))),
                           ("Enter Cargo Volume", Enum.GetNames(typeof(eNumOfDoors)))
                       };
        }

        public override void ValidateAnswersAndSetValues(string[] i_Answers, int i_Index)
        {
            if (i_Answers.Length != k_NumOfAdditionalOptions)
            {
                throw new ArgumentException($"Truck expects exactly {k_NumOfAdditionalOptions} additional answers.");
            }

            if (i_Index == 0)
            {
                // Hazardous materials
                if (i_Answers[0] != "1" && i_Answers[0] != "2")
                {
                    throw new FormatException("Invalid hazardous materials answer.");
                }

                CarriesHazardousMaterials = i_Answers[0] == "1";
            }
            else if (i_Index == 1)
            {
                // Cargo volume
                if (!float.TryParse(i_Answers[i_Index], out float cargoVolume))
                {
                    throw new FormatException("Invalid cargo volume.");
                }

                CargoVolume = cargoVolume;
            }
        }

        public override string GetAdditionalInfo()
        {
            StringBuilder engineInfo = new StringBuilder();

            engineInfo.AppendLine("-----\tADDITIONAL INFO\t-----");
            engineInfo.AppendLine($"Carries Hazardous Materials: {CarriesHazardousMaterials}");
            engineInfo.AppendLine($"Cargo Volume: {CargoVolume}");
            engineInfo.AppendLine();

            return engineInfo.ToString();
        }
    }
}
