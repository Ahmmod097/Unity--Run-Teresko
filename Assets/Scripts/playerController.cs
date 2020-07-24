using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float maxSpeed;
    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpHeight;

    Rigidbody2D MyRB;
    Animator MyAnim;
    bool facingRight;

    //for shooting
     public Transform guntip;
     public GameObject bullet;
     float fireRate = 0.5f; //fire one rocket in a half second
     float nextFire = 0f;

    // Use this for initialization
    void Start()
    {
        MyRB = GetComponent<Rigidbody2D>();
        MyAnim = GetComponent<Animator>();
        facingRight = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (grounded && Input.GetAxis("Jump")>0)
        {
            grounded = false;
            MyAnim.SetBool("isGrounded", grounded);
            MyRB.AddForce(new Vector2(0, jumpHeight));
        }
        //player shooting
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            fireRocket();
        }

    }
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,groundLayer);
        MyAnim.SetBool("isGrounded", grounded);

        MyAnim.SetFloat("verticalSpeed", MyRB.velocity.y);

        float move = Input.GetAxisRaw("Horizontal");

        MyAnim.SetFloat("speed", Mathf.Abs(move));

        MyRB.velocity = new Vector2(move * maxSpeed, MyRB.velocity.y);

        if (move > 0 && !facingRight)
        {
            flip();
        }
        else if (move < 0 && facingRight)
        {
            flip();
        }

        

        
    }
    void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    void fireRocket()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (facingRight)
            {
                Instantiate(bullet, guntip.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else if (!facingRight)
            {
                Instantiate(bullet, guntip.position, Quaternion.Euler(new Vector3(0, 0, 180f)));
            }

        } 
    }

    
}
