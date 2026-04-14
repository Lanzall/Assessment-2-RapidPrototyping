using System;
using System.Text.RegularExpressions;
using UnityEngine;

public static class StringX 
{
    /// <summary>
    /// Formats an integer with leading spaces
    /// </summary>
    /// <returns>The formatted string</returns>
    /// <param name="value">Value.</param>
    /// <param name="length">Min length.</param>
    static public string FormatIntSpaces(int value, int length)
    {
        return String.Format("{0," + length + "}", value);
    }
    /// <summary>
    /// Formats an integer with leading Zeroes
    /// </summary>
    /// <returns>The formatted string</returns>
    /// <param name="value">Value.</param>
    /// <param name="length">Min length.</param>
    static public string FormatIntZeroes(int value, int length)
    {
        return value.ToString("D" + length);
    }
    /// <summary>
    /// Formats a float with a specified number of decimals
    /// </summary>
    /// <returns>The formatted string</returns>
    /// <param name="value">The Value.</param>
    /// <param name="decimals">Max Number of decimals.</param>
    /// <param name="decimals">If flexible, will drop trailing zeroes</param>
    static public string FormatFloat(float number, int decimals = 2, bool flex = false)
    {
        if (decimals <= 0) return ((int)number).ToString();
        string fmt = FormatDecimalsFormat(decimals, flex);
        return String.Format(fmt, number);
    }
    /// <summary>
    /// Formats a double with a specified number of decimals
    /// </summary>
    /// <returns>The formatted string</returns>
    /// <param name="value">The Value.</param>
    /// <param name="decimals">Max Number of decimals.</param>
    /// <param name="decimals">If flexible, will drop trailing zeroes</param>
    static public string FormatDouble(double number, int decimals, bool flex = false)
    {
        if (decimals <= 0) return ((long)number).ToString();
        string fmt = FormatDecimalsFormat(decimals, flex);
        return String.Format(fmt, number);
    }

    static public string FormatDecimalsFormat(int decimals, bool flex = true)
    {
        string fmt = "{0:#,0.";
        for (int i = 0; i < decimals; i++)
            fmt += (flex && i > 0) ? "#" : "0";
        fmt += "}";
        return fmt;
    }


    /// <summary>
    /// Formats a float value a a time string like mm:ss or h:mm:ss
    /// </summary>
    /// <returns>The formatted string</returns>
    /// <param name="value">The time in seconds</param>
    /// <param name="millis">Formats second decimals (h:mm:ss.dd)</param>
    static public string FormatTime(float value, bool decimals = false)
    {
        string result = "";
        float v = value;
        if (v < 0f)
        {
            v = -v;
            result = "-";
        }
        v = Mathf.FloorToInt(v);
        int secs = Mathf.FloorToInt(v % 60f);
        int mins = Mathf.FloorToInt((v % 3600f) / 60f);
        int hours = Mathf.FloorToInt(v / 3600f);
        if (hours > 0)
            result += hours.ToString() + ":" + FormatIntZeroes(mins, 2);
        else
            result += mins.ToString();
        result += ":" + FormatIntZeroes(secs, 2);
        if (decimals)
            result += "." + FormatIntZeroes(Mathf.FloorToInt((Mathf.Abs(value) - v) * 100f), 2);
        return result;
    }
    /// <summary>
    /// Un-Format a time string likeh:mm:ss into float
    /// </summary>
    /// <returns>The formatted time string</returns>
    /// <param name="value">The time in seconds, or -1 if invlaid string</param>
    static public float UnFormatTime(string value)
    {
        if (string.IsNullOrEmpty(value))
            return 0f;
        bool neg = (value[0] == '-');
        if (neg)
            value = value.Substring(1);
        float result = 0f;
        // Get decimals
        string[] parts = value.Split('.');
        if (parts.Length == 2f)
        {
            value = parts[0];
            if (!float.TryParse("0." + parts[1], out result))
                return -1f;
        }
        else if (parts.Length == 1)
            value = parts[0];
        else
            return -1f;
        // GEt hh:mm:ss
        int hours = 0;
        int minutes = 0;
        int seconds = 0;
        parts = value.Split(':');
        int partCount = parts.Length;
        if (partCount > 0)
            if (!Int32.TryParse(parts[--partCount], out seconds))
                return -1f;
        if (partCount > 0)
            if (!Int32.TryParse(parts[--partCount], out minutes))
                return -1f;
        if (partCount > 0)
            if (!Int32.TryParse(parts[--partCount], out hours))
                return -1f;
        result += (hours * 60f * 60f) + (minutes * 60f) + seconds;
        return neg ? -result : result;
    }

    /// <summary>
    /// Ex: "ThisIsCamelCase" -> "This Is Camel Case"
    /// </summary>
    static public string SplitCamelCase(this string input)
    {
        return Regex.Replace(input, "[A-Z]", " $0");
    }
}
