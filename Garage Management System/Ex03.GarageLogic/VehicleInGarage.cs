﻿using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;
        private Vehicle m_Vehicle;
        public eGarageVehicleStatus Status { get; set; }

        public enum eGarageVehicleStatus
        {
            InRepair = 1,
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

            r_OwnerName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
            Status = i_Status;
            m_Vehicle = i_Vehicle ?? throw new ArgumentNullException("i_Vehicle", "Vehicle cannot be null");
        }

        internal string OwnerName
        {
            get { return r_OwnerName; }
        }

        internal string OwnerPhoneNumber
        {
            get { return r_OwnerPhoneNumber; }
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
