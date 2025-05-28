using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly string r_ModelName;
        protected readonly string r_LicenseNumber;
        protected List<Wheel> m_Wheels;
        protected Engine m_Engine;

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
        
        public Engine Engine
        {
            get
            {
                return m_Engine;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
            set
            {
                m_Wheels = value ?? throw new ArgumentNullException("value");
            }
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
            vehicleInfo.AppendLine($"Wheel Current Air pressure: {m_Wheels[0].CurrentAirPressure}");
            vehicleInfo.AppendLine($"Wheel Max Air pressure: {m_Wheels[0].MaxAirPressure}");
            vehicleInfo.AppendLine();

            vehicleInfo.AppendLine(m_Engine.ToString());

            return vehicleInfo.ToString();
        }

        public abstract void SetAdditionalInfo(string i_AdditionalInfo1, string i_AdditionalInfo2);

        public abstract List<(string Question, string[] options)> GetAddAdditionalQuestionsAndAnswerOptions();

        public abstract void ValidateAnswersAndSetValues(string[] i_Answers, int index);

        public void AddEnergy(float i_AmountToAdd)
        {
            m_Engine.AddEnergy(i_AmountToAdd);
        }

    }
}



