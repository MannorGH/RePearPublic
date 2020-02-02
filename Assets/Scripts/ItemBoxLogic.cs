using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBoxLogic : MonoBehaviour
{
    public GameObject attachedItemObject { private get; set; }
    [SerializeField]
    private Image itemImage;
    private Image thisImage;

    [SerializeField]
    private float fadeOutTime = 1.0f;
    private Color initialColour;
    private bool doFadeOut = false;
    private float FadeOutProgress = 0.0f;

    // Initializes variables.
    private void Awake()
    {
        thisImage = this.GetComponent<Image>();
        initialColour = thisImage.color;
    }

    // Triggers grabObject function in the corresponding item object, called in the OnClick()-event.
    public void ForwardOnClick()
    {
        attachedItemObject.GetComponent<ObjectPlacement>().grabObject();
    }

    // Enables or disables the item image. 
    public void ToggleItemImage(bool doEnable)
    {
        if (doEnable)
        {
            itemImage.enabled = true;
        } else
        {
            itemImage.enabled = false;
        }
    }

    // Removes the item box from the conveyor belt.
    public void StartRemove()
    {
        doFadeOut = true;
        Invoke("RemoveFull", fadeOutTime); // Box is removed after it has completely faded.
    }

    //********************
    private void Update()
    {
        if (doFadeOut)
        {
            FadeOutProgress += Time.deltaTime / fadeOutTime;
            thisImage.color = Color.Lerp(initialColour, Color.clear, FadeOutProgress);
        }
    }

    //*******************************
    private void RemoveFull()
    {
        ConveyorBeltLogic.instance.BeltItems.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}
