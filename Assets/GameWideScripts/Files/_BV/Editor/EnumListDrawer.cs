using System;
using UnityEditor;
using UnityEngine;

namespace BV
{
    [CustomPropertyDrawer(typeof(EnumListAttribute))]
    public class EnumListDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EnumListAttribute enumListAttrib = attribute as EnumListAttribute;

            int index = BV.Editor.GetPropertyArrayIndex(property);
            string name = GetEnumNameByValue(enumListAttrib.enumType, index);
            string str = (index + enumListAttrib.startIndex).ToString() + ": " + name;
            label = new GUIContent(str);

            //		int indent = EditorGUI.indentLevel;
            //		Rect rc = position;
            //		rc = EditorGUI.PrefixLabel(rc, label);
            //		EditorGUI.indentLevel = 0;
            //		EditorGUI.PropertyField(rc, property, GUIContent.none, true);
            //		EditorGUI.indentLevel = indent;

            EditorGUI.PropertyField(position, property, label, true);
        }

        static string GetEnumNameByValue(Type enumType, int value)
        {
            foreach (var v in Enum.GetValues(enumType))
                if ((int)v == value)
                    return v.ToString();
            return "???";
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property);
        }
    }
}
