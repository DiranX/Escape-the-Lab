using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float health = 100f;
    float horizontal;
    float vertical;
    [SerializeField]float speed;
    float jumpingPower = 16f;
    bool isFacingRight = true;

    public Rigidbody2D rb;
    public Transform GroundCheck;
    public LayerMask layer;
    public Animator topBody;
    public Animator lowerBody;

    public Image healthBar;

    public AudioSource audioSource;
    public AudioClip[] clip;

    public static PlayerMovement instance;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //Health
        healthBar.fillAmount = health / 100;
        if(health <= 0)
        {
            health = 0;
            topBody.SetBool("Death", true);
            lowerBody.SetBool("Death", true);
        }else if(health >= 100)
        {
            health = 100;
        }

        //Movement Input
        if (health != 0)
        {
            if (horizontal != 0 && isGrounded())
            {
                topBody.SetBool("isWalking", true);
                lowerBody.SetBool("isWalking", true);
            }
            else if (horizontal == 0 || !isGrounded())
            {
                topBody.SetBool("isWalking", false);
                lowerBody.SetBool("isWalking", false);
            }

            if (Input.GetKeyDown(KeyCode.X) && isGrounded())
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                audioSource.PlayOneShot(clip[1]);
            }

            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
            Flip();
            lowerBody.SetBool("isJumping", isGrounded());
        }

        //Shooting Input
        if (Input.GetKey(KeyCode.Z))
        {
            if(vertical == 0)
            {
                topBody.SetBool("isShooting", true);
                topBody.SetBool("Up", false);
                topBody.SetBool("Down", false);
            }
            else if(vertical >= .5f)
            {
                topBody.SetBool("Up", true);
            }else if(vertical <= .5f)
            {
                topBody.SetBool("Down", true);
            }
        }
        else
        {
            topBody.SetBool("isShooting", false);
            topBody.SetBool("Up", false);
            topBody.SetBool("Down", false);
        }
    }

    private void FixedUpdate()
    {
        //Move the Player using Velocity of the rigidbody
        if(health != 0)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
    }

    void Flip()
    {
        //Flip player object using X local scale of the object
        if(isFacingRight && horizontal<0 || !isFacingRight && horizontal > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    bool isGrounded()
    {
        //check if player is on ground
        return Physics2D.OverlapCircle(GroundCheck.position, 0.1f, layer);
    }
    public void TakeDamage(float damage)
    {
        if(health != 0)
        {
            health -= damage;
            topBody.SetTrigger("Hit");
            lowerBody.SetTrigger("Hit");
            audioSource.PlayOneShot(clip[2]);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Battery"))
        {
            health += 30;
            Destroy(collision.gameObject);
        }
    }
}
