using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine.Events;

public class GeneralControlsPlayer : MonoBehaviour
{
    [Header("References")]
    public Animator P1animator;
    public CinemachineCamera CineCam;
    public GameObject opponent;

    [Header("Stats")]
    public int maxHealth = 3;
    public int currentHealth;

    [Header("Events")]      // Using this for triggering sound effects, vfx and ragdoll
    public UnityEvent onHit;
    public UnityEvent onBlock;
    public UnityEvent onDeath;

    private bool isFrontStance;
    private bool canAct;

    private Vector3 originalCamPos;
    private Tweener camShakeTween;

    void Start()
    {
        isFrontStance = true;
        canAct = true;
        originalCamPos = CineCam.gameObject.transform.localPosition;
    }

    void Update()
    {
        Debug.Log(canAct);
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

    public void RefreshOnIdle()
    {
        //When in Idle animation, resets canAct to true in case of damage during an attack animation, allowing the player to act again.
        canAct = true;
    }


    public void TakeDamage()
    {
        P1animator.SetTrigger("TakeDamage");
        Debug.Log("Damage taken!");
        camShakeTween?.Kill();
        camShakeTween = CineCam.gameObject.transform.DOShakePosition(.3f, 2f, 10, 90, false, true).OnComplete(() => ResetCamPos());
    }

    public void ResetCamPos()
    {
        CineCam.gameObject.transform.DOLocalMove(originalCamPos, .1f);
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
