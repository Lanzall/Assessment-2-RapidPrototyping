using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public static class ColorX 
{
    /// <summary>
    /// Applies intensity to a Color. 0f returns baseColor, 1f returns Color.white
    /// </summary>
    /// <returns>The new color.</returns>
    /// <param name="baseColor">Base color.</param>
    /// <param name="intensity">Intensity.</param>
    static public Color ApplyColorIntensity(Color baseColor, float intensity)
    {
        float i = 1f - intensity;
        float r = Mathf.Clamp01(baseColor.r + i);
        float g = Mathf.Clamp01(baseColor.g + i);
        float b = Mathf.Clamp01(baseColor.b + i);
        return new Color(r, g, b);
    }

    /// <summary>
    /// Applies alpha to a Color.
    /// </summary>
    /// <returns>New Color with alpha.</returns>
    /// <param name="baseColor">Base color.</param>
    /// <param name="alpha">Alpha vlaue.</param>
    static public Color ApplyAlpha(Color baseColor, float alpha)
    {
        return new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
    }

    /// <summary>
    /// Colorizes a ParticleSystem.
    /// </summary>
    /// <param name="partSys">the ParticleSystem.</param>
    /// <param name="newColor">the color.</param>
    static public void ColorizeParticles(ParticleSystem partSys, Color newColor)
    {
        if (partSys != null)
        {
            var main = partSys.main;
            main.startColor = newColor;
        }
    }

    /// <summary>
    /// Colorizes a MeshRenderer
    /// </summary>
    /// <param name="mesh">The MeshRenderer.</param>
    /// <param name="newColor">The color.</param>
    static public void ColorizeMesh(MeshRenderer mesh, Color newColor)
    {
        if (mesh != null)
        {
            if (mesh.material.HasProperty("_Color"))
                mesh.material.SetColor("_Color", newColor);
            else if (mesh.material.HasProperty("_TintColor"))
                mesh.material.SetColor("_TintColor", newColor);
        }
    }

    /// <summary>
    /// Sets the color property of a Component
    /// </summary>
    /// <param name="comp">some generic Component</param>
    /// <param name="newColor">New color.</param>
    static public void SetColor(Component comp, Color newColor)
    {
        PropertyInfo prop = comp.GetType().GetProperty("color");
        if (prop != null)
            prop.SetValue(comp, newColor, null);
    }
    /// <summary>
    /// Sets the alpha property of a compnent
    /// </summary>
    /// <returns>The previous alpha value</returns>
    /// <param name="comp">some generic Component</param>
    /// <param name="newAlpha">New alpha.</param>
    static public float SetAlpha(Component comp, float newAlpha)
    {
        PropertyInfo prop = comp.GetType().GetProperty("color");
        if (prop == null)
            return 0f;
        Color c = (Color)prop.GetValue(comp, null);
        float oldAlpha = c.a;
        c.a = newAlpha;
        prop.SetValue(comp, c, null);
        return oldAlpha;
    }

    /// <summary>
    /// Changes the alpha value of a Sprite Renderer. Default is 0 (transparent)
    /// </summary>
    /// <param name="_sr">The Sprite Renderer</param>
    /// <param name="_to">The value to.</param>
    static public void ChangeAlpha(SpriteRenderer _sr, float _to = 0)
    {
        Color temp = _sr.color;
        temp.a = _to;
        _sr.color = temp;
    }

    /// <summary>
    /// Changes the alpha value of an Image. Default is 0 (transparent)
    /// </summary>
    /// <param name="_sr">The Image to change</param>
    /// <param name="_to">The value to. </param>
    static public void ChangeAlpha(Image _image, float _to = 0)
    {
        Color temp = _image.color;
        temp.a = _to;
        _image.color = temp;
    }

    /// <summary>
    /// Changes the alpha value of a Renderers Material. Default is 0 (transparent)
    /// </summary>
    /// <param name="_sr">The Renderer</param>
    /// <param name="_to">The value to.</param>
    static public void ChangeAlpha(Renderer _ren, float _to = 0)
    {
        Color temp = _ren.material.color;
        temp.a = _to;
        _ren.material.color = temp;
    }

    static public string GetColorHex(Color _color)
    {
        return ColorUtility.ToHtmlStringRGBA(_color);
    }
}
