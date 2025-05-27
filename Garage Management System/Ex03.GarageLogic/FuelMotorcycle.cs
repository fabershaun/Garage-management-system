using System;
using static Ex03.GarageLogic.FuelEngine;

namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        private const float k_MaxFuel = 5.8F;
        private const eFuelType k_FuelType = eFuelType.Octan98;

        internal FuelMotorcycle(string i_LicenseNumber, string i_ModelName) : base(i_LicenseNumber, i_ModelName)
        {
            m_Engine = new FuelEngine(k_MaxFuel, k_FuelType);
        }
    }
}
