using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Xamarin.Testing.Core.Helpers
{
    public class ReflectionHelper
    {
        private static PropertyInfo[] GetProperties(object obj)
        {
            return obj.GetType().GetProperties();
        }

        public static string GetPropertyValueAsString(object source, string propertyName)
        {
            try
            {
                if (source != null)
                {
                    var properties = GetProperties(source);

                    var property = properties.Where(p => p.Name == propertyName).FirstOrDefault();

                    if (property != null)
                        return property.GetValue(source, null).ToString();

                    return "Error:No Property";
                }
                else
                    return "Error:No Source Class";
            }
            catch (Exception ex)
            {
                return "Error:" + ex.Message;
            }
        }
    }
}
