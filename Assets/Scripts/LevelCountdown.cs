using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCountdown : MonoBehaviour
{
    [SerializeField]
    private GameObject pear;
    [SerializeField]
    private GameObject belt;

    [SerializeField]
    private float initialSpawnDelay, finalSpawnDelay;

    private float animationLength = 5.0f;
    private void Start()
    {
        belt.GetComponent<ConveyorBeltLogic>().spawnCooldown = initialSpawnDelay;
        Invoke("EnableMovement", animationLength);
    }

    private void EnableMovement()
    {
        pear.GetComponent<PlayerBehaviour>().StartMovement();
        belt.GetComponent<ConveyorBeltLogic>().spawnCooldown = finalSpawnDelay;
    }
}
