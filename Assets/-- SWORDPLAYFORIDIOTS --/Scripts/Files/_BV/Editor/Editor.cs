using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

namespace BV
{
	public class Editor
	{
		// Ident amount
		static public float indentAmount	{ get { return 15f; } }

		// Height of one line in the Inspector
		static public  float lineHeight		{ get { return EditorGUIUtility.singleLineHeight; } }

		static int bigFontSize = 14;
		static public GUILayoutOption bigFontLayout { get { return GUILayout.Height(bigFontSize+4); } }
		static GUIStyle _bigFontStyle;
		static public GUIStyle bigFontStyle {
			get {
				if (_bigFontStyle == null)
				{
					_bigFontStyle = new GUIStyle(GUI.skin.label);
					_bigFontStyle.fontSize = bigFontSize;
				}
				return _bigFontStyle;
			}
		}

		static GUIStyle _readOnlyFieldStyle;
		static public GUIStyle readOnlyFieldStyle {
			get {
				if (_readOnlyFieldStyle == null)
				{
					_readOnlyFieldStyle = new GUIStyle("TextField");
					_readOnlyFieldStyle.normal.textColor = Color.white;
				}
				return _readOnlyFieldStyle;
			}
		}

		static GUIStyle _editableFieldStyle;
		static public GUIStyle editableFieldStyle {
			get {
				if (_editableFieldStyle == null)
				{
					_editableFieldStyle = new GUIStyle("TextField");
					_editableFieldStyle.normal.textColor = Color.yellow;
				}
				return _editableFieldStyle;
			}
		}


		static GUIStyle _centeredLabelStyle;
		static public GUIStyle centeredLabelStyle {
			get {
				if (_centeredLabelStyle == null)
				{
					_centeredLabelStyle = new GUIStyle("Label");
					_centeredLabelStyle.alignment = TextAnchor.MiddleCenter;
				}
				return _centeredLabelStyle;
			}
		}
		// Get a rect at line X
		static public Rect GetLineRect(Rect fullRect, int lineStart, int lineCount=1)
		{
			Rect r = fullRect;
			r.y += BV.Editor.lineHeight * lineStart;
			r.height = BV.Editor.lineHeight * lineCount;
			return r;
		}

		// Draw an idented inspector
		static public void DrawProperty(Rect position, SerializedProperty property, int line, int lineCount = 1)
		{
			Rect r = GetLineRect(position,(line++),lineCount);
			EditorGUI.PropertyField(r, property);
		}
		static public void DrawProperty(Rect position, SerializedProperty property, string propName, int line, int lineCount=1)
		{
			Rect r = GetLineRect(position,(line++),lineCount);
			EditorGUI.PropertyField(r, property.FindPropertyRelative(propName));
		}

		// Draw an idented inspector
		static public void DrawIndented(Rect fullRect, SerializedProperty property, string propName, string label, int indent, int line)
		{
			EditorGUI.indentLevel = indent;
			Rect position = GetLineRect(fullRect, line);
			if (!String.IsNullOrEmpty(label))
			{
				position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent(label));
				EditorGUI.indentLevel = 0;
			}
			EditorGUI.PropertyField(position, property.FindPropertyRelative(propName), GUIContent.none);
		}

		// Draw some property, always expanded (open)
		static public void PropertyFieldExpanded(Rect r, SerializedProperty property, string fieldName)
		{
			SerializedProperty p = property.FindPropertyRelative(fieldName);
			p.isExpanded = true;
			EditorGUI.PropertyField(r, p, true);
		}

		// Make a simple Color texture
		static public Texture2D MakeTexture(Color c, int w=1, int h=1)
		{
			w = Mathf.Max(1,w);
			h = Mathf.Max(1,h);
			Texture2D tex = new Texture2D(w,h);
			tex.SetPixel(0,0,c);
			tex.Apply();
			return tex;
		}

		// Make a simple Color texture
		// Must be at "Assets/Resources/"
		static public Texture LoadTexture(string path)
		{
			return (Texture)Resources.Load(path);
		}

		// Get the index of a SerializedProperty whe its inside an array or list
		public static int GetPropertyArrayIndex(SerializedProperty property)
		{
			var pattern = @"\[[0-9\.]+\]$";
			var matches = Regex.Matches(property.propertyPath, pattern);
			if (matches.Count > 0)
			{
				string str = matches[0].ToString();
				str = str.Substring(1,str.Length-2);
				return Int32.Parse(str);
			}
			return 0;
		}

	}
}
