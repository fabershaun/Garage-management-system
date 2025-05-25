using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal sealed class Wheel
    {
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        private void inflate(float i_AirPressureToAdd)
        {
            float newAirPressure = m_CurrentAirPressure + i_AirPressureToAdd;

            if (newAirPressure > r_MaxAirPressure || newAirPressure < 0)
            {
                throw new ArgumentException($"Air pressure must be between 0 and {r_MaxAirPressure}. Resulting pressure would be {newAirPressure}");
            }

            m_CurrentAirPressure = newAirPressure;

            //Add exception of negative air pressure to add
        }


        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
        {
            if (string.IsNullOrEmpty(i_ManufacturerName))
            {
                throw new ArgumentNullException("i_ManufacturerName", "Manufacturer name cannot be null or empty");
            }

            if (i_CurrentAirPressure < 0)
            {
                throw new ArgumentException("Current air pressure cannot be negative", "i_CurrentAirPressure");
            }

            if (i_MaxAirPressure <= 0)
            {
                throw new ArgumentException("Maximum air pressure must be positive", "i_MaxAirPressure");
            }

            if (i_CurrentAirPressure > i_MaxAirPressure)
            {
                throw new ArgumentException($"Current air pressure ({i_CurrentAirPressure}) cannot exceed maximum air pressure ({i_MaxAirPressure})");
            }

            r_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = i_CurrentAirPressure;
            r_MaxAirPressure = i_MaxAirPressure;
        }

    }
}
