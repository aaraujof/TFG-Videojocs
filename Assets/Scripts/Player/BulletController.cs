using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy object on collision and hitEffect
        if ("Enemy" == collision.gameObject.tag || "Wall" == collision.gameObject.tag || "EnemyBullet" == collision.gameObject.tag)
        {
            if ("Enemy" == collision.gameObject.tag)
            {
                // Take hp
                if ("Larva(Clone)" == collision.gameObject.name)
                {
                    collision.gameObject.GetComponent<LarvaController>().Damage(GameObject.Find("Player").GetComponent<ShootController>().damage);
                }
                if ("Slime(Clone)" == collision.gameObject.name)
                {
                    collision.gameObject.GetComponent<SlimeController>().Damage(GameObject.Find("Player").GetComponent<ShootController>().damage);
                }
                if ("Mouse(Clone)" == collision.gameObject.name)
                {
                    collision.gameObject.GetComponent<MouseController>().Damage(GameObject.Find("Player").GetComponent<ShootController>().damage);
                }
                if ("Mushroom(Clone)" == collision.gameObject.name)
                {
                    collision.gameObject.GetComponent<MushroomController>().Damage(GameObject.Find("Player").GetComponent<ShootController>().damage);
                }
                if ("Bamboo(Clone)" == collision.gameObject.name)
                {
                    collision.gameObject.GetComponent<BambooController>().Damage(GameObject.Find("Player").GetComponent<ShootController>().damage);
                }
                if ("Skull(Clone)" == collision.gameObject.name)
                {
                    collision.gameObject.GetComponent<SkullController>().Damage(GameObject.Find("Player").GetComponent<ShootController>().damage);
                }
                if ("Frog(Clone)" == collision.gameObject.name)
                {
                    collision.gameObject.GetComponent<FrogController>().Damage(GameObject.Find("Player").GetComponent<ShootController>().damage, "Frog");
                }
                if ("FrogGreen(Clone)" == collision.gameObject.name)
                {
                    collision.gameObject.GetComponent<FrogController>().Damage(GameObject.Find("Player").GetComponent<ShootController>().damage, "FrogGreen");
                }
            }

            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.1f);
            Destroy(gameObject);
        }
    }
}
