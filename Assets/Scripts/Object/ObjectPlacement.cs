using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacement : MonoBehaviour
{
    [SerializeField]
    private GrabState currentGrabState;
    private Vector3 initialPosition = Vector3.zero;

    [SerializeField]
    private Collider2D thisCollider;
    [SerializeField]
    private Rigidbody2D thisRigidbody;
    [SerializeField]
    private SpriteRenderer thisSpriteRenderer;

    [SerializeField]
    private AudioClip dropAudio;

    [SerializeField]
    public GameObject ItemBoxPrefab;
    public GameObject attachedItemBoxObject { private get; set; }

    private Camera mainCamera;
    private Plane dragPlane;
    private Vector3 dragOffset;
    private AudioSource audioPlayer;

    private float rotationScaleFactor = 22.5f; // Degrees of rotation per scroll whell tick.

    // Sets up variables.
    private void Awake()
    {
        mainCamera = Camera.main;
        initialPosition = this.transform.position;
        audioPlayer = GetComponent<AudioSource>();
    }

    // Tests whether the object's collider is touching any other colliders.
    private bool testCanBePlaced()
    {
        if (thisCollider.IsTouchingLayers(LayerMask.GetMask("Default")))
        {
            return false;
        } else
        {
            return true;
        }
    }

    // Moves the object to the mouse position when it is grabbed.
    private void Update()
    {
        if (currentGrabState == GrabState.Held) {
            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            float planeDist;
            dragPlane.Raycast(camRay, out planeDist);
            transform.position = camRay.GetPoint(planeDist) + dragOffset;

            if (Input.GetMouseButtonDown(0)) // Clicking while an object is held.
            {
                if (testCanBePlaced())
                {
                    thisCollider.isTrigger = false;
                    currentGrabState = GrabState.Placed;
                    this.gameObject.layer = 0;
                    attachedItemBoxObject.GetComponent<Button>().interactable = false;
                    attachedItemBoxObject.GetComponent<ItemBoxLogic>().StartRemove();
                    audioPlayer.PlayOneShot(dropAudio, 1f);
                }
                else
                {
                    this.transform.position = initialPosition;
                    currentGrabState = GrabState.Grabable;
                    attachedItemBoxObject.GetComponent<ItemBoxLogic>().ToggleItemImage(true);
                }
                thisSpriteRenderer.sortingLayerName = "Default";
                thisRigidbody.bodyType = RigidbodyType2D.Static;
            }

            if (Input.GetMouseButtonDown(1))
            {
                this.transform.position = initialPosition;
                currentGrabState = GrabState.Grabable;
                attachedItemBoxObject.GetComponent<ItemBoxLogic>().ToggleItemImage(true);

                thisSpriteRenderer.sortingLayerName = "Default";
                thisRigidbody.bodyType = RigidbodyType2D.Static;
            }

            if (Input.mouseScrollDelta.y != 0.0f)
            {
                this.transform.RotateAround(this.transform.position, Vector3.back, Input.mouseScrollDelta.y * rotationScaleFactor);
            }
        }
    }

    // Tries to attach the object corresponding to the item box to the cursor.
    public void grabObject()
    {
        if (currentGrabState == GrabState.Grabable)
        {
            attachedItemBoxObject.GetComponent<ItemBoxLogic>().ToggleItemImage(false);

            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, this.transform.position.z);

            dragPlane = new Plane(mainCamera.transform.forward, this.transform.position);
            Ray camRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            float planeDistance;
            dragPlane.Raycast(camRay, out planeDistance);
            dragOffset = transform.position - camRay.GetPoint(planeDistance);

            thisRigidbody.bodyType = RigidbodyType2D.Dynamic;
            thisRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

            thisSpriteRenderer.sortingLayerName = "HeldObject";
            currentGrabState = GrabState.Held;
        }
    }

    // States of the object.
    private enum GrabState
    {
        Ungrabable, Grabable, Held, Placed
    }
}
