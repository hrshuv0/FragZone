using System.ComponentModel;
using System.Reflection;
using Core.Common.Constants;

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

    public static List<FragEnum> LoadEnumToValue<TEnum>()
    {
        var enumList = new List<FragEnum>();
        var enumType = typeof(TEnum);
        var enumValues = Enum.GetValues(enumType);
        
        foreach (var enumValue in enumValues)
        {
            var enumName = Enum.GetName(enumType, enumValue);
            var enumDescription = enumType.GetField(enumName!)!.GetCustomAttribute<DescriptionAttribute>()?.Description;
            if (enumDescription != null)
            {
                enumList.Add(new FragEnum
                {
                    Key = enumDescription,
                    Value = (int)enumValue
                });
            }
        }
        return enumList;
    }
    
    
}