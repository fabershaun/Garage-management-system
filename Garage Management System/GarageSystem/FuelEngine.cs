using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class FuelEngine : Engine
    {
        internal enum eFuelType
        {
            Soler,
            Octan95,
            Octan96,
            Octan98,
        }

        private readonly eFuelType r_FuelType;

        public FuelEngine(float i_CurrentAmount, float i_MaxAmount, eFuelType i_FuelType)
        : base(i_CurrentAmount, i_MaxAmount)
        {
            r_FuelType = i_FuelType;
        }

        internal eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }
    }
}
