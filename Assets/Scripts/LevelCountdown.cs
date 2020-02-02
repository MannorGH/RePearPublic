using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCountdown : MonoBehaviour
{
    [SerializeField]
    private GameObject pear;

    private float animationLength = 5.5f;
    private void Start()
    {
        Invoke("EnableMovement", animationLength);
    }

    private void EnableMovement()
    {
        pear.GetComponent<PlayerBehaviour>().doMove = true;
    }
}
