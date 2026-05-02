using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEditor;
using UnityEngine.UI;
using Unity.Cinemachine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public GameObject pausePanel;
    private AudioPlayer audioPlayer;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highestScoreText;

    [Header("UI")]
    public TextMeshProUGUI clicksText;
    public TextMeshProUGUI resultsText;
    public TextMeshProUGUI readyText;
    public TextMeshProUGUI fireText;

    [Header("Cameras")]
    public CinemachineCamera cardsCam;
    public CinemachineCamera combatCam;

    //public Animator revolverAnimator;
    public Animator cowboyAnimator;

    [Header("Player Stats")]
    public int maxHP = 6;
    public int currentHP;
    public Image[] heartSprites;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public bool isCorrect;

    [Header("Game Settings")]
    public float timeLimit = 1f;
    public int maxClicks = 6;
    public int betweenQuestionsDelay = 3;

    private int num1, num2, correctAnswer;
    private int num1Display, num2Display; // For card display purposes
    private string operatorSymbol;
    private float currentTime;
    private int clickCount = 0;
    private bool isAnswering = false;

    [Header("Playing Cards")]
    public GameObject cardNum1;
    public GameObject cardOperator;
    public GameObject cardNum2;
    public Animator cardsAnimator;

    public Sprite[] numberCards; // Index 1 to 6
    public Sprite plusCard;
    public Sprite minusCard;

    public float revealDuration = 2f;

    // Add this for the new input system
    public InputAction clickAction;

    void OnEnable()
    {
        clickAction.Enable();
        clickAction.performed += OnClick;
    }

    void OnDisable()
    {
        clickAction.performed -= OnClick;
        clickAction.Disable();
    }

    void Start()
    {
        currentHP = maxHP;
        UpdateHearts();

        gameOverPanel.SetActive(false);

        resultsText.text = "";
        GenerateNewQuestion();

        // Setting UI text to be disabled first, add more if needed
        fireText.gameObject.SetActive(false);
        clicksText.gameObject.SetActive(false);

        audioPlayer = GetComponent<AudioPlayer>();

        if (audioPlayer == null)
        {
            Debug.LogError("AudioPlayer component missing from GameManager!");
        }
    }

    void Update()
    {
        if (!isAnswering) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0 && currentHP > 0)
        {
            EndQuestion();
        }

    }

    private void SwitchToCardsCam()
    {
        cardsCam.Priority = 10;
        combatCam.Priority = 1;
    }

    private void SwitchToCombatCam()
    {
        cardsCam.Priority = 1;
        combatCam.Priority = 10;
    }

    // This replaces the old Input.GetMouseButtonDown(0) logic
    private void OnClick(InputAction.CallbackContext context)
    {
        if (!isAnswering) return;
        if (pausePanel.activeInHierarchy) return;
        if (currentHP <= 0) return;
        if (clickCount < maxClicks)
        {
            clickCount++;
            clicksText.text = "" + clickCount;
            //Play shoot sound + animation here
            //revolverAnimator.SetTrigger("CountUp");
            cowboyAnimator.SetInteger("BulletCount", clickCount);
            audioPlayer.PlayClip("Spin");
            audioPlayer.PlayClipFromGroup("Prep", clickCount - 1);
        }
    }

    void GenerateNewQuestion()
    {
        if (currentHP <= 0) return;
        SwitchToCardsCam();
        readyText.gameObject.SetActive(true);
        clickCount = 0;
        currentTime = timeLimit;
        clicksText.text = "0";
        resultsText.text = "";

        //Generate simple maths: + or -, results always between 1 and 6
        bool isAddition = Random.value > 0.5f;

        if (isAddition)
        {
            num1 = Random.Range(1, 6);
            num2 = Random.Range(1, 7 - num1); // Ensure the sum is between 1 and 6
            correctAnswer = num1 + num2;
            operatorSymbol = "+";
        }
        else
        {
            num1 = Random.Range(2, 7); // Start from 2 to ensure a positive result
            num2 = Random.Range(1, num1); // Ensure the difference is between 1 and 6
            correctAnswer = num1 - num2;
            operatorSymbol = "-";
        }

        isAnswering = false;

        // Make sure card numbers are correct
        num1Display = num1 - 1; // Adjust for 0-based index
        num2Display = num2 - 1; // Adjust for 0-based index

        // Set the card sprites
        cardNum1.GetComponent<SpriteRenderer>().sprite = numberCards[num1Display];
        cardNum2.GetComponent<SpriteRenderer>().sprite = numberCards[num2Display];
        cardOperator.GetComponent<SpriteRenderer>().sprite = operatorSymbol == "+" ? plusCard : minusCard;

        // Deal the cards
        cardNum1.gameObject.SetActive(true);
        cardNum2.gameObject.SetActive(true);
        cardOperator.gameObject.SetActive(true);
        cardsAnimator.Play("CardsDealt");

        // Hide Player Sprites
        cowboyAnimator.gameObject.SetActive(false);

    // Start the reveal coroutine
    StartCoroutine(RevealCards());
    }

    private IEnumerator RevealCards()
    {
        yield return new WaitForSeconds(revealDuration);

        readyText.gameObject.SetActive(false);
        fireText.gameObject.SetActive(true);
        clicksText.gameObject.SetActive(true);

        audioPlayer.PlayClip("Start");

        isAnswering = true;
        SwitchToCombatCam();

        cowboyAnimator.gameObject.SetActive(true);
        cardsAnimator.Play("ReturnCards");

        // Wait for 6 frames then disable fireText
        yield return new WaitForSeconds(0.5f);
        fireText.gameObject.SetActive(false);

        cardNum1.gameObject.SetActive(false);
        cardNum2.gameObject.SetActive(false);
        cardOperator.gameObject.SetActive(false);

    }

    void EndQuestion()
    {
        isAnswering = false;

        isCorrect = clickCount == correctAnswer;

        if (isCorrect)
        {
            resultsText.text = "YOU WIN!";
        }
        else
        {
            int damage = Mathf.Abs(clickCount - correctAnswer);
            currentHP -= damage;
            if(currentHP < 0) currentHP = 0;

            UpdateHearts();

            if (currentHP <= 0)
            {
                GameOver();
            }

            resultsText.text = "YOU LOSE!";
            // play lose sound, animation, player death fx
        }

        StartCoroutine(PlayAimSequence());
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartSprites.Length; i++)
        {
            heartSprites[i].sprite = i < currentHP ? fullHeart : emptyHeart;
        }
    }

    private IEnumerator PlayAimSequence()
    {
        cowboyAnimator.SetTrigger("Aiming");

        // Wait until the aiming animation is playing before proceeding
        yield return new WaitUntil(() =>
        cowboyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Aiming"));

        // Wait for the aiming animation to finish
        yield return new WaitForSeconds(cowboyAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Hide the cowboy after the animation
        cowboyAnimator.gameObject.SetActive(false);

        if (isCorrect)
        {
            // Play the Hit animation
        }
        else
        {
            // Play the Missfire animation
        }
    }

    public void GameOver()
    {
        isAnswering = false;
        gameOverPanel.SetActive(true);
    }
}
