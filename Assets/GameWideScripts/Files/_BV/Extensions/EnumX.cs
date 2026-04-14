using System;
using System.Linq;

public static class EnumX 
{
    static public string GetEnumNameFromInt(Type enumType, int value)
    {
        return Enum.GetName(enumType, value);
    }
    static public string GetEnumNameByValue(Type enumType, int value)
    {
        foreach (var v in Enum.GetValues(enumType))
            if ((int)v == value)
                return v.ToString();
        return null;
    }
    static public int GetEnumValueByName(Type enumType, string name)
    {
        foreach (var v in Enum.GetValues(enumType))
            if (v.ToString().Equals(name))
                return (int)v;
        return -999999;
    }
    static public int GetEnumValueByInt(Type enumType, int value)
    {
        foreach (var v in Enum.GetValues(enumType))
            if ((int)v == value)
                return (int)v;
        return -999999;
    }
    /// <summary>
    /// Extension method to return an enum value of type T for the given string.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
    /// <summary>
    /// Adds space between words in an enum
    /// </summary>
    /// <param name="_enum">The enum as a string</param>
    /// <returns>Formatted enum with spaces</returns>
    static public string EnumNameFormatted(string _enum)
    {
        return string.Concat(_enum.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
    }
    /// <summary>
    /// Gets a random enum for an enum list
    /// </summary>
    /// <typeparam name="T">The Enum type to look through</typeparam>
    /// <returns>A random enum value</returns>
    static public T RandomEnum<T>()
    {
        var values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(UnityEngine.Random.Range(0, values.Length));
    }

    /// <summary>
    /// Gets the next enum in its list
    /// https://stackoverflow.com/questions/642542/how-to-get-next-or-previous-enum-value-in-c-sharp
    /// </summary>
    public static T Next<T>(this T v) where T : struct
    {
        return Enum.GetValues(v.GetType()).Cast<T>().Concat(new[] { default(T) }).SkipWhile(e => !v.Equals(e)).Skip(1).First();
    }

    /// <summary>
    /// Get the previous enum in its list
    /// </summary>
    public static T Previous<T>(this T v) where T : struct
    {
        return Enum.GetValues(v.GetType()).Cast<T>().Concat(new[] { default(T) }).Reverse().SkipWhile(e => !v.Equals(e)).Skip(1).First();
    }

    //static public T GetEnumByString<T>(string _value)
    //{
    //          bool success = Enum.TryParse<T>(_value, out T result);
    //          if (success)
    //              return result;
    //          else
    //              return null;
    //      }

    //static public T GetEnumByStringContaining<T>(string _file, T t)
    //      {
    //          string stringCheck = "";
    //          var subGroups = Enum.GetValues(typeof(T));
    //          foreach (var v in subGroups)
    //          {
    //              if (v.ToString().ToLower().StartsWith(_file.ToLower()))
    //                  stringCheck = v.ToString();
    //          }

    //          if (Enum.TryParse(stringCheck, out T result))
    //              return result;
    //          else
    //              return t;
    //      }


}
