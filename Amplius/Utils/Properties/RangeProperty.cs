namespace Amplius.Utils.Properties
{
#nullable enable
    /// <summary>
    /// A <see cref="Property{T}"/> of type <see cref="double"/> which implements a <see cref="Min"/> and <see cref="Max"/>
    /// </summary>
    public class RangeProperty : Property<double>
    {
        public override double Value
        {
            get => base.Value;
            set
            {
                if (value > Max)
                    base.Value = Max;
                else if (value < Min)
                    base.Value = Min;
                else
                    base.Value = value;
            }
        }
        public double Max { get; }
        public double Min { get; }

        public RangeProperty(double max, double min = 0.1d, double defaultValue = default, NamespaceKey? key = null) : base(defaultValue, key)
        {
            Max = max;
            Min = min;
        }
    }
}
