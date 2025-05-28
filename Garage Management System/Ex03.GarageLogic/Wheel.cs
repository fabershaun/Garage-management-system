using System;

namespace Ex03.GarageLogic
{
    public sealed class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure = 0;
        internal float MaxAirPressure { get; }

        public Wheel(float i_MaxAirPressure)
        {
            if(i_MaxAirPressure <= 0)
            {
                throw new ArgumentException("Maximum air pressure must be positive", "i_MaxAirPressure");
            }

            MaxAirPressure = i_MaxAirPressure;
        }


        public void Inflate(float i_AirPressureToAdd)
        {
            if (i_AirPressureToAdd < 0)
            {
                throw new ValueRangeException(i_AirPressureToAdd, 0, MaxAirPressure);
            }

            float newAirPressure = m_CurrentAirPressure + i_AirPressureToAdd;

            if (newAirPressure > MaxAirPressure)
            {
                throw new ValueRangeException(newAirPressure, 0, MaxAirPressure);
            }

            m_CurrentAirPressure = newAirPressure;
        }

        public void InflateToMax()
        {
            m_CurrentAirPressure = MaxAirPressure;
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
                if(value < 0 || value > MaxAirPressure)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(value),
                        $"Current air pressure must be between 0 and {MaxAirPressure}");
                }

                m_CurrentAirPressure = value;
            }
        }
    }
}