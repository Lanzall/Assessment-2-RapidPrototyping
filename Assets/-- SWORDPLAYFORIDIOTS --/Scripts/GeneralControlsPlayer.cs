using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class GeneralControlsPlayer : MonoBehaviour
{
    [Header("References")]
    public Animator P1animator;
    public CinemachineCamera CineCam;

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

    public void PivotP1(InputAction.CallbackContext context)
    {
        if (canAct && context.performed)
        {
            P1animator.SetBool("isFrontStance", !P1animator.GetBool("isFrontStance"));
            isFrontStance = !isFrontStance;
        }
        
        
    }

    public void StabP1(InputAction.CallbackContext context)
    {
        if (canAct && context.performed)
        {
            P1animator.SetTrigger("Stab");

        }
    }

    public void SwingP1(InputAction.CallbackContext context)
    {
        if (canAct && context.performed)
        {
            P1animator.SetTrigger("Swing");

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
        P1animator.SetTrigger("TakeDamage");
        Debug.Log("Damage taken!");
        CineCam.gameObject.transform.DOShakePosition(.3f, 4f, 10, 90, false, true);
    }

    public void DefendDamage()
    {
        if (canAct)
        {
            P1animator.SetTrigger("Defend");
            Debug.Log("Damage Blocked!");
        }

        else
        {
            TakeDamage();
        }
    }
}
