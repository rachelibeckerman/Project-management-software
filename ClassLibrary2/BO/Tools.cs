using System.Reflection;

namespace BO;

internal static class Tools
{
    public static string ToStringProperty<T>(T item)
    {
        string stringProperty = "";
        foreach (PropertyInfo prop in item!.GetType().GetProperties())
        {
            stringProperty += prop.Name;
            stringProperty += " : ";
            if (prop.PropertyType == typeof(double))
                stringProperty += String.Format("{0:0.00}", item.GetType().GetProperty(prop.Name)?.GetValue(item));
            else
                stringProperty += item.GetType().GetProperty(prop.Name)?.GetValue(item);
            stringProperty += "\n";
        }
        return stringProperty;
    }
}
