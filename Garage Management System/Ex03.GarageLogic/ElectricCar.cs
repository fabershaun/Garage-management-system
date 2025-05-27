using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.ElectricEngine;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car, IRechargeable
    {
        private const float k_BatteryMaxHours = 4.8F;
        internal ElectricEngine m_Engine;

        internal ElectricCar(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            m_Engine = new ElectricEngine(k_BatteryMaxHours);
        }

        public override string GetEngineDescription()
        {
            return m_Engine.ToString();
        }

        public void Recharge(float i_HoursToCharge)
        {
            if (i_HoursToCharge < 0)
            {
                throw new ValueRangeException(i_HoursToCharge, 0, m_Engine.MaxBatteryHours);
            }

            float newBatteryAmount = m_Engine.CurrentHours + i_HoursToCharge;

            if (newBatteryAmount > m_Engine.MaxBatteryHours)
            {
                throw new ValueRangeException(newBatteryAmount, 0, m_Engine.MaxBatteryHours);
            }
            
            SetEnergyAmountByAmount(newBatteryAmount);
            SetEnergyPercentage(newBatteryAmount);
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
