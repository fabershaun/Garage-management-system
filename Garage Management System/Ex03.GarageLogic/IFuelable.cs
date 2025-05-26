using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public interface IFuelable
    {
        void Refuel(Utils.eFuelType i_FuelType, float i_Amount);
    }
}
