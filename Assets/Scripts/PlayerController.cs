using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;


    public float MoveSpeed;
    public Rigidbody2D theRB;
    public float JumpForce;

    private bool isGrounded;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;


    private bool canDoubleJump;

    private Animator anim;
    private SpriteRenderer theSR;

    public float knockBackLenght, knockBackForce;
    private float knockBackCounter;

    public float bounceForce;

    public bool stopInput;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.instance.isPaused && !stopInput)
        {
            if (knockBackCounter <= 0)
            {
                theRB.velocity = new Vector2(MoveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);

                isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);

                if (isGrounded)
                {
                    canDoubleJump = true;
                }

                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded)
                    {
                        theRB.velocity = new Vector2(theRB.velocity.x, JumpForce);
                        AudioManager.instance.PlaySFX(10);
                    }
                    else
                    {
                        if (canDoubleJump)
                        {
                            theRB.velocity = new Vector2(theRB.velocity.x, JumpForce);
                            AudioManager.instance.PlaySFX(10);
                            canDoubleJump = false;
                        }
                    }
                }
                if (theRB.velocity.x < 0)
                {
                    theSR.flipX = true;
                }
                else if (theRB.velocity.x > 0)
                {
                    theSR.flipX = false;
                }
            }
            else
            {
                knockBackCounter -= Time.deltaTime;
                if (!theSR.flipX)
                {
                    theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
                }
                else
                {
                    theRB.velocity = new Vector2(+knockBackForce, theRB.velocity.y);
                }

            }

        }
        anim.SetFloat("MoveSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLenght;
        theRB.velocity = new Vector2(0f, knockBackForce);

        anim.SetTrigger("isHurt");
    }

    public void Bounce()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, bounceForce);
    }
}
