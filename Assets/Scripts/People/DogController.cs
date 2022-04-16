using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float speed;
    public Vector2 left;
    public Vector2 right;
    public float timer;

    private Vector2 objective;
    private Vector2 newPosition;
    private bool inPoint = false;
    private float newTimer;

    // Start is called before the first frame update
    void Start()
    {
        objective = left;
        animator.SetBool("Move", true);
        newTimer = timer;
    }

    void FixedUpdate()
    {
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
                if (rb.position.x == left.x)
                {
                    objective = right;
                }
                else
                {
                    objective = left;
                }
                inPoint = false;
                timer = newTimer;
                animator.SetBool("Move", true);
            }
        }
        else
        {
            if (rb.position.x == objective.x)
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
                spriteRenderer.flipX = false;
            }
            if (objective.x < rb.position.x)
            {
                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Vertical", 0);
                spriteRenderer.flipX = true;
            }
        }
        else
        {
            if (objective.y > rb.position.y)
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", 1);
                spriteRenderer.flipX = false;
            }
            if (objective.y < rb.position.y)
            {
                animator.SetFloat("Horizontal", 0);
                animator.SetFloat("Vertical", -1);
                spriteRenderer.flipX = true;
            }
        }

        rb.MovePosition(newPosition);
    }
}
