using System;
using ReactiveUI;

namespace GensouSakuya.Aria2.Desktop.Shell.Helper
{

    public abstract class FromDecimalConverter : IBindingTypeConverter
    {
        public int GetAffinityForObjects(Type fromType, Type toType)
        {
            if (fromType == typeof(decimal))
            {
                return 100;
            }

            return 0;
        }

        public abstract bool TryConvert(object from, Type toType, object conversionHint, out object result);
    }
    public class DecimalToDoubleConverter : FromDecimalConverter
    {
        public override bool TryConvert(object from, Type toType, object conversionHint, out object result)
        {
            try
            {
                result = Convert.ToDouble((decimal) from);
            }
            catch
            {
                result = null;
                return false;
            }

            return true;
        }
    }
}
