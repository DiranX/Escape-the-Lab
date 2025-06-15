using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Hitbox : MonoBehaviour
{
    public bool bullet;
    public float Damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(Damage);
            if (bullet)
            {
                Destroy(gameObject);
            }
        }
    }
}
