using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class FuelEngine
    {
        private float m_CurrentFuelAmount;
        private readonly float r_MaxFuelAmount;
        private readonly eFuelType r_FuelType;

        internal enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98,
        }

        public FuelEngine(float i_FuelAmount, float i_MaxFuelAmount, eFuelType i_FuelType)
        {
            if (i_FuelAmount < 0 || i_FuelAmount > r_MaxFuelAmount)
            {
                throw new ValueRangeException(nameof(i_FuelAmount), $"Current amount must be between 0 and {i_MaxFuelAmount}. Provided value: {i_FuelAmount}");
            }

            m_CurrentFuelAmount = i_FuelAmount;
            r_MaxFuelAmount = i_MaxFuelAmount;
            r_FuelType = i_FuelType;
        }

        internal eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        internal float CurrentAmount
        {
            get
            {
                return m_CurrentFuelAmount;
            }
        }

        internal void AddFuel(float i_AmountToAdd)
        {
            if (i_AmountToAdd < 0)
            {
                throw new ArgumentException("Amount to add cannot be negative", nameof(i_AmountToAdd));//UI
            }

            float newAmount = m_CurrentFuelAmount + i_AmountToAdd;

            if (newAmount > r_MaxFuelAmount)
            {
                throw new ValueRangeException(nameof(i_AmountToAdd), $"Adding {i_AmountToAdd} would exceed the maximum amount of {r_MaxFuelAmount}. Current amount: {m_CurrentAmount}");//UI
            }

            m_CurrentFuelAmount = newAmount;
        }

        internal float MaxAmount
        {
            get
            {
                return r_MaxFuelAmount;
            }
        }

    }
}
