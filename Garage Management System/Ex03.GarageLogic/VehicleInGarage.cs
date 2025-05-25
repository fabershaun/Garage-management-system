using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class VehicleInGarage
    {
        internal string m_OwnerName;
        internal string m_OwnerPhoneNumber;
        internal eVehicleStatus m_Status;
        internal Vehicle m_Vehicle;

        internal VehicleInGarage(string i_OwnerName, string i_OwnerPhoneNumber, eVehicleStatus i_Status, Vehicle i_Vehicle)
        {
            if (string.IsNullOrEmpty(i_OwnerName))
            {
                throw new ArgumentNullException("i_OwnerName", "Owner name cannot be null or empty");
            }
            if (string.IsNullOrEmpty(i_OwnerPhoneNumber))
            {
                throw new ArgumentNullException("i_OwnerPhoneNumber", "Owner phone number cannot be null or empty");
            }
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_Status = i_Status;
            m_Vehicle = i_Vehicle ?? throw new ArgumentNullException("i_Vehicle", "Vehicle cannot be null");
        }
    }

    internal enum eVehicleStatus
    {
        eInRepair,
        eRepaired,
        ePaid
    }
}
