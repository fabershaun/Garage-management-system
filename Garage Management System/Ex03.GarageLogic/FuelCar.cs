using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.FuelEngine;

namespace Ex03.GarageLogic
{
    internal class FuelCar : Car
    {
        internal FuelEngine engine;

        internal FuelCar(string i_LicenseID, string i_ModelName, float i_EnergyPercentage, List<Wheel> i_Wheels, eCarColor i_Color, int i_NumOfDoors, float i_FuelAmount, float i_MaxFuelAmount, eFuelType i_FuelType)
            : base(i_LicenseID, i_ModelName, i_EnergyPercentage, i_Wheels, i_Color, i_NumOfDoors);
        {
            //
            
        }
    }
}
 