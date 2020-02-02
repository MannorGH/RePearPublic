using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinking : MonoBehaviour
{
    public float blinkEyeRate;
    public float minSec;
    public float maxSec;
    private Animator anim;
    private float previousBlinkEyeRate;
    private float blinkEyeTime;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (Time.time > blinkEyeTime)
        {
            previousBlinkEyeRate = blinkEyeRate;
            blinkEyeTime = Time.time + blinkEyeRate;
            anim.SetTrigger("Blink");
            while (previousBlinkEyeRate == blinkEyeRate)
            {
                blinkEyeRate = Random.Range(minSec, maxSec);
            }
        }
    }
}
