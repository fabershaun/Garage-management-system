using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class ElectricEngine : Engine
    {
        public ElectricEngine(float i_BatteryMaxHours)
        {
            m_MaxEnergyAmount = i_BatteryMaxHours;
            m_EngineType = eEngineType.Electric;
        }
        
        public override string ToString()
        {
            StringBuilder engineInfo = new StringBuilder();

            engineInfo.AppendLine($"-----\tENGINE\t-----");
            engineInfo.AppendLine($"Engine type:\tElectric");
            engineInfo.AppendLine($"Current hours left :\t{m_CurrentEnergyAmount:F1}");
            engineInfo.AppendLine($"Max energy amount:\t{m_MaxEnergyAmount}");
            engineInfo.AppendLine();

            return engineInfo.ToString();
        }
    }
}
