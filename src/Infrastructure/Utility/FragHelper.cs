using System.ComponentModel;
using System.Reflection;

namespace Infrastructure.Utility;

public static class FragHelper
{
    public static Dictionary<string, int> LoadEnumToDictionary<TEnum>() where TEnum : Enum
    {
        var dict = new Dictionary<string, int>();
        var enumType = typeof(TEnum);
        var enumValues = Enum.GetValues(enumType);
        
        foreach (var enumValue in enumValues)
        {
            var enumName = Enum.GetName(enumType, enumValue);
            var enumDescription = enumType.GetField(enumName!)!.GetCustomAttribute<DescriptionAttribute>()?.Description;
            if (enumDescription != null)
            {
                dict.Add(enumDescription, (int)enumValue);
            }
        }
        return dict;
    }
}