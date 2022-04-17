using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    public float visionRange;
    public GameObject runsMenu;

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
                if (runsMenu.activeSelf)
                {
                    runsMenu.SetActive(false);
                }
                else
                {
                    runsMenu.SetActive(true);
                }
            }
        }
        else
        {
            runsMenu.SetActive(false);
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
