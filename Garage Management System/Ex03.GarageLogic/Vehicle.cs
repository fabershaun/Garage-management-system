using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly string r_ModelName;
        protected readonly string r_LicenseNumber;
        private float m_EnergyPercentage;
        private List<Wheel> m_Wheels;

        protected Vehicle(string i_LicenseNumber, string i_ModelName)
        {
            if (string.IsNullOrEmpty(i_ModelName))
            {
                throw new ArgumentNullException("i_ModelName", "Model cannot be null or empty");
            }
            if (string.IsNullOrEmpty(i_LicenseNumber))
            {
                throw new ArgumentNullException("i_LicenseNumber", "License plate cannot be null or empty");
            }
            r_ModelName = i_ModelName;
            r_LicenseNumber = i_LicenseNumber;
        }

        public List<Wheel> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = SetWheels(value); }
        }

        protected float EnergyPercentage
        {
            get { return m_EnergyPercentage; }
            set { m_EnergyPercentage = SetEnergyPercentage(value); }
        }

        internal List<Wheel> SetWheels(List<Wheel> i_Wheels)
        {
            if (i_Wheels == null)
            {
                throw new ArgumentNullException("i_Wheels");
            }

            return i_Wheels;
        }

        public float SetEnergyPercentage(float i_Percentage)
        {
            if (i_Percentage < 0 || i_Percentage > 100)
            {
                throw new ValueRangeException(i_Percentage, 0, 100);
            }

            return i_Percentage;
        }

        public override string ToString()
        {
            StringBuilder vehicleInfo = new StringBuilder();

            vehicleInfo.AppendLine("-----\tGENERAL\t-----");
            vehicleInfo.AppendLine($"License Plate:\t{r_LicenseNumber}");
            vehicleInfo.AppendLine($"Model Name:\t{r_ModelName}");
            vehicleInfo.AppendLine();

            vehicleInfo.AppendLine("-----\tTIERS\t-----");
            vehicleInfo.AppendLine($"Wheel Manufacture: {m_Wheels[0].Manufacturer}");
            vehicleInfo.AppendLine($"Wheel Current Air pressure:\t{m_Wheels[0].CurrentAirPressure}");

            return vehicleInfo.ToString();
        }

        public abstract void SetAdditionalInfo(string i_AdditionalInfo1, string i_AdditionalInfo2);

        public abstract void AddAdditionalQuestions(List<string> io_QuestionsList);
    }
}
