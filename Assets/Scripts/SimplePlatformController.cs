using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    [HideInInspector] public bool facingright = true;
    [HideInInspector] public bool jump = false;

    public float moveForce = 365f;
    public float maxspeed = 5f;
    public float jumpforce = 1000f;
    public Transform groundCheck;


    
    
    private Animator anim;
    private Rigidbody2D rb2d;

    Vector2 myPos;
    Vector2 groundCheckPos;



    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        myPos = new Vector2(transform.position.x, transform.position.y);
        groundCheckPos = new Vector2(transform.position.x, transform.position.y);
        if (Input.GetKey("w") && !isGrounded())
        {
            transform.position += Vector3.up * 20 * Time.deltaTime;
        }
    }

    public bool isGrounded()
    {

        return Physics2D.Linecast(myPos, groundCheckPos, 1 << LayerMask.NameToLayer("Ground"));

        if (result)
        {
            Debug.DrawLine(myPos, groundCheckPos, Color.green, 0.5f, false);
        }
        else
        {
            Debug.DrawLine(myPos, groundCheckPos, Color.red, 0.5f, false);
        }
        return result;
    }
}


void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxspeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxspeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxspeed, rb2d.velocity.y);

        if (h > 0 && !facingright)
            Flip();
        else if (h < 0 && facingright)
            Flip();

        
    }


    void Flip()
    {
        facingright = !facingright;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }






