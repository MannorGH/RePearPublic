using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTouchEffect : MonoBehaviour
{
    [SerializeField]
    private AudioClip bounceAudio;

    [SerializeField]
    private EffectType thisEffectType;
    private PlayerBehaviour birne;
    private Animator anim;
    private AudioSource audioPlayer;

    public float jumpForce = 1f;

    private bool OneTimeEffectWasTriggered = false;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();

    }

    // Applies the object effect when the player touches the object.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitbox"))
        {
            birne = collision.gameObject.GetComponent<PlayerBehaviour>();
            // Effekt auslösen.
            switch (thisEffectType)
            {
                case EffectType.None:
                    {

                        break;
                    }
                case EffectType.Bouncy:
                    {
                        birne.Jump(jumpForce, Vector2.up);
                        audioPlayer.PlayOneShot(bounceAudio, 1f);
                        break;
                    }
                case EffectType.Breaking:
                    {
                        if (!OneTimeEffectWasTriggered) {
                            anim.SetTrigger("Break");
                            Invoke("CookieDestroyer", 1f);
                            OneTimeEffectWasTriggered = true;
                        }
                        break;
                    }
                case EffectType.Deadly:
                    {
                        if (!OneTimeEffectWasTriggered)
                        {
                            birne.Kill();
                            OneTimeEffectWasTriggered = true;
                        }
                        break;
                    }
                case EffectType.Slippery:
                    {
                        birne.SpeedUp();
                        break;
                    }
                case EffectType.Sticky:
                    {
                        birne.StickUp();
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


    // Removes the object effect when the player stops touching the object.
    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("PlayerHitbox") && birne.isEffectActive)
        //{
        //    // Effekt entfernen.
        //    switch (thisEffectType)
        //    {
        //        case EffectType.None:
        //            {

        //                birne.isEffectActive = false;
        //                break;
        //            }
        //        case EffectType.Bouncy:
        //            {

        //                birne.isEffectActive = false;
        //                break;
        //            }
        //        case EffectType.Breaking:
        //            {

        //                break;
        //            }
        //        case EffectType.Deadly:
        //            {

        //                birne.isEffectActive = false;
        //                break;
        //            }
        //        case EffectType.Slippery:
        //            {
        //                birne.isEffectActive = false;
        //                break;
        //            }
        //        case EffectType.Sticky:
        //            {
        //                birne.isEffectActive = false;
        //                break;
        //            }
        //        default:
        //            {
        //                Debug.Log("Invalid EffectType!");
        //                break;
        //            }
        //    }
        //}
    }

    private void CookieDestroyer()
    {
        Destroy(this.gameObject);
    }

    // Types of objects.
    private enum EffectType
    {
        None, Bouncy, Breaking, Deadly, Slippery, Sticky
    }
}
