using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.FuelEngine;
using static Ex03.GarageLogic.Motorcycle;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const int k_MaxAirPressureOfTruck = 27;
        private const int k_NumOfTruckWheels = 12;
        private const float k_MaxFuel = 135F;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private bool m_CarriesHazardousMaterials;
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

        internal bool CarriesHazardousMaterials
        {
            get { return m_CarriesHazardousMaterials; }
            set { m_CarriesHazardousMaterials = value; }
        }

        internal float CargoVolume
        {
            get { return m_CargoVolume; }
            
            set
            {
                if (value < 0)
                {
                    throw new ValueRangeException(value, 0, float.MaxValue);
                }

                m_CargoVolume = value;
            }
        }

        public override void SetAdditionalInfo(string i_AdditionalInfo1, string i_AdditionalInfo2)
        {
            m_CarriesHazardousMaterials = bool.Parse(i_AdditionalInfo1);
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

        public override void ValidateAnswersAndSetValues(string[] i_Answers)
        {
            if (i_Answers.Length != 2)
            {
                throw new ArgumentException("Truck expects exactly 2 additional answers.");
            }

            // Hazardous materials
            if (i_Answers[0] != "1" && i_Answers[0] != "2")
            {
                throw new FormatException("Invalid hazardous materials answer.");
            }

            m_CarriesHazardousMaterials = i_Answers[0] == "1";

            // Cargo volume
            if (!float.TryParse(i_Answers[1], out float cargoVolume))
            {
                throw new FormatException("Invalid cargo volume.");
            }
            m_CargoVolume = cargoVolume;
        }
    }
}
