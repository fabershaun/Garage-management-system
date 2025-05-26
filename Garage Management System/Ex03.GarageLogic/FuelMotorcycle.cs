using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.FuelEngine;

namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        internal FuelEngine m_Engine;
        private const float k_MaxFuel = 5.8F;
        private const Utils.eFuelType k_FuelType = Utils.eFuelType.Octan98;

        internal FuelMotorcycle(string i_LicenseNumber, string i_ModelName) : base(i_LicenseNumber, i_ModelName)
        {
            m_Engine = new FuelEngine(k_MaxFuel, k_FuelType);
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

        public override void SetEnergyPercentage(float i_EnergyAmount)
        {
            m_EnergyPercentage = i_EnergyAmount * 100.0f / k_MaxFuel;
            m_IsFuelType = true;
            m_IsElectricType = false;
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
