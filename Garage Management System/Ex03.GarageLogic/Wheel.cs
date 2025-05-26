using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal sealed class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        private void inflate(float i_AirPressureToAdd)
        {
            float newAirPressure = m_CurrentAirPressure + i_AirPressureToAdd;

            if(newAirPressure > r_MaxAirPressure || newAirPressure < 0)
            {
                throw new ArgumentException(
                    $"Air pressure must be between 0 and {r_MaxAirPressure}. Resulting pressure would be {newAirPressure}");
            }

            m_CurrentAirPressure = newAirPressure;

            //Add exception of negative air pressure to add
        }

        internal Wheel(float i_MaxAirPressure)
        {
            if(i_MaxAirPressure <= 0)
            {
                throw new ArgumentException("Maximum air pressure must be positive", "i_MaxAirPressure");
            }

            r_MaxAirPressure = i_MaxAirPressure;
        }

        internal string Manufacturer
        {
            get
            {
                return m_ManufacturerName;

            }
            set
            {
                if(string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(value), "Manufacturer name cannot be null or empty");
                }

                m_ManufacturerName = value;
            }
        }

        internal float CurrentAirPressure
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





        /*        public Wheel(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure)
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

            }*/
    }
}