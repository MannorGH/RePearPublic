using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneReaction : MonoBehaviour
{
    [SerializeField]
    private ZoneType thisZoneType;

    [SerializeField]
    private AudioClip fanfare;

    private AudioSource audioplayer;

    private void Awake()
    {
        this.GetComponent<SpriteRenderer>().color = Color.clear;
        audioplayer = GetComponent<AudioSource>();
    }

    // Triggers the zone effect when the player touches the zone.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitbox"))
        {
            switch (thisZoneType)
            {
                case ZoneType.Death:
                    {
                        collision.gameObject.GetComponent<PlayerBehaviour>().KillFast();
                        break;
                    }
                case ZoneType.Goal:
                    {
                        collision.gameObject.GetComponent<PlayerBehaviour>().GoalReached();
                        audioplayer.PlayOneShot(fanfare, 0.3f);
                        break;
                    }
                default:
                    {
                        Debug.Log("Invalid ZoneType!");
                        break;
                    }
            }
        }
    }

    // Types of Zones
    private enum ZoneType
    {
        Death, Goal
    }
}
