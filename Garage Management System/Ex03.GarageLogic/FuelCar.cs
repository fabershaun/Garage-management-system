using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.FuelEngine;

namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        private const float k_MaxFuel = 48F;
        private const eFuelType k_FuelType = eFuelType.Octan95;

        internal FuelCar(string i_LicenseNumber, string i_ModelName) : base(i_LicenseNumber, i_ModelName) 
        {
            m_Engine = new FuelEngine(k_MaxFuel, k_FuelType);
        }
    }
}
 