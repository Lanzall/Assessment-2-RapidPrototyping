using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class GeneralControlsPlayer : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public CinemachineCamera CineCam;
    public GeneralControlsPlayer opponent;

    public CinemachineCamera player1DeathCam;
    public CinemachineCamera player2DeathCam;

    [Header("Stats")]
    public int maxHealth = 3;
    public int currentHealth;
    public Image[] heartSprites;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    [Header("Events")]      // Using this for triggering sound effects, vfx and ragdoll
    public UnityEvent onHit;
    public UnityEvent onBlock;
    public UnityEvent onDeath;

    private bool isFrontStance;
    public bool canAct;
    public bool isDead = false;

    private Vector3 originalCamPos;
    private Tweener camShakeTween;

    void Start()
    {
        isFrontStance = true;
        canAct = true;
        originalCamPos = CineCam.gameObject.transform.localPosition;
        currentHealth = maxHealth;

        CineCam.Priority = 10;   // Setting the main camera to have higher priority than the death cams at the start of the game, so it will be active
        player1DeathCam.Priority = 0;
        player2DeathCam.Priority = 0;

    }

    void Update()
    {
        if (opponent.isDead)
        {
            canAct = false;
        }
    }

    public void Pivot(InputAction.CallbackContext context)
    {
        if (canAct && context.performed)
        {
            animator.SetBool("isFrontStance", !animator.GetBool("isFrontStance"));
            isFrontStance = !isFrontStance;
        }
        
        
    }

    public void Stab(InputAction.CallbackContext context)   // STAB ANIMATION ACTION -- This only handles the animation triggering, the actual hit check occurs from the following trigger from the animation
    {
        if (canAct && context.performed)
        {
            animator.SetTrigger("Stab");

        }
    }

    public void StabHit()   // STAB HIT CHECK -- This is triggered from the animation, checking if they're in the correct stance, and will call the opponent's TakeDamage() function if they are, or DefendDamage() if they're not.
    {
        if (opponent == null) return;    // In case the opponent reference is not set, to avoid errors

        if (opponent.isFrontStance)
        {
            opponent.TakeDamage();
            onHit.Invoke();     // Invoking the Unity Event when hitting the opponent, for SFX and VFX
        }

        else
        {
            opponent.DefendDamage();
            onBlock.Invoke();   // Invoking the Unity Event when blocking the attack, for SFX and VFX
        }
    }

    public void Swing(InputAction.CallbackContext context)
    {
        if (canAct && context.performed)
        {
            animator.SetTrigger("Swing");

        }
    }

    public void SwingHit()
    {
        if (opponent == null) return;

        if (!opponent.isFrontStance)
        {
            opponent.TakeDamage();
            onHit.Invoke();
        }
        else
        {
            opponent.DefendDamage();
            onBlock.Invoke();
        }
    }

    public void ActionEnabler()
    {
        canAct = !canAct;
    }

    public void RefreshOnIdle()
    {
        //When in Idle animation, resets canAct to true in case of damage during an attack animation, allowing the player to act again.
        if (opponent.isDead) return;
        canAct = true;

    }


    public void TakeDamage()
    {
        currentHealth--;    // -- reduces the health by 1
        if(currentHealth <= 0)
        {
            Die();
        }

        animator.SetTrigger("TakeDamage");
        Debug.Log("Damage taken!");
        camShakeTween?.Kill();
        camShakeTween = CineCam.gameObject.transform.DOShakePosition(.3f, 2f, 10, 90, false, true).OnComplete(() => ResetCamPos());

        UpdateHearts();
    }

    public void ResetCamPos()
    {
        CineCam.gameObject.transform.DOLocalMove(originalCamPos, .1f);
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

    void UpdateHearts()
    {
        for (int i = 0; i < heartSprites.Length; i++)
        {
            heartSprites[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }

    public void Die()
    {
        isDead = true;
        canAct = false;
        opponent.canAct = false;

        // Switch cameras to dead player
        if (gameObject.name == "Player1")
        {
            player1DeathCam.Priority = 10;
            CineCam.Priority = 0;
            player2DeathCam.Priority = 0;
        }
        else if (gameObject.name == "Player2")
        {
            player2DeathCam.Priority = 10;
            CineCam.Priority = 0;
            player1DeathCam.Priority = 0;
        }

        StartCoroutine(DeathSlowdown());   // Start the slowdown effect when the player dies

        onDeath.Invoke();   // Invoking the Unity Event when the player dies, for SFX, VFX and ragdoll
        Debug.Log("Player has died!");
        return;
    }

    public IEnumerator DeathSlowdown()
    {
        DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0.3f, 0f);
            yield return new WaitForSecondsRealtime(1f);
            Time.timeScale = 1f;
        yield return null;
    }
}
