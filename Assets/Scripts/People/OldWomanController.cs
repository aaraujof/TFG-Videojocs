using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldWomanController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float speed;
    public Vector2 top;
    public Vector2 bottom;
    public float timer;
    public float visionRange;
    public GameObject oldWomanText;

    private Vector2 objective;
    private Vector2 newPosition;
    private bool inPoint = false;
    private float newTimer;
    private float playerDistance;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        objective = top;
        animator.SetBool("Move", true);
        newTimer = timer;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (playerDistance < visionRange)
        {
            oldWomanText.SetActive(true);
        }
        else
        {
            oldWomanText.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        playerDistance = Vector2.Distance(player.position, rb.position);

        // Movement
        if (inPoint == true)
        {
            if (timer > 0)
            {
                animator.SetBool("Move", false);
                timer -= Time.deltaTime;
            }
            else
            {
                if (rb.position.y == top.y)
                {
                    objective = bottom;
                }
                else
                {
                    objective = top;
                }
                inPoint = false;
                timer = newTimer;
                animator.SetBool("Move", true);
            }
        }
        else
        {
            if (rb.position.y == objective.y)
            {
                inPoint = true;
            }
        }

        newPosition = Vector2.MoveTowards(rb.position, objective, speed * Time.deltaTime);

        // Movement Animation
        if (Mathf.Abs(objective.x - rb.position.x) > Mathf.Abs(objective.y - rb.position.y))
        {
            if (objective.x > rb.position.x)
            {
                animator.SetFloat("Horizontal", 1);
                animator.SetFloat("Vertical", 0);
            }
            if (objective.x < rb.position.x)
            {
                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", 0);
            }
        }
        else
        {
            if (objective.y > rb.position.y)
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 1);
            }
            if (objective.y < rb.position.y)
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", -1);
            }
        }

        rb.MovePosition(newPosition);
    }

    private void OnDrawGizmos()
    {
        // See Vision Range in Scene
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
