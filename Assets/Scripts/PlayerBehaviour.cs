using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{

    public float speed = 2f;
    public bool doMove { private get; set; } = false;
    private bool doStop = false;
    public bool moveRight = true;

    private Rigidbody2D rb;
    private Animator anim;

    public bool speeding = false;
    public bool sticking = false;
    
	private float lastDirectionSwitchTime = 0.0f;
    [SerializeField]
    private float directionSwitchCooldown = 2.0f;
    [SerializeField]
    private ChangeLevel changeLevel;

    [SerializeField]
    Collider2D ColliderFront, ColliderBottom, ColliderBottomSmall, ColliderFrontSmall;

    [SerializeField]
    private Button turnButton, resetButton;

    [SerializeField]
    private Animator friendAnim1, friendAnim2;

    [SerializeField]
    private float jumpForce = 10.0f;
    [SerializeField]
    private float jumpCooldown = 1.0f;
    private float timeOfLastJump = -10.0f;
    private bool isJumpingScript = false;

    private float minJumpTime = 0.5f;
    private float maxJumpTime = 4.0f;

    private float brakeProgress = 0.0f;
    private float brakeTime = 0.4f;
    private bool doBrake = false;
    private float originalXVelocity;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (doBrake)
        {
            rb.velocity = new Vector2(Mathf.Lerp(originalXVelocity, 0.0f, brakeProgress), rb.velocity.y);
            brakeProgress += Time.deltaTime / brakeTime;
        }

		if (moveRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (ColliderFrontSmall.IsTouchingLayers(LayerMask.GetMask("Default")) && !ColliderBottomSmall.IsTouchingLayers(LayerMask.GetMask("Default")))
        {
            doStop = true;
        }
        if (!ColliderFrontSmall.IsTouchingLayers(LayerMask.GetMask("Default")) || ColliderBottomSmall.IsTouchingLayers(LayerMask.GetMask("Default")))
        {
            doStop = false;
        }

        if (doMove && !doStop)
        {
            anim.SetBool("isRunning", true);
            if (moveRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }

            if (!isJumpingScript && timeOfLastJump + jumpCooldown < Time.time)
            {
                
                if (ColliderBottom.IsTouchingLayers(LayerMask.GetMask("Default")) && ColliderFront.IsTouchingLayers(LayerMask.GetMask("Default")))
                {
                    Jump(jumpForce, Vector2.up);
                    timeOfLastJump = Time.time;
                }
            }

            if ((isJumpingScript && timeOfLastJump + minJumpTime < Time.time))
            {
                if (ColliderBottomSmall.IsTouchingLayers(LayerMask.GetMask("Default")) || timeOfLastJump + maxJumpTime < Time.time)
                {
                    Debug.Log("false");
                    isJumpingScript = false;
                    anim.SetBool("isJumping", false);
                }
            }
        }
    }
    
    public void switchMoveRight()
    {
        if (lastDirectionSwitchTime + directionSwitchCooldown < Time.time)
        {
            moveRight = !moveRight;
            lastDirectionSwitchTime = Time.time;
            turnButton.interactable = false;
            Invoke("InvokeReenableTurnButton", directionSwitchCooldown);
        }
    }

    public void Jump(float givenJumpForce, Vector2 direction)
    {
        rb.velocity += direction.normalized * givenJumpForce;
        anim.SetBool("isJumping", true);
        anim.SetTrigger("Takeoff");
        isJumpingScript = true;

    }

    public void SpeedUp()
    {
        if (!speeding)
        {
            speeding = true;
            speed = speed * 2f;
            Invoke("InvokeSpeedDown", 0.5f);
        }
    }

    public void StickUp()
    {
        if (!sticking)
        {
            sticking = true;
            speed = speed * 0.5f;
            Invoke("InvokeStickDown", 0.5f);
        }
    }

    public void KillFast()
    {
        anim.SetTrigger("Die");
        anim.SetBool("isDead", true);
        doMove = false;
        ResetLevel();
    }

    public void Kill()
    {
        anim.SetTrigger("Die");
        anim.SetBool("isDead", true);
        doMove = false;
        Invoke("ResetLevel", 2f);
    }
    
    public void GoalReached()
    {
        doMove = false;
        doBrake = true;
        originalXVelocity = rb.velocity.x;
        anim.SetTrigger("Goal");
        anim.SetTrigger("isHappy");
        friendAnim1.SetTrigger("Goal");
        friendAnim2.SetTrigger("Goal");
        friendAnim1.SetTrigger("isHappy");
        friendAnim2.SetTrigger("isHappy");
        Invoke("NextLevel", 4.0f);
    }
    

    private void ResetLevel()
    {
        changeLevel.InvokeResetLevel();
    }
    
    private void NextLevel()
    {
        changeLevel.InvokeLoadNextLevel();
    }

    private void InvokeSpeedDown()
    {
        speeding = false;
        speed = speed * 0.5f;
    }

    private void InvokeStickDown()
    {
        sticking = false;
        speed = speed * 2f;
    }

    public void StartMovement()
    {
        doMove = true;
        resetButton.interactable = true;
        turnButton.interactable = true;
    }

    private void InvokeReenableTurnButton()
    {
        turnButton.interactable = true;
    }
}
