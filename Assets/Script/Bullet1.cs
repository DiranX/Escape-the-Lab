using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(0 , speed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(5);
            Debug.Log("Hit");
            Destroy(gameObject);
        }
        if (gameObject.CompareTag("Wall")) { Destroy(gameObject); }
        if (gameObject.CompareTag("Ground")) { Destroy(gameObject); }
    }
}
