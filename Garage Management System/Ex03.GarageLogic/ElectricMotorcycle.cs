using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle
    {
        private const float k_BatteryMaxHours = 3.2F;

        internal ElectricMotorcycle(string i_LicenseNumber, string i_ModelName) : base(i_LicenseNumber, i_ModelName)
        {
            m_Engine = new ElectricEngine(k_BatteryMaxHours);
        }
    }
}
