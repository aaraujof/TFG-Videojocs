using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopShoot : MonoBehaviour
{
    public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy object on collision and hitEffect
        if ("Player" == collision.gameObject.tag || "Wall" == collision.gameObject.tag || "Bullet" == collision.gameObject.tag)
        {
            if ("Player" == collision.gameObject.tag)
            {
                collision.gameObject.GetComponent<PlayerController>().Damage();
            }

            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            effect.GetComponent<SpriteRenderer>().material.color = new Color(0.56f, 0.24f, 0.38f, 1);
            Destroy(effect, 0.1f);
            Destroy(this.gameObject);
        }
    }
}
