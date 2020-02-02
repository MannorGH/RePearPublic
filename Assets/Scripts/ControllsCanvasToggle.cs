using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllsCanvasToggle : MonoBehaviour
{
    [SerializeField]
    private GameObject CanvasObject;
    [SerializeField]
    private GameObject MenuCanvas;
    public void ToggleCanvas(bool doShow)
    {
        if (doShow)
        {
            CanvasObject.SetActive(true);
            if (MenuCanvas != null)
            {
                MenuCanvas.GetComponent<PauseMenuLogic>().enableEscape = false;
            }
        } else
        {
            CanvasObject.SetActive(false);
            if (MenuCanvas != null)
            {
                MenuCanvas.GetComponent<PauseMenuLogic>().enableEscape = true;
            }
        }
    }
}
