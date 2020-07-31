using System;

namespace Amplius.Utils.Properties
{
#nullable enable
    /// <summary>
    /// Represents an object which has a value and default value of a non-generic type
    /// </summary>
    public interface IProperty : INamespaced
    {
        /// <summary>
        /// Non-generic default value
        /// </summary>
        public object? DefaultValue { get; }
        /// <summary>
        /// Non generic value
        /// </summary>
        public object? Value { get; set; }
    }

    /// <summary>
    /// Represents an object which has a value and default value of type <typeparamref name="T"/>
    /// 
    /// <para>A generic version of <see cref="IProperty"/></para>
    /// </summary>
    /// <typeparam name="T">Type of value and default value</typeparam>
    public interface IProperty<T> : IProperty
    {
        /// <summary>
        /// Generic default value of type <typeparamref name="T"/>
        /// </summary>
        public new T DefaultValue { get; }
        /// <summary>
        /// Generic value of type <typeparamref name="T"/>
        /// </summary>
        public new T Value { get; set; }
        /// <summary>
        /// A <see cref="EventHandler{T}"/> invoked upon <see cref="Value"/> change
        /// </summary>
        public event EventHandler<T> OnValueChange;
    }
}
