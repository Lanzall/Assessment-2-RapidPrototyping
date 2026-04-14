using System;
using UnityEditor;
using UnityEngine;

namespace BV
{
    [System.Serializable]
    public struct Range : IEquatable<Range>
    {
        public float min;
        public float max;
        public float length { get { return (max - min); } }

        public Range(float v0, float v1)
        {
            min = v0;
            max = v1;
        }
        
        // Virtuals
        public override string ToString()
        {
            return "(" + StringX.FormatFloat(min, 3) + "," + StringX.FormatFloat(max, 3) + ")";
        }

        // IEquatable
        public bool Equals(Range other)
        {
            //			if(other == null) return false;
            return (other.min == min && other.max == max);
        }
        public override bool Equals(System.Object obj)
        {
            if (obj == null) return false;
            Range other = (Range)obj;
            return (other.min == min && other.max == max);
        }
        public static bool operator ==(Range r0, Range r1)
        {
            //			if (r0 == null && r1==null)
            //				return true;
            //			if (r0 == null || r1 == null)
            //				return false;
            return r0.Equals(r1);
        }
        public static bool operator !=(Range r0, Range r1)
        {
            return !(r0 == r1);
        }
        public override int GetHashCode()
        {
            return new Vector2(min, max).GetHashCode();
        }
        public static Range operator *(Range r, float m)
        {
            return new Range(r.min * m, r.max * m);
        }
    }
}