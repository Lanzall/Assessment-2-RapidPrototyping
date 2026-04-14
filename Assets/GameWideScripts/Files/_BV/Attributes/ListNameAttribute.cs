using System;
using System.Collections;
using UnityEngine;

// Based on: https://forum.unity3d.com/threads/generic-editor-array-propertyattribute-tools.240895/
namespace BV
{
	public class ListNameAttribute : PropertyAttribute
	{
		public string title;
		public int startIndex;
		public ListNameAttribute () {
			startIndex = 0;
		}
		public ListNameAttribute (int i) {
			startIndex = i;
		}
        public ListNameAttribute(string s)
        {
			title = s;
            startIndex = 0;
        }
    }
}
