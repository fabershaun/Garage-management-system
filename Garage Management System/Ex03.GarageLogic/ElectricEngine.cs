using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    internal class ElectricEngine
    {
        private float m_BatteryHoursLeft;
        private readonly float r_BatteryMaxHours;


        public ElectricEngine(float i_BatteryMaxHours)
        {
            r_BatteryMaxHours = i_BatteryMaxHours;
        }

        public float CurrentHours
        {
            get { return m_BatteryHoursLeft; }
            
            set
            {
                if (value < 0 || value > r_BatteryMaxHours)
                {
                    throw new ValueRangeException(value, 0, r_BatteryMaxHours);
                }

                m_BatteryHoursLeft = value;
            }
        }

        public float MaxBatteryHours
        {
            get { return r_BatteryMaxHours; }
        }

        internal void ChargeBattery(float i_HoursToAdd)
        {
            if (i_HoursToAdd < 0)
            {
                throw new ValueRangeException(i_HoursToAdd, 0, r_BatteryMaxHours);
            }

            float projectedBatteryHours = m_BatteryHoursLeft + i_HoursToAdd;

            if (projectedBatteryHours > r_BatteryMaxHours)
            {
                throw new ValueRangeException(projectedBatteryHours, 0, r_BatteryMaxHours);
            }

            m_BatteryHoursLeft = projectedBatteryHours;
        }

        public override string ToString()
        {
            StringBuilder engineInfo = new StringBuilder();

            engineInfo.AppendLine("-----\tENGINE\t-----");
            engineInfo.AppendLine($"Engine Type:\tElectric");
            engineInfo.AppendLine($"Current Fuel Amount:\t{CurrentHours}");
            engineInfo.AppendLine($"Max Fuel Amount:\t{MaxBatteryHours}");
            engineInfo.AppendLine();

            return engineInfo.ToString();
        }
    }
}
