using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricEngine
    {
        private float m_BatteryHoursLeft;
        private readonly float r_BatteryMaxHours;

        public ElectricEngine(float i_BatteryHoursLeft, float i_BatteryMaxHours)
        {
            if (i_BatteryHoursLeft < 0 || i_BatteryHoursLeft > i_BatteryMaxHours)
            {
                throw new ValueRangeException(nameof(i_BatteryHoursLeft), $"Current hours must be between 0 and {i_BatteryMaxHours}. Provided value: {i_BatteryHoursLeft}"); //UI
            }
            m_BatteryHoursLeft = i_BatteryHoursLeft;
            r_BatteryMaxHours = i_BatteryMaxHours;
        }

        public float CurrentHours
        {
            get
            {
                return m_BatteryHoursLeft;
            }
        }

        public float MaxHours
        {
            get
            {
                return r_BatteryMaxHours;
            }
        }

        internal void ChargeBattery(float i_HoursToAdd)
        {
            if (i_HoursToAdd < 0)
            {
                throw new ArgumentException("Hours to add cannot be negative", nameof(i_HoursToAdd)); //UI
            }

            float projectedBatteryHours = m_BatteryHoursLeft + i_HoursToAdd;

            if (projectedBatteryHours > r_BatteryMaxHours)
            {
                throw new ValueRangeException(nameof(i_HoursToAdd), $"Charging hours cannot exceed maximum hours ({r_BatteryMaxHours}). Attempted to add: {i_HoursToAdd}");
            }

            m_BatteryHoursLeft = projectedBatteryHours;
        }
    }
}
