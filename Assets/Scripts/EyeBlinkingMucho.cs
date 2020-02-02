using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBlinkingMucho : MonoBehaviour
{
    public float blinkEyeRate;
    public float minSec;
    public float maxSec;
    public int numberOfEyes;
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
            int randomBlink = Random.Range(1, numberOfEyes);
            anim.SetTrigger("Blink"+randomBlink);
            while (previousBlinkEyeRate == blinkEyeRate)
            {
                blinkEyeRate = Random.Range(minSec, maxSec);
            }
        }
    }
}
