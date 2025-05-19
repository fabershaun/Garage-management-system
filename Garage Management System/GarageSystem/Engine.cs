using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class Engine
    {
        protected float m_CurrentAmount;
        protected readonly float r_MaxAmount;


        protected Engine(float i_CurrentAmount, float i_MaxAmount)
        {
            if(i_CurrentAmount < 0 || i_CurrentAmount > i_MaxAmount)
            {
                throw new ValueRangeException(0, i_MaxAmount, i_CurrentAmount);
            }

            m_CurrentAmount = i_CurrentAmount;
            r_MaxAmount = i_MaxAmount;
        }
        
        public float GetCurrentAmount
        {
            get
            {
                return m_CurrentAmount;
            }
        }

        public float GetMaxAmount
        {
            get
            {
                return r_MaxAmount;
            }
        }

        public void AddEnergy(float i_Amount)
        {
            if (i_Amount < 0)
            {
                throw new ArgumentException("Fuel amount cannot be negative", nameof(i_Amount));
            }
            if (m_CurrentAmount + i_Amount > r_MaxAmount)
            {
                throw new ArgumentException($"Fuel amount exceeds maximum capacity. Current: {m_CurrentAmount}, Adding: {i_Amount}, Max: {m_MaxAmount}");
            }
            m_CurrentAmount += i_Amount;
        }
        
    }
}
