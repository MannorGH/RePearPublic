using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject MenuUI;
    private bool isGamePaused = false;
    public bool enableEscape { private get; set; } = true;

    // Disables UI.
    private void Awake()
    {
        MenuUI.SetActive(false);
    }

    // Pauses the game on player input.
    private void Update()
    {
        if (enableEscape)
        {
            if (Input.GetKeyDown("escape"))
            {
                if (isGamePaused)
                {
                    DoTogglePause(false);
                }
                else
                {
                    DoTogglePause(true);
                }
            }
        }
    }

    public void DoTogglePause(bool doPause)
    {
        if (doPause)
        {
            Time.timeScale = 0.0f;
            MenuUI.SetActive(true);
            isGamePaused = true;
        }  else
        {
            Time.timeScale = 1.0f;
            MenuUI.SetActive(false);
            isGamePaused = false;
        }
    }
}
