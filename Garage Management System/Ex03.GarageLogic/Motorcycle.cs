using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        private readonly eLicenseType r_LicenseType;
        private readonly int r_engineDisplacementCc;

        internal Motorcycle(string i_LicenseID, string i_ModelName, float i_EnergyPercentage, List<Wheel> i_Wheels, eLicenseType i_LicenseType, int i_engineDisplacementCc)
            : base(i_LicenseID, i_ModelName, i_EnergyPercentage, i_Wheels)
        {
            r_LicenseType = i_LicenseType;
            r_engineDisplacementCc = i_engineDisplacementCc;
        }

        internal enum eLicenseType
        {
            A,
            A2,
            AB,
            B2,
        }
    }
}
