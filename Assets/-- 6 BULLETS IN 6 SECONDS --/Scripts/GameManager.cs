using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI clicksText;
    public TextMeshProUGUI resultsText;
    public Animator cigarAnimator;
    public Animator revolverAnimator;

    [Header("Game Settings")]
    public float timeLimit = 3f;
    public int maxClicks = 6;
    public int betweenQuestionsDelay = 3;

    private int num1, num2, correctAnswer;
    private string operatorSymbol;
    private float currentTime;
    private int clickCount = 0;
    private bool isAnswering = false;

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
        resultsText.text = "";
        GenerateNewQuestion();
    }

    void Update()
    {
        if (!isAnswering) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            EndQuestion();
        }
    }

    // This replaces the old Input.GetMouseButtonDown(0) logic
    private void OnClick(InputAction.CallbackContext context)
    {
        if (!isAnswering) return;
        if (clickCount < maxClicks)
        {
            clickCount++;
            clicksText.text = "" + clickCount;
            //Play shoot sound + animation here
            revolverAnimator.SetTrigger("CountUp");
        }
    }

    void GenerateNewQuestion()
    {
        clickCount = 0;
        currentTime = timeLimit;
        clicksText.text = "0";
        resultsText.text = "";
        cigarAnimator.SetTrigger("StartTimer");

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

        questionText.text = $"{num1} {operatorSymbol} {num2}";
        isAnswering = true;
    }

    void EndQuestion()
    {
        isAnswering = false;
        cigarAnimator.SetTrigger("EmptyTimer");

        bool isCorrect = clickCount == correctAnswer;

        questionText.text = "";

        if (isCorrect)
        {
            resultsText.text = "YOU WIN!";
            // play win sound, animation, enemy death fx
        }
        else
        {
            resultsText.text = "YOU LOSE!";
            // play lose sound, animation, player death fx
        }

        // Wait a few seconds, then asks a new question
        Invoke("GenerateNewQuestion", betweenQuestionsDelay);
    }
}
