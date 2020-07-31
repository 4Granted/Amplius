using Amplius.Utils.Properties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Amplius.Collections
{
    public interface IPropertyList : IEnumerable<IProperty>
    {
        public void ResetDefaults();
    }

    public interface IPropertyList<T> : IEnumerable<IProperty<T>>, IPropertyList
    {
        public new void ResetDefaults();
    }
}
