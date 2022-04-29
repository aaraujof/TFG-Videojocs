using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullShoot : MonoBehaviour
{
    public GameObject hitEffect;

    private Color orange = new Color(255.0f, 153.0f, 51.0f, 125.0f);

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
            effect.GetComponent<SpriteRenderer>().material.color = new Color(0.89f, 0.42f, 0.23f, 1);
            Destroy(effect, 0.1f);
            Destroy(this.gameObject);
        }
    }
}
