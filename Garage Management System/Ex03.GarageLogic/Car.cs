using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.GarageLogic
{
    internal abstract class Car : Vehicle
    {
        private readonly eCarColor r_Color;
        private readonly int r_NumOfDoors;

        internal Car(string i_LicenseID, string i_ModelName, float i_EnergyPercentage, List<Wheel> i_Wheels, eCarColor i_Color, int i_NumOfDoors) 
            : base(i_LicenseID, i_ModelName, i_EnergyPercentage, i_Wheels)
        {
            r_Color = i_Color;
            r_NumOfDoors = i_NumOfDoors;
        }

        internal enum eCarColor
        {
            Yellow,
            Black,
            White,
            Silver,
        }
    }
}

private float m_EnergyPercentage;
private List<Wheel> m_Wheels;