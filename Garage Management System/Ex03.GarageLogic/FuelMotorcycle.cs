using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.FuelEngine;

namespace Ex03.GarageLogic
{
    internal class FuelMotorcycle : Motorcycle
    {
        internal FuelEngine m_Engine;
        private const float k_MaxFuel = 5.8F;
        private const Utils.eFuelType k_FuelType = Utils.eFuelType.Octan98;

        internal FuelMotorcycle(string i_LicenseNumber, string i_ModelName) : base(i_LicenseNumber, i_ModelName)
        {
            m_Engine = new FuelEngine(k_MaxFuel, k_FuelType);
        }
    }
}
