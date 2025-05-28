using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eGarageVehicleStatus m_Status;
        private Vehicle m_Vehicle;

        public enum eGarageVehicleStatus
        {
            InRepair,
            Repaired,
            Paid,
        }

        public VehicleInGarage(string i_OwnerName, string i_OwnerPhoneNumber, eGarageVehicleStatus i_Status, Vehicle i_Vehicle)
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

        internal string OwnerName
        {
            get { return m_OwnerName; }
        }

        internal string OwnerPhoneNumber
        {
            get { return m_OwnerPhoneNumber; }
        }

        public eGarageVehicleStatus Status
        {
            get { return m_Status; }
            set { m_Status = value; }
        }

        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "Vehicle cannot be null");
                }
                m_Vehicle = value;
            }
        }

        public override string ToString()
        {
            StringBuilder details = new StringBuilder();

            details.AppendLine("----- Vehicle In Garage -----");
            details.AppendLine($"Owner Name:\t{OwnerName}");
            details.AppendLine($"Owner Phone:\t{OwnerPhoneNumber}");
            details.AppendLine($"Status:\t\t{Status}");
            details.AppendLine();
            details.AppendLine(Vehicle.ToString());

            return details.ToString();
        }
    }
}
