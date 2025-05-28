using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Engine
    {
        protected float m_EnergyPercentage;
        protected float m_CurrentEnergyAmount;
        protected float m_MaxEnergyAmount;
        protected eEngineType m_EngineType;

        public enum eEngineType
        {
            Fuel,
            Electric,
        }   

        public float EnergyPercentage
        {
            get { return m_EnergyPercentage; }
            set
            {
                if(value < 0 || value > 100)
                {
                    throw new ValueRangeException(0, 100, $"Energy percentage must be between 0 and 100: {value}");
                }

                m_EnergyPercentage = value;
            }
        }

        public float CurrentEnergyAmount
        {
            get { return m_CurrentEnergyAmount; }
            set
            {
                if(value < 0)
                {
                    throw new ValueRangeException(0, m_MaxEnergyAmount, $"Current energy amount cannot be negative: {value}");
                }

                if(value > m_MaxEnergyAmount)
                {
                    throw new ValueRangeException(0, m_MaxEnergyAmount, $"Current energy amount cannot exceed max energy amount: {m_MaxEnergyAmount}");
                }

                m_CurrentEnergyAmount = value;
            }
        }

        public float MaxEnergyAmount
        {
            get { return m_MaxEnergyAmount; }
            set { m_MaxEnergyAmount = value; }
        }

        public eEngineType EngineType
        {
            get { return m_EngineType; }
            set { m_EngineType = value; }
        }

        public override string ToString()
        {
            return $"Energy Percentage: {m_EnergyPercentage}%{Environment.NewLine}Max Energy Amount: {m_MaxEnergyAmount}";
        }

        internal virtual void AddEnergy(float i_AmountToAdd)
        {
            if (i_AmountToAdd < 0)
            {
                throw new ValueRangeException(0, m_MaxEnergyAmount, $"Cannot add negative fuel: {i_AmountToAdd}");
            }

            float newAmount = m_CurrentEnergyAmount + i_AmountToAdd;

            if (newAmount > m_MaxEnergyAmount)
            {
                throw new ValueRangeException(0, m_MaxEnergyAmount, $"Fuel amount exceeds max allowed: {m_MaxEnergyAmount}");
            }

            m_CurrentEnergyAmount = newAmount;
            m_EnergyPercentage = (m_CurrentEnergyAmount / m_MaxEnergyAmount) * 100.0f;
        }

        public float ConvertAmountToPercentage(float i_Amount)
        {
            return (i_Amount / m_MaxEnergyAmount) * 100.0f;
        }

        public float ConvertPercentageToAmount(float i_Percentage)
        {
            return (i_Percentage / 100.0f) * m_MaxEnergyAmount;
        }
    }
}
