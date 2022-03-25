using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float speed;
    public float playerDistance;
    public float visionRange;

    private Transform player;
    private float hp = 13f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Dead
        if (hp <= 0)
        {
            animator.SetBool("Dead", true);
            rb.mass = 999;
            Destroy(gameObject, 0.8f);
        }
        else
        {
            animator.SetBool("Dead", false);
        }
    }

    void FixedUpdate()
    {
        playerDistance = Vector2.Distance(player.position, rb.position);
        if (hp > 0)
        {
            // Movement
            if (playerDistance < visionRange)
            {
                Vector2 objective = new Vector2(player.position.x, player.position.y);
                Vector2 newPosition = Vector2.MoveTowards(rb.position, objective, speed * Time.deltaTime);
                rb.mass = 1;
                animator.SetBool("Move", true);

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
            // Idle Animation
            else
            {
                rb.mass = 999;
                animator.SetBool("Move", false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // See Vision Range in Scene
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    public void Damage(float damage)
    {
        hp = hp - damage;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Damage player
        if ("Player" == collision.gameObject.tag)
        {
            collision.gameObject.GetComponent<PlayerController>().Damage();
        }
    }
}
