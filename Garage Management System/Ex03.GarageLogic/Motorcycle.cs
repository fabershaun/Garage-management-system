using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;

namespace Ex03.GarageLogic
{
    internal abstract class Motorcycle : Vehicle
    {
        private const int k_MaxAirPressureOfMotorcycle = 30;
        private const int k_NumOfMotorcycleWheels = 2;
        private const int k_NumOfAdditionalOptions = 2;
        private eLicenseType m_LicenseType;
        private int m_EngineDisplacementCc;

        internal Motorcycle(string i_LicenseNumber, string i_ModelName): base(i_LicenseNumber, i_ModelName)
        {
            List<Wheel> wheels = new List<Wheel>();

            for (int i = 0; i < k_NumOfMotorcycleWheels; i++) //Pay attention to facebook - what guy said about user update every wheel
            {
                wheels.Add(new Wheel(k_MaxAirPressureOfMotorcycle));
            }

            Wheels = wheels;
        }
        public enum eLicenseType
        {
            A = 1,
            A2,
            AB,
            B2,
        }

        internal eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set
            {
                if (!Enum.IsDefined(typeof(eLicenseType), value))
                {
                    throw new ArgumentException("Invalid license type");
                }
                m_LicenseType = value;
            }
        }
        
        internal int EngineDisplacementCc
        {
            get { return m_EngineDisplacementCc; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Engine displacement must be a positive value");
                }
                m_EngineDisplacementCc = value;
            }
        }

        public override void SetAdditionalInfo(string i_AdditionalInfo1, string i_AdditionalInfo2)
        {
            m_LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_AdditionalInfo1, false);
            m_EngineDisplacementCc = int.Parse(i_AdditionalInfo2);
        }

        public override List<(string Question, string[] options)> GetAddAdditionalQuestionsAndAnswerOptions()
        {
            return new List<(string, string[])>
            {
                ("Enter license type", Enum.GetNames(typeof(eLicenseType))),
                ("Enter engine displacement in cc", null)
            };
        }

        public override void ValidateAnswersAndSetValues(string[] i_Answers, int i_Index)
        {
            if (i_Answers.Length != k_NumOfAdditionalOptions)
            {
                throw new ArgumentException($"Motorcycle expects exactly {k_NumOfAdditionalOptions} additional answers.");
            }

            if (i_Index == 0)
            {
                // License type
                if (!Enum.TryParse(i_Answers[0], ignoreCase: true, out eLicenseType license))
                {
                    throw new FormatException("Invalid license type.");
                }

                LicenseType = license;
            }
            else if (i_Index == 1)
            {
                // Engine displacement
                if (!int.TryParse(i_Answers[1], out int displacement))
                {
                    throw new FormatException("Invalid engine displacement.");
                }

                EngineDisplacementCc = displacement;
            }
        }

        public override string GetAdditionalInfo()
        {
            StringBuilder additionalMotorcycleInfo = new StringBuilder();

            additionalMotorcycleInfo.AppendLine("-----\tADDITIONAL INFO\t-----");
            additionalMotorcycleInfo.AppendLine($"License Type: {m_LicenseType}");
            additionalMotorcycleInfo.AppendLine($"Engine Displacement: {m_EngineDisplacementCc}");
            additionalMotorcycleInfo.AppendLine();

            return additionalMotorcycleInfo.ToString();
        }

    }
}
