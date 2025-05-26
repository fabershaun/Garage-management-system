using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public sealed class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure = 0;
        private readonly float r_MaxAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            if(i_MaxAirPressure <= 0)
            {
                throw new ArgumentException("Maximum air pressure must be positive", "i_MaxAirPressure");
            }

            r_MaxAirPressure = i_MaxAirPressure;
        }

        public void Inflate(float i_AirPressureToAdd)
        {
            if (i_AirPressureToAdd < 0)
            {
                throw new ValueRangeException(i_AirPressureToAdd, 0, r_MaxAirPressure);
            }

            float newAirPressure = m_CurrentAirPressure + i_AirPressureToAdd;

            if (newAirPressure > r_MaxAirPressure)
            {
                throw new ValueRangeException(newAirPressure, 0, r_MaxAirPressure);
            }

            m_CurrentAirPressure = newAirPressure;
        }

        public void InflateToMax()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }


        public string Manufacturer
        {
            get
            {
                return m_ManufacturerName;

            }
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("value", "Manufacturer name cannot be null or empty");
                }

                m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                if(value < 0 || value > r_MaxAirPressure)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        $"Current air pressure must be between 0 and {r_MaxAirPressure}");
                }

                m_CurrentAirPressure = value;
            }
        }
    }
}