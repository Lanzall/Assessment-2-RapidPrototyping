using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    public GameObject pausePanel;
    private bool paused;

    public InputAction pauseGameAction;

    private void OnEnable()
    {
        pauseGameAction.Enable();
        pauseGameAction.performed += OnPause;
    }

    private void OnDisable()
    {
        pauseGameAction.performed -= OnPause;
        pauseGameAction.Disable();
    }

    void Start()
    {
        paused = false;
        pausePanel.SetActive(paused);
        Time.timeScale = 1;
    }


    public void OnPause(InputAction.CallbackContext context)
    {
        Pause();
    }

    public void Pause()
    {
        paused = !paused;
        pausePanel.SetActive(paused);
        Time.timeScale = paused ? 0 : 1;
    }
}
