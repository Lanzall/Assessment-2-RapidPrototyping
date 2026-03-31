using UnityEngine;

public static class UIX
{
    static public Vector2 SetPivotPoints(UIPivotPosition _pos)
    {
        switch (_pos)
        {
            case UIPivotPosition.TopRight:
                return new Vector2(1,1);
            case UIPivotPosition.MiddleRight:
                return new Vector2(1,0.5f);
            case UIPivotPosition.BottomRight:
                return new Vector2(1,0);
            case UIPivotPosition.TopLeft:
                return new Vector2(0,1);
            case UIPivotPosition.MiddleLeft:
                return new Vector2(0,0.5f);
            case UIPivotPosition.BottomLeft:
                return new Vector2(0,0);
            case UIPivotPosition.MiddleTop:
                return new Vector2(0.5f,1);
            case UIPivotPosition.MiddleBottom:
                return new Vector2(0.5f,0);
            case UIPivotPosition.Centre:
                return new Vector2(0,0);
            default:
                return new Vector2(0,0);
            
        }
    }
}

public enum UIPivotPosition
{
    TopRight, MiddleRight, BottomRight, TopLeft, MiddleLeft, BottomLeft, MiddleTop, MiddleBottom, Centre
}