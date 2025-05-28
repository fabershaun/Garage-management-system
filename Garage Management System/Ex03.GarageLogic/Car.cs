using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.Motorcycle;

namespace Ex03.GarageLogic
{
    internal abstract class Car : Vehicle
    {
        private const int k_MaxAirPressureOfCar = 32;
        private const int k_NumOfCarWheels = 5;
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

        
        internal eCarColor Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }

        internal eNumOfDoors NumOfDoors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
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

        public override void ValidateAnswersAndSetValues(string[] i_Answers)
        {
            if (i_Answers.Length != 2)
            {
                throw new ArgumentException("Car expects exactly 2 additional answers.");
            }

            // License type
            if (!Enum.TryParse(i_Answers[0], out eCarColor color))
            {
                throw new FormatException("Invalid car color.");
            }
            m_Color = color;

            // Engine displacement
            if (!Enum.TryParse(i_Answers[1], out eNumOfDoors numOfDoors))
            {
                throw new FormatException("Invalid number of doors.");
            }
            m_NumOfDoors = numOfDoors;
        }
    }
}
