using System;

namespace Ex03.GarageLogic
{
    // Exception thrown when a value is out of the allowed range
    internal class ValueRangeException : Exception
    {
        internal float MaxValue { get; }
        internal float MinValue { get; }

        public ValueRangeException(float i_Value, float i_MinValue, float i_MaxValue)
            : base($"Value {i_Value} is out of range. Allowed range: {i_MinValue} - {i_MaxValue}.")
        {
            MinValue = i_MinValue;
            MaxValue = i_MaxValue;
        }

        public ValueRangeException(float i_MinValue, float i_MaxValue, string i_Message)
            : base(i_Message)
        {
            MinValue = i_MinValue;
            MaxValue = i_MaxValue;
        }
    }
}
