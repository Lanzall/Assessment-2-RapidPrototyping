using System;
using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Based on: https://forum.unity3d.com/threads/generic-editor-array-propertyattribute-tools.240895/
namespace BV
{
	public class EnumListAttribute : PropertyAttribute
	{
		public readonly Type enumType;
		public readonly int startIndex;
		public EnumListAttribute (Type t) {
			enumType = t;
			startIndex = 0;
		}
		public EnumListAttribute (Type t, int i) {
			enumType = t;
			startIndex = i;
		}
	}
}
