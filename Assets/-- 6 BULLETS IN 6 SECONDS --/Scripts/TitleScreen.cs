using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor.Timeline.Actions;
using System.Collections;
using Unity.VectorGraphics;

public class TitleScreen : MonoBehaviour
{
    [Header("References")]
    public Animator howToPlayAnim;
    public Canvas mainCanvas;

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

    public void StartGame()
    {
        SceneManager.LoadScene("6Bullets");
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
