using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    private ShootController shootController;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        shootController = GameObject.FindWithTag("Player").GetComponent<ShootController>();
    }

    IEnumerator ItemWaiting()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Animator>().SetBool("Item", false);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    private void pickUpItem(GameObject item)
    {
        Destroy(item.GetComponent<BoxCollider2D>());
        item.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1);
        player.GetComponent<Animator>().SetBool("Item", true);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(ItemWaiting());
        Destroy(item.gameObject, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ("Item" == other.gameObject.tag)
        {
            if ("Heart(Clone)" == other.gameObject.name)
            {
                if(playerController.hp < 10)
                {
                    pickUpItem(other.gameObject);
                    playerController.hp = playerController.hp + 1;
                    playerController.DisplayLives();
                }
                    
            }

            if ("Honey(Clone)" == other.gameObject.name)
            {
                pickUpItem(other.gameObject);
                playerController.speed = playerController.speed - 1f;
            }

            if ("Fish(Clone)" == other.gameObject.name)
            {
                pickUpItem(other.gameObject);
                playerController.speed = playerController.speed + 1f;
            }

            if ("Noodle(Clone)" == other.gameObject.name)
            {
                pickUpItem(other.gameObject);
                shootController.damage = shootController.damage + 1f;
            }

            if ("Octopus(Clone)" == other.gameObject.name)
            {
                pickUpItem(other.gameObject);
                shootController.fireRate = shootController.fireRate - 0.1f;
            }

            if ("Yakitori(Clone)" == other.gameObject.name)
            {
                pickUpItem(other.gameObject);
                shootController.range = shootController.range + 0.3f;
            }

            if ("Beaf(Clone)" == other.gameObject.name)
            {
                pickUpItem(other.gameObject);
                shootController.bulletForce = shootController.bulletForce + 2f;
            }

            if ("FortuneCookie(Clone)" == other.gameObject.name)
            {
                int randomUpgrade = Random.Range(1, 4);
                pickUpItem(other.gameObject);
                if (randomUpgrade == 1)
                {
                    shootController.damage = shootController.damage + 1f;
                }
                else if (randomUpgrade == 2)
                {
                    shootController.fireRate = shootController.fireRate - 0.1f;
                }
                else if (randomUpgrade == 3)
                {
                    shootController.bulletForce = shootController.bulletForce + 2f;
                }
            }

        }
    }
}