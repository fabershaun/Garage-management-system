using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        protected readonly string r_ModelName;
        protected readonly string r_LicenseNumber;
        private float m_EnergyPercentage;
        private List<Wheel> m_Wheels;

        protected Vehicle(string i_ModelName, string i_LicenseNumber, float i_EnergyPercentage, List<Wheel> i_Wheels)
        {
            if (string.IsNullOrEmpty(i_ModelName))
            {
                throw new ArgumentNullException(nameof(i_ModelName), "Model cannot be null or empty");
            }
            if (string.IsNullOrEmpty(i_LicenseNumber))
            {
                throw new ArgumentNullException(nameof(i_LicenseNumber), "License plate cannot be null or empty");
            }
            if (i_EnergyPercentage < 0 || i_EnergyPercentage > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(i_EnergyPercentage), "Energy percentage must be between 0 and 100");
            }
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
            m_EnergyPercentage = i_EnergyPercentage;
            m_Wheels = i_Wheels ?? throw new ArgumentNullException(nameof(i_Wheels), "Wheels cannot be null");
        }
    }


}
