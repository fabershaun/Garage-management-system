using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        private readonly string r_Model;
        private readonly string r_LicensePlate;
        private float m_EnergyPercentage;
        private List<Wheel> m_Wheels;
    }
}
