using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomShoot : MonoBehaviour
{
    public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy object on collision and hitEffect
        if ("Player" == collision.gameObject.tag || "Wall" == collision.gameObject.tag || "Bullet" == collision.gameObject.tag || "EnemyBullet" == collision.gameObject.tag || ("Enemy" == collision.gameObject.tag && collision.gameObject.name != "Mushroom(Clone)"))
        {
            if ("Player" == collision.gameObject.tag)
            {
                collision.gameObject.GetComponent<PlayerController>().Damage();
            }

            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            effect.GetComponent<SpriteRenderer>().material.color = Color.red;
            Destroy(effect, 0.1f);
            Destroy(this.gameObject);
        }
    }
}
