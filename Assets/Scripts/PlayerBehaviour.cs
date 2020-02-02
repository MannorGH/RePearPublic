using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public float speed = 2f;
    public bool doMove { private get; set; } = false;
    public bool moveRight = true;

    private Rigidbody2D rb;
    private Animator anim;
    //public bool isEffectActive = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);


        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        } else
        {
            anim.SetBool("isRunning", true);
        }

        if (moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        } else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }





        if (doMove)
        {
            anim.SetBool("isRunning", true);
            if (moveRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.eulerAngles = new Vector3(0, 0, 0);
            } else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }

    }

}
