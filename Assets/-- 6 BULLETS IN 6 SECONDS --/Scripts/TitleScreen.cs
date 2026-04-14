using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor.Timeline.Actions;
using System.Collections;

public class TitleScreen : MonoBehaviour
{
    [Header("References")]
    public Animator howToPlayAnim;
    public Animator fadeController;
    public Canvas mainCanvas;
    public Canvas fadeCanvas;


    public InputAction closeWindowAction;

    private bool helpWindowOpen;

    private void OnEnable()
    {
        closeWindowAction.Enable();
        closeWindowAction.performed += OnCloseWindow;

        howToPlayAnim.SetTrigger("isClosed");
        helpWindowOpen = false;
    }

    void Update()
    {
        
    }

    public void FadeComplete()
    {
        fadeCanvas.enabled = false;
    }

    public void StartGame()
    {
        fadeCanvas.enabled = true;
        StartCoroutine(StartingGame("6Bullets"));
    }

    private IEnumerator StartingGame(string sceneName)
    {
        fadeController.SetTrigger("FadeEnable");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }

    public void HowToPlay()
    {
        howToPlayAnim.SetTrigger("isOpened");
        helpWindowOpen = true;
        mainCanvas.enabled = false;
    }

    public void OnCloseWindow(InputAction.CallbackContext context)
    {
        if (!helpWindowOpen) return;
        howToPlayAnim.SetTrigger("isClosed");
        helpWindowOpen = false;
        mainCanvas.enabled = true;
    }

    public void QuitToMenu()
    {
        Application.Quit();
    }
}
