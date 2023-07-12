using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlScene : MonoBehaviour
{
    public Canvas canvasPause;
    private bool isPaused = false;
    public Canvas canvas;
    private CustomInput input;

    private void Awake()
    {
        input = new CustomInput();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Ingame.Settings.performed += OnPausePerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Ingame.Settings.performed -= OnPausePerformed;
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        TogglePause();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        canvasPause.gameObject.SetActive(true);
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        canvasPause.gameObject.SetActive(false);
    }

    public void LoadScenePlay()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    public void ReturnMenu()
    {
        canvas.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
