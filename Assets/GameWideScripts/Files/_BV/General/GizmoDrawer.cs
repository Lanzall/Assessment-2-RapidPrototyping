using UnityEngine;
using UnityEditor;

public class GizmoDrawer : MonoBehaviour
{
    public enum GizmoType { Sphere, WireSphere, Cube, WireCube, Line, BoxCollider, SphereCollider }
    public GizmoType gizmoType;
    public Color color;
    public float radius = 1f;
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        switch (gizmoType)
        {
            case GizmoType.Sphere:
                Gizmos.DrawSphere(transform.position, radius);
                break;
            case GizmoType.WireSphere:
                Gizmos.DrawWireSphere(transform.position, radius);
                break;
            case GizmoType.Cube:
                Gizmos.DrawCube(transform.position, new Vector3(radius, radius, radius));
                break;
            case GizmoType.WireCube:
                Gizmos.DrawWireCube(transform.position, new Vector3(radius, radius, radius));
                break;
            case GizmoType.Line:
                Gizmos.DrawLine(transform.position, new Vector3(radius, radius, radius));
                break;
            case GizmoType.BoxCollider:
                if (TryGetComponent(out CapsuleCollider capsuleCollider))
                {
                    Gizmos.DrawWireSphere(capsuleCollider.bounds.center, capsuleCollider.radius);
                }
                if (TryGetComponent(out BoxCollider col))
                {
                    Gizmos.DrawWireCube(col.bounds.center, col.bounds.size);
                }
                break;
        }
        
    }
}


