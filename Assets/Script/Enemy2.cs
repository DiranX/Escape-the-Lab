using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public GameObject bullet;
    public Transform Spawn;
    public float Speed;
    public AudioSource audio;
    public AudioClip clip;

    public void ShootNormal()
    {
        GameObject Bullet = Instantiate(bullet, new Vector3(Spawn.position.x, Spawn.position.y, 0), Quaternion.identity);

        float direction = Mathf.Sign(transform.localScale.x);
        Bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * -Speed, 0);
        Destroy(Bullet, 3);
        audio.PlayOneShot(clip);
    }
}
