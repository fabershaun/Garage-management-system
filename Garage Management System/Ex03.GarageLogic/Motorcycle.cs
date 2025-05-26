using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        private const int k_MaxAirPressureOfMotorcycle = 30;
        private const int k_NumOfMotorcycleWheels = 2;
        private eLicenseType m_LicenseType;
        private int m_EngineDisplacementCc;

        internal Motorcycle(string i_LicenseNumber, string i_ModelName): base(i_LicenseNumber, i_ModelName)
        {
            List<Wheel> wheels = new List<Wheel>();

            for (int i = 0; i < k_NumOfMotorcycleWheels; i++) //Pay attention to facebook - what guy said about user update every wheel
            {
                wheels.Add(new Wheel(k_MaxAirPressureOfMotorcycle));
            }
            SetWheels(wheels);
        }

        internal eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }
        
        internal int EngineDisplacementCc
        {
            get { return m_EngineDisplacementCc; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Engine displacement must be a positive value", "value");
                }
                m_EngineDisplacementCc = value;
            }
        }

        public enum eLicenseType
        {
            A,
            A2,
            AB,
            B2,
        }

        public override void SetAdditionalInfo(string i_AdditionalInfo1, string i_AdditionalInfo2)
        {
            m_LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_AdditionalInfo1, false);
            m_EngineDisplacementCc = int.Parse(i_AdditionalInfo2);
        }

        public override void AddAdditionalQuestions(List<string> io_QuestionsList)
        {
            io_QuestionsList.Add("License type (A, A2, AB, B2)");
            io_QuestionsList.Add("Engine displacement in cc");
        }
    }
}
