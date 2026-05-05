using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;

    public float speed = 8f;
    private float moveZ;

    public void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        moveZ = v.x;

        // ignore if moving backwards
        if (moveZ < 0)
        {
            moveZ = 0;
        }
    }

    void Update()
    {
        transform.Translate(Vector3.forward * moveZ * speed * Time.deltaTime);

        // if moving, set animation to Running, else set to Idle
        if (moveZ > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
