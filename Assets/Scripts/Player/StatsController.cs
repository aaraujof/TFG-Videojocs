using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerController;
    private ShootController shootController;

    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        shootController = GameObject.FindWithTag("Player").GetComponent<ShootController>();
        //StartCoroutine(WaitingToMenu(3f));
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    IEnumerator Waiting(float wait)
    {
        yield return new WaitForSeconds(wait);

        playerController.hp = 7;
        playerController.DisplayLives();

    }

    IEnumerator ItemWaiting()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Animator>().SetBool("Item", false);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    private void pickUpItem()
    {
        player.GetComponent<Animator>().SetBool("Item", true);
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        StartCoroutine(ItemWaiting());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ("Item" == other.gameObject.tag)
        {
            if ("Heart" == other.gameObject.name)
            {
                if(playerController.hp < 10)
                {
                    pickUpItem();
                    playerController.hp = playerController.hp + 1;
                    playerController.DisplayLives();
                }
                    
            }

            if ("Honey" == other.gameObject.name)
            {
                Destroy(other.GetComponent<BoxCollider2D>());
                pickUpItem();
                other.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1);
                playerController.speed = playerController.speed - 0.3f;
                Destroy(other.gameObject, 0.5f);
            }

        }
    }
}