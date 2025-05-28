using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.ElectricEngine;

namespace Ex03.GarageLogic
{
    internal class ElectricCar : Car
    {
        private const float k_BatteryMaxHours = 4.8F;
        
        internal ElectricCar(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            m_Engine = new ElectricEngine(k_BatteryMaxHours);
        }
    }
}
