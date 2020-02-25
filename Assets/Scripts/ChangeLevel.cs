using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField]
    private string nextLevelSceneName = "";
    private string menuSceneName = "Menu_Main";
    private string levelSelectSceneName = "Menu_LevelSelect";

    private float fadeDuration = 0.5f;
    [SerializeField]
    private Animator thisAnimator;

    public void InvokeLoadNextLevel()
    {
        thisAnimator.gameObject.SetActive(true);
        thisAnimator.SetTrigger("TriggerFadeOut");
        Invoke("LoadNextLevel", fadeDuration);
    }

    public void InvokeLoadMenu()
    {
        thisAnimator.gameObject.SetActive(true);
        thisAnimator.SetTrigger("TriggerFadeOut");
        Invoke("LoadMenu", fadeDuration);
    }

    public void InvokeLoadLevelSelect()
    {
        thisAnimator.gameObject.SetActive(true);
        thisAnimator.SetTrigger("TriggerFadeOut");
        Invoke("LoadLevelSelect", fadeDuration);
    }

    public void InvokeResetLevel()
    {
        thisAnimator.gameObject.SetActive(true);
        thisAnimator.SetTrigger("TriggerFadeOut");
        Invoke("ResetLevel", fadeDuration);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelSceneName);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene(levelSelectSceneName);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
