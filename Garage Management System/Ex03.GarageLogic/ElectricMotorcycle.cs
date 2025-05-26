using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle
    {
        private const float k_BatteryMaxHours = 3.2F;
        internal ElectricEngine m_Engine;

        internal ElectricMotorcycle(string i_LicenseNumber, string i_ModelName) : base(i_LicenseNumber, i_ModelName)
        {
            m_Engine = new ElectricEngine(k_BatteryMaxHours);
            m_IsElectricType = true;
            m_IsFuelType = false;
        }

        public override void SetEnergyPercentage(float i_EnergyAmount)
        {
            m_EnergyPercentage = i_EnergyAmount * 100.0f / k_BatteryMaxHours;
        }

        public override void SetEnergyAmountByPercentage(float i_EnergyPercentage)
        {
            m_Engine.CurrentHours = i_EnergyPercentage / 100.0f * k_BatteryMaxHours;
        }

        public override void SetEnergyAmountByAmount(float i_EnergyAmount)
        {
            m_Engine.CurrentHours = i_EnergyAmount;
        }
    }
}
