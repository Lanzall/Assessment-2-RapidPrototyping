using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(BV.Range))]
public class RangeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect fullRect, SerializedProperty property, GUIContent label)
    {
        Rect r = EditorGUI.PrefixLabel(fullRect, label);

        EditorGUI.BeginChangeCheck();

        SerializedProperty minProp = property.FindPropertyRelative("min");
        SerializedProperty maxProp = property.FindPropertyRelative("max");
        float min = minProp.floatValue;
        float max = maxProp.floatValue;


        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;
        DrawRange(r, ref min, ref max);
        EditorGUI.indentLevel = indent;

        if (EditorGUI.EndChangeCheck())
        {
            minProp.floatValue = min;
            maxProp.floatValue = max;
        }
    }

    protected virtual void DrawRange(Rect r, ref float min, ref float max)
    {
        float valueSpacer = 20f;
        float valueWidth = (r.width - valueSpacer) / 2f;

        Rect r0 = new Rect(r.x, r.y, valueWidth, r.height);
        Rect r1 = new Rect(r.x + valueWidth, r.y, valueSpacer, r.height);
        Rect r2 = new Rect(r.x + r.width - valueWidth, r.y, valueWidth, r.height);

        min = EditorGUI.FloatField(r0, min);
        EditorGUI.LabelField(r1, "to", BV.Editor.centeredLabelStyle);
        max = EditorGUI.FloatField(r2, max);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return BV.Editor.lineHeight;
    }
}