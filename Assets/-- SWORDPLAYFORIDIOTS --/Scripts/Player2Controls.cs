using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Controls : MonoBehaviour
{
    [Header("References")]
    public Animator animator;

    [Header("Attributes")]
    [SerializeField]
    public int Health = 3;

    private bool isFrontStance;
    private bool canAct;

    void Start()
    {
        isFrontStance = true;
        canAct = true;
        
    }

    void Update()
    {

    }

    public void Pivot(InputAction.CallbackContext context)
    {
        if (canAct && context.performed)
        {
            animator.SetBool("isFrontStance", !animator.GetBool("isFrontStance"));
            isFrontStance = !isFrontStance;
        }
        
        
    }

    public void Stab(InputAction.CallbackContext context)
    {
        if (canAct && context.performed)
        {
            animator.SetTrigger("Stab");

        }
    }

    public void Swing(InputAction.CallbackContext context)
    {
        if (canAct && context.performed)
        {
            animator.SetTrigger("Swing");

        }
    }

    public void ActionEnabler()
    {
        canAct = !canAct;
    }

    public void IncomingStab()
    {
        if (isFrontStance)
        {
            //TakeDamage
            TakeDamage();
        }

        else
        {
            //DefendAnimation
            DefendDamage();
        }
    }

    public void IncomingSwing()
    {
        if (isFrontStance)
        {
            //DefendAnimation
            DefendDamage();
        }
        else
        {
            //TakeDamage
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        animator.SetTrigger("TakeDamage");
        Debug.Log("Damage taken!");
    }

    public void DefendDamage()
    {
        if (canAct)
        {
            animator.SetTrigger("Defend");
            Debug.Log("Damage Blocked!");
        }

        else
        {
            TakeDamage();
        }
    }
}
