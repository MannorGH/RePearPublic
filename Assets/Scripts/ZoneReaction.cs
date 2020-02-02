using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneReaction : MonoBehaviour
{
    [SerializeField]
    private ZoneType thisZoneType;

    // Triggers the zone effect when the player touches the zone.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitbox"))
        {
            switch (thisZoneType)
            {
                case ZoneType.Death:
                    {
                        // Levelreset.
                        break;
                    }
                case ZoneType.Goal:
                    {
                        // Zur Map?
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
