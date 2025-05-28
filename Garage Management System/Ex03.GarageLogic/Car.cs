using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.FuelEngine;
using static Ex03.GarageLogic.Motorcycle;

namespace Ex03.GarageLogic
{
    internal abstract class Car : Vehicle
    {
        private const int k_MaxAirPressureOfCar = 32;
        private const int k_NumOfCarWheels = 5;
        private const int k_NumOfAdditionalOptions = 2;
        private eCarColor m_Color;
        private eNumOfDoors m_NumOfDoors;

        internal Car(string i_LicenseNumber, string i_ModelName)
            : base(i_LicenseNumber, i_ModelName)
        {
            List<Wheel> wheels = new List<Wheel>();

            for (int i = 0; i < k_NumOfCarWheels; i++)
            {
                wheels.Add(new Wheel(k_MaxAirPressureOfCar));
            }

            Wheels = wheels;
        }

        public enum eCarColor
        {
            Yellow = 1,
            Black,
            White,
            Silver,
        }

        public enum eNumOfDoors
        {
            Two = 1,
            Three,
            Four,
            Five,
        }

        internal eCarColor CarColor
        {
            get { return m_Color; }
            set
            {
                if (!Enum.IsDefined(typeof(eCarColor), value))
                {
                    throw new ArgumentException("Invalid car color");
                }
                m_Color = value;
            }
        }

        internal eNumOfDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
            set
            {
                if (!Enum.IsDefined(typeof(eNumOfDoors), value))
                {
                    throw new ArgumentException("Invalid number of doors");
                }
                m_NumOfDoors = value;
            }
        }

        public override void SetAdditionalInfo(string i_AdditionalInfo1, string i_AdditionalInfo2)
        {
            m_Color = (eCarColor)Enum.Parse(typeof(eCarColor), i_AdditionalInfo1, false);
            m_NumOfDoors = (eNumOfDoors)Enum.Parse(typeof(eNumOfDoors), i_AdditionalInfo2, false);
        }

        public override List<(string Question, string[] options)> GetAddAdditionalQuestionsAndAnswerOptions()
        {
            return new List<(string, string[])>
                       {
                           ("Enter car color: ", Enum.GetNames(typeof(eCarColor))),
                           ("Enter number of doors in the car", Enum.GetNames(typeof(eNumOfDoors)))
                       };
        }

        public override void ValidateAnswersAndSetValues(string[] i_Answers, int i_Index)
        {
            if(i_Answers.Length != k_NumOfAdditionalOptions)
            {
                throw new ArgumentException($"Car expects exactly {k_NumOfAdditionalOptions} additional answers.");
            }

            if(i_Index == 0)
            {
                // License type
                if (!Enum.TryParse(i_Answers[0], ignoreCase: true, out eCarColor color))
                {
                    throw new FormatException("Invalid car color.");
                }

                CarColor = color;
            }
            else if(i_Index == 1)
            {
                // Engine displacement
                if(!Enum.TryParse(i_Answers[1], ignoreCase: true, out eNumOfDoors numOfDoors))
                {
                    throw new FormatException("Invalid number of doors.");
                }

                NumOfDoors = numOfDoors;
            }
        }

        public override string GetAdditionalInfo()
        {
            StringBuilder engineInfo = new StringBuilder();

            engineInfo.AppendLine("-----\tADDITIONAL INFO\t-----");
            engineInfo.AppendLine($"Car Color: {CarColor}");
            engineInfo.AppendLine($"Number Of Doors: {NumOfDoors}");
            engineInfo.AppendLine();

            return engineInfo.ToString();
        }
    }
}
