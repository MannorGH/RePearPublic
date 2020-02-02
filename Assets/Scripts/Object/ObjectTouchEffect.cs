using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTouchEffect : MonoBehaviour
{
    [SerializeField]
    private EffectType thisEffectType;
    private bool isEffectActive = false;
    //private PlayerBehaviour birne;

    public float jumpForce = 1f;

    // Applies the object effect when the player touches the object.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitbox") && !isEffectActive)
        {
            // Effekt auslösen.
            switch (thisEffectType)
            {
                case EffectType.None:
                    {

                        break;
                    }
                case EffectType.Bouncy:
                    {
                        var rb = collision.collider.attachedRigidbody;
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                        break;
                    }
                case EffectType.Breaking:
                    {

                        break;
                    }
                case EffectType.Deadly:
                    {

                        break;
                    }
                case EffectType.Slippery:
                    {
                        //birne = collision.gameObject.GetComponent<PlayerBehaviour>();
                        //birne.speed = birne.speed * 3f;
                        //Invoke("SlideStop", 0.5f);
                        break;
                    }
                case EffectType.Sticky:
                    {
                        //var pb = collision.gameObject.GetComponent<PlayerBehaviour>();
                        //pb.speed = pb.speed / 3f;
                        //Invoke("StickStop", 0.5f);
                        break;
                    }
                default:
                    {
                        Debug.Log("Invalid EffectType!");
                        break;
                    }
            }

            isEffectActive = true;
        }
    }


    // Removes the object effect when the player stops touching the object.
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitbox") && isEffectActive)
        {
            // Effekt entfernen.
            switch (thisEffectType)
            {
                case EffectType.None:
                    {

                        isEffectActive = false;
                        break;
                    }
                case EffectType.Bouncy:
                    {

                        isEffectActive = false;
                        break;
                    }
                case EffectType.Breaking:
                    {

                        break;
                    }
                case EffectType.Deadly:
                    {

                        isEffectActive = false;
                        break;
                    }
                case EffectType.Slippery:
                    {
                        isEffectActive = false;
                        break;
                    }
                case EffectType.Sticky:
                    {
                        isEffectActive = false;
                        break;
                    }
                default:
                    {
                        Debug.Log("Invalid EffectType!");
                        break;
                    }
            }
        }
    }

    private void SlideStop()
    {
        //birne.speed = birne.speed / 3f;
        isEffectActive = false;
    }

    private void StickStop()
    {
        //birne.speed = birne.speed * 3f;
        isEffectActive = false;
    }


    // Types of objects.
    private enum EffectType
    {
        None, Bouncy, Breaking, Deadly, Slippery, Sticky
    }
}
