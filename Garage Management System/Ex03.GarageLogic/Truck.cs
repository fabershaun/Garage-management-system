using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.FuelEngine;

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

        public override void AddAdditionalQuestions(List<string> io_QuestionsList)
        {
            io_QuestionsList.Add("If carries hazardous materials type 'true' else type 'false'");
            io_QuestionsList.Add("Cargo volume");
        }
    }
}
