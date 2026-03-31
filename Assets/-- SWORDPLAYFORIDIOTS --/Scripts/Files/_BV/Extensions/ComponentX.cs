using UnityEngine;

public static class ComponentX
{
    // Generic extension method to disable a component of any type
    // If no component then do nothing
    public static void Disable<T>(this GameObject _go) where T : MonoBehaviour
    {
        T component = _go.GetComponent<T>();
        if (component != null)
            component.enabled = false;
    }
}