using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle, IFuelable
    {
        private const int k_MaxAirPressureOfTruck = 27;
        private const int k_NumOfTruckWheels = 12;
        private const float k_MaxFuel = 135F;
        private const Utils.eFuelType k_FuelType = Utils.eFuelType.Soler;
        internal FuelEngine m_Engine;

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

        public void Refuel(Utils.eFuelType i_FuelType, float i_Amount)
        {
            if (i_FuelType != k_FuelType)
            {
                throw new ArgumentException("Incorrect fuel type.");
            }

            if (i_Amount < 0)
            {
                throw new ValueRangeException(i_Amount, 0, m_Engine.MaxFuelAmount);
            }

            float newFuelAmount = m_Engine.CurrentFuelAmount + i_Amount;

            if (newFuelAmount > m_Engine.MaxFuelAmount)
            {
                throw new ValueRangeException(newFuelAmount, 0, m_Engine.MaxFuelAmount);
            }

            SetEnergyAmountByAmount(newFuelAmount);
            SetEnergyPercentage(newFuelAmount);
        }

        public override string GetEngineDescription()
        {
            return m_Engine.ToString();
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

        public override void SetEnergyPercentage(float i_EnergyAmount)
        {
            m_EnergyPercentage = i_EnergyAmount * 100.0f / k_MaxFuel;
        }

        public override void SetEnergyAmountByPercentage(float i_EnergyPercentage)
        {
            m_Engine.CurrentFuelAmount = i_EnergyPercentage / 100.0f * k_MaxFuel;
        }

        public override void SetEnergyAmountByAmount(float i_EnergyAmount)
        {
            m_Engine.CurrentFuelAmount = i_EnergyAmount;
        }
    }
}
