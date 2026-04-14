using UnityEngine;

public static class MathX
{
    /// <summary>
    /// Maps a value from one range to another
    /// </summary>
    /// <returns>The mapped value</returns>
    /// <param name="value">The input Value.</param>
    /// <param name="inMin">Input min</param>
    /// <param name="inMax">Input max</param>
    /// <param name="outMin">Output min</param>
    /// <param name="outMax">Output max</param>
    /// <param name="clamp">Clamp output value to outMin..outMax</param>
    static public float Map(float value, float inMin, float inMax, float outMin, float outMax, bool clamp = true)
    {
        float f = ((value - inMin) / (inMax - inMin));
        float d = (outMin <= outMax ? (outMax - outMin) : -(outMin - outMax));
        float v = (outMin + d * f);
        if (clamp) v = ClampSmart(v, outMin, outMax);
        return v;
    }
    /// <summary>
    /// Maps a value from 0f..1f to another range
    /// </summary>
    /// <returns>The mapped value</returns>
    /// <param name="value">The input Value.</param>
    /// <param name="outMin">Output min</param>
    /// <param name="outMax">Output max</param>
    /// <param name="clamp">Clamp output value to outMin..outMax</param>
    static public float MapFrom01(float value, float outMin, float outMax, bool clamp = true)
    {
        return Map(value, 0f, 1f, outMin, outMax, clamp);
    }
    /// <summary>
    /// Maps a value from a range to 0f..1f
    /// </summary>
    /// <returns>The mapped value</returns>
    /// <param name="value">The input Value.</param>
    /// <param name="inMin">Input min</param>
    /// <param name="inMax">Input max</param>
    /// <param name="clamp">Clamp output value to 0f..1f</param>
    static public float MapTo01(float value, float inMin, float inMax, bool clamp = true)
    {
        return Map(value, inMin, inMax, 0f, 1f, clamp);
    }
    /// <summary>
    /// Clamps a value, swapping min/max if necessary
    /// </summary>
    /// <returns>The smart.</returns>
    /// <param name="value">The value to clamp</param>
    /// <param name="min">Min output value</param>
    /// <param name="max">Max output value</param>
    static public float ClampSmart(float value, float min, float max)
    {
        if (min < max)
            return Mathf.Clamp(value, min, max);
        if (max < min)
            return Mathf.Clamp(value, max, min);
        return value;
    }

    static public float TrimFloat(float value, int decimals = 2)
    {
        float m = Mathf.Pow(10f, decimals);
        int i = (int)(value * m);
        return ((float)i) / m;
    }

    /// <summary>
    /// Max Of 2 numbers  functions
    /// </summary>
    static public byte Max(byte a, byte b) { return a > b ? a : b; }
    static public short Max(short a, short b) { return a > b ? a : b; }
    static public int Max(int a, int b) { return a > b ? a : b; }
    static public long Max(long a, long b) { return a > b ? a : b; }
    static public float Max(float a, float b) { return a > b ? a : b; }
    static public double Max(double a, double b) { return a > b ? a : b; }
    /// <summary>
    /// Max of 3 numbers  functions
    /// </summary>
    static public byte Max(byte a, byte b, byte c) { return a > b ? (a > c ? a : c) : (b > c ? b : c); }
    static public short Max(short a, short b, short c) { return a > b ? (a > c ? a : c) : (b > c ? b : c); }
    static public int Max(int a, int b, int c) { return a > b ? (a > c ? a : c) : (b > c ? b : c); }
    static public long Max(long a, long b, long c) { return a > b ? (a > c ? a : c) : (b > c ? b : c); }
    static public float Max(float a, float b, float c) { return a > b ? (a > c ? a : c) : (b > c ? b : c); }
    static public double Max(double a, double b, double c) { return a > b ? (a > c ? a : c) : (b > c ? b : c); }
    /// <summary>
    /// Min Of 2 numbers  functions
    /// </summary>
    static public byte Min(byte a, byte b) { return a < b ? a : b; }
    static public short Min(short a, short b) { return a < b ? a : b; }
    static public int Min(int a, int b) { return a < b ? a : b; }
    static public long Min(long a, long b) { return a < b ? a : b; }
    static public float Min(float a, float b) { return a < b ? a : b; }
    static public double Min(double a, double b) { return a < b ? a : b; }
    /// <summary>
    /// Min of 3 numbers  functions
    /// </summary>
    static public byte Min(byte a, byte b, byte c) { return a < b ? (a < c ? a : c) : (b < c ? b : c); }
    static public short Min(short a, short b, short c) { return a < b ? (a < c ? a : c) : (b < c ? b : c); }
    static public int Min(int a, int b, int c) { return a < b ? (a < c ? a : c) : (b < c ? b : c); }
    static public long Min(long a, long b, long c) { return a < b ? (a < c ? a : c) : (b < c ? b : c); }
    static public float Min(float a, float b, float c) { return a < b ? (a < c ? a : c) : (b < c ? b : c); }
    static public double Min(double a, double b, double c) { return a < b ? (a < c ? a : c) : (b < c ? b : c); }

    // Geometry
    static public float AngleBetween(Vector3 v0, Vector3 v1)
    {
        return Mathf.Atan2(v1.y - v0.y, v1.x - v0.x) * Mathf.Rad2Deg;
    }
    /// <summary>
    /// Abs function for Vector2
    /// </summary>
    static public Vector2 Abs(Vector2 v)
    {
        return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
    }
    /// <summary>
    /// Abs function for Vector3
    /// </summary>
    static public Vector3 Abs(Vector3 v)
    {
        return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
    }
    /// <summary>
    /// Min of Abs function for Vector2
    /// </summary>
    static public Vector2 MinAbs(Vector2 v0, Vector2 v1)
    {
        Vector2 abs0 = Abs(v0);
        Vector2 abs1 = Abs(v1);
        return Vector2.Min(abs0, abs1) == abs0 ? v0 : v1;
    }
    /// <summary>
    /// Min of Abs function for Vector3
    /// </summary>
    static public Vector3 MinAbs(Vector3 v0, Vector3 v1)
    {
        Vector3 abs0 = Abs(v0);
        Vector3 abs1 = Abs(v1);
        return Vector3.Min(abs0, abs1) == abs0 ? v0 : v1;
    }
    /// <summary>
    /// Max of Abs function for Vector2
    /// </summary>
    static public Vector2 MaxAbs(Vector2 v0, Vector2 v1)
    {
        Vector2 abs0 = Abs(v0);
        Vector2 abs1 = Abs(v1);
        return Vector2.Max(abs0, abs1) == abs0 ? v0 : v1;
    }
    /// <summary>
    /// Max of Abs function for Vector3
    /// </summary>
    static public Vector3 MaxAbs(Vector3 v0, Vector3 v1)
    {
        Vector3 abs0 = Abs(v0);
        Vector3 abs1 = Abs(v1);
        return Vector3.Max(abs0, abs1) == abs0 ? v0 : v1;
    }

    /// <summary>
    /// Checks if a value is inbetween a range of numbers
    /// </summary>
    /// <param name="val">The value to check</param>
    /// <param name="min">the minimum range (inclusive)</param>
    /// <param name="max">the maximum range (inclusive)</param>
    /// <returns></returns>
    static public bool InRange(float val, float min, float max)
    {
        return val >= Mathf.Min(min, max) && val <= Mathf.Max(min, max);
    }
    /// <summary>
    /// Checks if a value is inbetween a range of numbers
    /// </summary>
    /// <param name="val">The value to check</param>
    /// <param name="min">the minimum range (inclusive)</param>
    /// <param name="max">the maximum range (inclusive)</param>
    /// <returns></returns>
    static public bool InRange(int val, int min, int max)
    {
        return val >= Mathf.Min(min, max) && val <= Mathf.Max(min, max);
    }

    /// <summary>
    /// Gets the remaining values between two floats
    /// </summary>
    /// <param name="final">The value to get the remainder to</param>
    /// <param name="current">The current value to compare</param>
    /// <returns></returns>
    static public float Remainder(float final, float current)
    {
        return final - current;
    }

    /// <summary>
    /// Inverts a value to its negative (or positive) equivelant
    /// </summary>
    /// <param name="_value">The value to invert</param>
    /// <returns>An inverted value</returns>
    static public float InvertedValue(float _value)
    {
        return -_value;
    }

    /// <summary>
    /// Gets just the decimal part from a float
    /// </summary>
    /// <param name="_value">The float to get the decimals from</param>
    /// <returns>Just the decimal part of a float</returns>
    static public float FloatDecimals(float _value)
    {
        return _value % 1;
    }

    /// <summary>
    /// Checks if we are a multiple of a number
    /// </summary>
    /// <param name="_a">the number to check for</param>
    /// <param name="_b">the number to check against</param>
    /// <returns>true if the numbers are multiples of each other</returns>
    static public bool IsMultiple(int _a, int _b)
    {
        return (_a % _b) == 0;
    }

    /// <summary>
    /// Gets a random int
    /// </summary>
    /// <param name="_min">min value</param>
    /// <param name="_max">max value</param>
    /// <returns>A random int</returns>
    static public int RandomInt(int _min, int _max)
    {
        return UnityEngine.Random.Range(_min, _max);
    }

    /// <summary>
    /// Gets a percentage of a value
    /// </summary>
    /// <param name="_initialValue">The value to get a percentage from</param>
    /// <param name="_percentage">The percentage to take off the main value</param>
    /// <returns>A new value with the percentage taken off the initial value</returns>
    static public float GetPercentageValue(float _initialValue, float _percentage)
    {
        float temp = _initialValue / 100f;
        float reverse = 100f - _percentage;
        temp *= reverse;
        return temp;
    }

    /// <summary>
    /// Increases a value by a set percentage
    /// </summary>
    /// <param name="_initialValue">The initial value to change</param>
    /// <param name="_increase">The percentage to increase the inital value by</param>
    /// <returns>A value increased by a set percentage</returns>
    static public float GetPercentageIncrease(float _initialValue, float _increase)
    {
        float temp = _initialValue / 100f;
        temp *= _increase;
        return _initialValue + temp;
    }

    /// <summary>
    /// Gets the percentage change value of a value
    /// </summary>
    /// <param name="_initialValue">The value to check</param>
    /// <param name="_increase">The percentage to increase the inital value by</param>
    /// <returns>The amount in a percentage the value has changed</returns>
    static public float GetPercentageChange(float _initialValue, float _increase)
    {
        float temp = _initialValue / 100f;
        temp *= _increase;
        return temp;
    }

    /// <summary>
    /// Calculates a win loss percentage between 100% and -100%
    /// </summary>
    /// <param name="playerOneWins"></param>
    /// <param name="playerTwoWins"></param>
    /// <returns></returns>
    static public double CalculateWinLossRatio(int playerOneWins, int playerTwoWins)
    {
        int totalGames = playerOneWins + playerTwoWins;

        if (totalGames == 0)
        {
            return 0; // Avoid division by zero
        }

        double winLossRatio = (double)(playerOneWins - playerTwoWins) / totalGames;
        return winLossRatio * 100;
    }
}