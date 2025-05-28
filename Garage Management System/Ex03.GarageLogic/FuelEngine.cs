using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private readonly eFuelType r_FuelType;

        public enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler,
        }

        public FuelEngine(float i_MaxFuelAmount, eFuelType i_FuelType)
        {
            m_MaxEnergyAmount = i_MaxFuelAmount;
            r_FuelType = i_FuelType;
            m_EngineType = eEngineType.Fuel;
        }

        public eFuelType FuelType
        {
            get { return r_FuelType; }
        }

        public override string ToString()
        {
            StringBuilder engineInfo = new StringBuilder();

            engineInfo.AppendLine("-----\tENGINE\t-----");
            engineInfo.AppendLine($"Engine Type:\tFuel");
            engineInfo.AppendLine($"Fuel Type:\t{r_FuelType}");
            engineInfo.AppendLine($"Current Fuel Amount: {m_CurrentEnergyAmount}");
            engineInfo.AppendLine($"Max Fuel Amount: {m_MaxEnergyAmount}");

            return engineInfo.ToString();
        }
    }
}
