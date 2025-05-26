using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private const float k_MaxFuel = 135F;
        private const Utils.eFuelType k_DefaultFuel = Utils.eFuelType.Soler;
        internal FuelEngine m_Engine;

        private bool m_CarriesHazardousMaterials;
        private float m_CargoVolume;

        internal Truck(string i_LicenseNumber, string i_ModelName) : base(i_LicenseNumber, i_ModelName)
        {
            m_Engine = new FuelEngine(k_MaxFuel, k_DefaultFuel);
        }

        internal bool CarriesHazardousMaterials
        {
            get { return m_CarriesHazardousMaterials; }
            set { m_CarriesHazardousMaterials = value; }
        }

        internal float CargoVolume
        {
            get { return m_CargoVolume; }
            
            set
            {
                if (value < 0)
                {
                    throw new ValueRangeException(value, 0, float.MaxValue);
                }

                m_CargoVolume = value;
            }
        }
    }
}
