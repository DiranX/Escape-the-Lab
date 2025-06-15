using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float Health;
    private Animator Animator;
    private Rigidbody2D Rigidbody;
    private Collider2D collider;
    public Transform player;
    public bool isFlipped = false;
    public bool isdead = false;
    public bool Boss;
    public Image bostHealtBar;
    public GameObject winText;
    private SpriteRenderer sprite;
    private AudioSource audio;
    public AudioClip[] clip;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(this.Health <= 0 && !isdead)
        {
            isdead = true;
            Health = 0;
            Animator.SetBool("Death", true);
            collider.isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer("Dead");
            Rigidbody.constraints = RigidbodyConstraints2D.None;
            Destroy(gameObject, 3);
        }else if (isdead)
        {
            Health = 0;
            collider.isTrigger = true;
            gameObject.layer = LayerMask.NameToLayer("Dead");
            Rigidbody.constraints = RigidbodyConstraints2D.None;
            Destroy(gameObject, 3);
        }

        if (Boss) 
        {
            bostHealtBar.fillAmount = Health / 500;
            if(Vector2.Distance(player.position, this.gameObject.transform.position) <= 25)
            {
                bostHealtBar.gameObject.SetActive(true);
            }
            LookAtPlayer();
            if (isdead) { winText.SetActive(true); }
        }
    }

    public void LookAtPlayer()
    {
        Vector2 flipped = transform.localScale;
        flipped.x *= -1f;

        if(transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            isFlipped = false;
        }else if(transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            isFlipped = true;
        }
    }
    public void TakeDamage(float damage)
    {
        if (Health != 0)
        {
            Health -= damage;
            Animator.SetTrigger("Hit");
            audio.PlayOneShot(clip[1]);
            if (Boss) { StartCoroutine(Bipping()); }
        }
    }

    IEnumerator Bipping()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(.1f);
        sprite.color = Color.white;
    }

    public void playDeadSound()
    {
        audio.PlayOneShot(clip[0]);
    }

    public void Attack()
    {
        audio.PlayOneShot(clip[2]);
    }
}
