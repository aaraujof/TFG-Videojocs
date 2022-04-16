using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldManController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public float speed;
    public Vector2 left;
    public Vector2 right;
    public float timer;
    public float visionRange;
    public GameObject oldManText;

    private Vector2 objective;
    private Vector2 newPosition;
    private bool inPoint = false;
    private float newTimer;
    private float playerDistance;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        objective = left;
        animator.SetBool("Move", true);
        newTimer = timer;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (playerDistance < visionRange && animator.GetBool("Move") == false)
        {
            oldManText.SetActive(true);
        }
        else
        {
            oldManText.SetActive(false);
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

    private void OnDrawGizmos(){
        // See Vision Range in Scene
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
