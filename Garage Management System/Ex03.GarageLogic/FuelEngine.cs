using System.Text;
using static Ex03.GarageLogic.Utils;

namespace Ex03.GarageLogic
{
    internal class FuelEngine
    {
        private float m_CurrentFuelAmount = 0;
        private readonly float r_MaxFuelAmount;
        private readonly Utils.eFuelType r_FuelType;

        public FuelEngine(float i_MaxFuelAmount, Utils.eFuelType i_FuelType)
        {
            r_MaxFuelAmount = i_MaxFuelAmount;
            r_FuelType = i_FuelType;
        }

        internal float CurrentFuelAmount
        {
            get
            {
                return m_CurrentFuelAmount;
            }
            set
            {
                if(value > r_MaxFuelAmount || value < 0)
                {
                    throw new ValueRangeException(value, 0, r_MaxFuelAmount);
                }
                
                m_CurrentFuelAmount = value;
            }
        }

        internal float MaxFuelAmount
        {
            get
            {
                return r_MaxFuelAmount;
            }
        }

        internal void AddFuel(float i_AmountToAdd)
        {
            if (i_AmountToAdd < 0)
            {
                throw new ValueRangeException(0, r_MaxFuelAmount, $"Cannot add negative fuel: {i_AmountToAdd}");
            }

            float newAmount = CurrentFuelAmount + i_AmountToAdd;

            if (newAmount > r_MaxFuelAmount)
            {
                throw new ValueRangeException(0, r_MaxFuelAmount, $"Fuel amount exceeds max allowed: {newAmount}");
            }

            CurrentFuelAmount = newAmount;
        }

        public override string ToString()
        {
            StringBuilder engineInfo = new StringBuilder();

            engineInfo.AppendLine("-----\tENGINE\t-----");
            engineInfo.AppendLine($"Engine Type:\tFuel");
            engineInfo.AppendLine($"Fuel Type:\t{r_FuelType}");
            engineInfo.AppendLine($"Current Fuel Amount:{CurrentFuelAmount}");
            engineInfo.AppendLine($"Max Fuel Amount:{MaxFuelAmount}");
            engineInfo.AppendLine();

            return engineInfo.ToString();
        }
    }
}
