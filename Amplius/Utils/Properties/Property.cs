using System;

namespace Amplius.Utils.Properties
{
#nullable enable
    /// <summary>
    /// A default implementation of <see cref="IProperty{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Property<T> : IProperty<T>
    {
        object? IProperty.DefaultValue => DefaultValue;
        object? IProperty.Value
        {
            get => value;
            set
            {
                if (value is T)
                    this.value = (T)value;
            }
        }
        public T DefaultValue { get; }
        public virtual T Value
        {
            get => value;
            set
            {
                this.value = value;
                OnValueChange?.Invoke(this, value);
            }
        }
        /// <summary>
        /// Nullable <see cref="NamespaceKey"/> for use in localization of settings/properties UI or the like
        /// </summary>
        public NamespaceKey? Key { get; }

        public event EventHandler<T>? OnValueChange;

        private T value;

        public Property(T defaultValue = default, NamespaceKey? key = null)
        {
            Key = key;
            DefaultValue = defaultValue;
            value = defaultValue;
        }
    }
}
