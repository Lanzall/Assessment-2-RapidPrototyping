using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public static class InputX 
{
    public static void Vibrate(float _low, float _high, float _seconds = 0.1f)
    {
        Gamepad.current.SetMotorSpeeds(_low, _high);
    }
    public static void StopVibration()
    {
        Gamepad.current.SetMotorSpeeds(0, 0);
    }
    public static void SetLightColor(Color _color)
    {
        if(Gamepad.current is DualShockGamepad)
            DualShockGamepad.current.SetLightBarColor(_color);
    }
    /// <summary>
    /// Returns the int value from a numerical key
    /// https://forum.unity.com/threads/setting-an-integer-to-a-number-pressed.510688/
    /// </summary>
    /// <returns>An int based on the keyboard number pressed</returns>
    public static int GetPressedNumber()
    {
        for (var number = 0; number <= 9; number++)
        {
            if (Input.GetKeyDown(number.ToString()))
                return number;
        }
        return -1;
    }
}