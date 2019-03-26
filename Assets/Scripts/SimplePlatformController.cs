using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SimplePlatformController : MonoBehaviour
{

    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    public Transform WallCheckPoint;
    public bool WallCheck;
    public LayerMask WallLayerMask;

    public bool WallSlide;
    public bool grounded = false;
    public Animator anim;
    public Rigidbody2D rb2d;

    public int BaseSpeed { get; internal set; }


    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        Debug.Log("grounded =" + grounded);
        if (Input.GetButtonDown("Jump") && grounded && !WallSlide)
        {
            jump = true;

        }

        if (!grounded)
        {
            WallCheck = Physics2D.OverlapCircle(WallCheckPoint.position, 0.1f, WallLayerMask);

            if (facingRight && Input.GetAxis("Horizontal") > 0.1f || !facingRight && Input.GetAxis("Horizontal") < 0.1f)
            {
                if (WallCheck)
                {
                    HandleWallSlide();
                }

            }

        }
        else
        {
            WallSlide = false;
        }

    }

    void HandleWallSlide()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, -0.1f);

        WallSlide = true;

        if (facingRight && Input.GetButtonDown("Jump") && !grounded)
        {
            rb2d.AddForce(new Vector2(-1, 1) * jumpForce);
        }

        else if ( Input.GetButtonDown("Jump") && !grounded)
        {
            rb2d.AddForce(new Vector2(1, 1) * jumpForce);
        }

    }

    

    void FixedUpdate()
    {
       
        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();

        if (jump)
        {
            anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

      
    }


    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}



