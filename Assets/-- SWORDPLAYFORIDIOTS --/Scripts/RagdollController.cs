using UnityEngine;

public class RagdollController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody[] ragdollBodies;
    public Collider[] ragdollColliders;
    public Rigidbody mainBody;
    public Collider mainCollider;

    private void Awake()
    {
        // Disable ragdoll physics at the start
        SetRagdoll(false);
    }


    public void SetRagdoll(bool enabled)
    {
        animator.enabled = !enabled;

        foreach (var rb in ragdollBodies)
        {
            rb.isKinematic = !enabled;
        }

        foreach (var col in ragdollColliders)
        {
            col.enabled = enabled;
        }

        mainBody.isKinematic = enabled;
        mainCollider.enabled = !enabled;
    }
}
