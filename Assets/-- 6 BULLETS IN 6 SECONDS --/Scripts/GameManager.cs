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
    public GameObject reticule;
    public GameObject enemy;
    public Animator enemyAnimator;
    private AudioPlayer audioPlayer;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highestScoreText;

    [Header("UI")]
    public TextMeshProUGUI resultsText;
    public TextMeshProUGUI readyText;
    public TextMeshProUGUI fireText;

    [Header("Cameras")]
    public CinemachineCamera cardsCam;
    public CinemachineCamera combatCam;

    public Animator cowboyAnimator;
    public Animator revolverAnimator;
    public string[] spinAnimations;

    [Header("Player Stats")]
    public int maxHP = 6;
    public int currentHP;
    public Image[] heartSprites;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public bool isCorrect;
    public int score = 0;

    [Header("Game Settings")]
    public float timeLimit = 1f;
    public int maxClicks = 6;
    public float betweenQuestionsDelay = 1.5f;

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

    [Header("Shooting Animations")]
    public Animator reticleAnimation;
    public string[] hitAnimations;
    public string[] missAnimations;
    public string missfireAnimation;

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

        enemy.SetActive(false);
        reticule.SetActive(false);

        resultsText.text = "";
        GenerateNewQuestion();

        // Setting UI text to be disabled first, add more if needed
        fireText.gameObject.SetActive(false);
        audioPlayer = GetComponent<AudioPlayer>();

        if (audioPlayer == null)
        {
            Debug.LogError("AudioPlayer component missing from GameManager!");
        }

        // Load Previous Scores
        int LastScore = PlayerPrefs.GetInt("LastScore", 0);
        int HighestScore = PlayerPrefs.GetInt("HighestScore", 0);

        highestScoreText.text = "* was previously worth $ " + HighestScore;
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

        enemy.SetActive(false);
    }

    private void SwitchToCombatCam()
    {
        cardsCam.Priority = 1;
        combatCam.Priority = 10;

        enemy.SetActive(true);
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
            cowboyAnimator.SetInteger("BulletCount", clickCount);
            audioPlayer.PlayClip("Spin");
            audioPlayer.PlayClipFromGroup("Prep", clickCount - 1);
        }
        revolverAnimator.Play(spinAnimations[clickCount - 1]);
    }

    void GenerateNewQuestion()
    {
        if (currentHP <= 0) return;
        reticule.SetActive(false);
        SwitchToCardsCam();
        readyText.gameObject.SetActive(true);
        clickCount = 0;
        currentTime = timeLimit;
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

        audioPlayer.PlayClip("Start");

        isAnswering = true;
        SwitchToCombatCam();

        cowboyAnimator.gameObject.SetActive(true);
        enemyAnimator.Play("Idle");
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

        reticule.SetActive(true);

        if (isCorrect)
        {
            reticleAnimation.Play(hitAnimations[clickCount - 1]);
        }
        else
        {
            if (clickCount == 0)
            {
                reticleAnimation.Play(missfireAnimation);
            }
            else
            {
                reticleAnimation.Play(missAnimations[clickCount - 1]);
            }
                
        }

        yield return new WaitForSeconds(reticleAnimation.GetCurrentAnimatorStateInfo(0).length);

        if (isCorrect)
        {
            //Awesome
            resultsText.text = "HIT!";
            enemyAnimator.Play("Death");
            score++;
        }
        else
        {
            int damage = Mathf.Abs(clickCount - correctAnswer);
            currentHP -= damage;
            if (currentHP < 0) currentHP = 0;

            UpdateHearts();

            if (currentHP <= 0)
            {
                GameOver();
            }

            if (clickCount == 0)
            {
                resultsText.text = "TOO LATE!";
            }
            else

                resultsText.text = "MISS!";
        }
        Invoke("GenerateNewQuestion", betweenQuestionsDelay);
    }

    public void GameOver()
    {
        finalScoreText.text = "$ " + score + "*";
        isAnswering = false;
        gameOverPanel.SetActive(true);

        //Saving the recent score
        PlayerPrefs.SetInt("LastScore", score);

        // Update the score if needed
        int highest = PlayerPrefs.GetInt("HighestScore", 0);
        if (score > highest)
        {
            PlayerPrefs.SetInt("HighestScore", score);
        }

        PlayerPrefs.Save();
    }
}
