using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBody : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bulletNormal;
    public GameObject bulletUp;
    public GameObject bulletDown;
    private PlayerMovement player;

    private void Start()
    {
        player  = GetComponentInParent<PlayerMovement>();
    }
    public void ShootNormal()
    {
        //Shoot in Horizontal Direction
        GameObject Bullet = Instantiate(bulletNormal, new Vector3(bulletSpawn.position.x, bulletSpawn.position.y, 0), Quaternion.identity); 
        Vector3 scale = Bullet.transform.localScale;
        scale.x *= this.transform.parent.localScale.x;
        Bullet.transform.localScale = scale;
        player.audioSource.PlayOneShot(player.clip[0]);
    }
    public void ShootUp()
    {
        //Shoot in Vertical Direction (UP)
        GameObject Bullet = Instantiate(bulletUp, new Vector3(bulletSpawn.position.x, bulletSpawn.position.y, 0), Quaternion.Euler(0, 0, 90));
        player.audioSource.PlayOneShot(player.clip[0]);
    }
    public void ShootDown()
    {
        //Shoot in vertical direction (Down)
        GameObject Bullet = Instantiate(bulletDown, new Vector3(bulletSpawn.position.x, bulletSpawn.position.y, 0), Quaternion.Euler(0, 0, -90));
        player.audioSource.PlayOneShot(player.clip[0]);
    }

}
