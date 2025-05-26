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
        private const int k_MaxAirPressureOfCar = 32;
        private const int k_NumOfCarWheels = 5;
        private eCarColor m_Color;
        private eNumOfDoors m_NumOfDoors;

        internal Car(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            List<Wheel> wheels = new List<Wheel>();

            for (int i = 0; i < k_NumOfCarWheels; i++)
            {
                wheels.Add(new Wheel(k_MaxAirPressureOfCar));
            }

            SetWheels(wheels);
        }

        internal eCarColor Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        internal eNumOfDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
        }

        public enum eCarColor
        {
            Yellow,
            Black,
            White,
            Silver,
        }

        public enum eNumOfDoors
        {
            Two,
            Three,
            Four,
            Five,
        }
    }
}
