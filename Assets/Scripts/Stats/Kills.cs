using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kills : MonoBehaviour
{
    public float visionRange;
    public GameObject killsMenu;

    private float playerDistance;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (playerDistance < visionRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (killsMenu.activeSelf)
                {
                    killsMenu.SetActive(false);
                }
                else
                {
                    killsMenu.SetActive(true);
                }
            }
        }
        else
        {
            killsMenu.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        playerDistance = Vector2.Distance(player.position, transform.position);
    }

    private void OnDrawGizmos()
    {
        // See Vision Range in Scene
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
