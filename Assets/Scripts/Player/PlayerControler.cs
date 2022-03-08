using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private bool walking;

    public float speed;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        
        animator.SetFloat("Speed", movement.sqrMagnitude);

        if(movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            if (!walking)
            {
                walking = true;
                animator.SetBool("Walking", walking);
            }
        }else
        {
            walking = false;
            animator.SetBool("Walking", walking);
        }

            if (Input.GetKey("left")) 
        {
            //gameObject.transform.Translate(-5f * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey("right"))
        {
            //gameObject.transform.Translate(5f * Time.deltaTime, 0, 0);
        }
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);

        //rb.velocity = new Vector3(movement.x * speed, movement.y * speed, 0);
    }
}
