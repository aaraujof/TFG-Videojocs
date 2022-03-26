using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Animator animator;
    public int hp = 3;
    public bool collision;
    public List<GameObject> lives;

    Vector2 distance;
    Vector2 movement;
    Vector2 shoot;

    // Start is called before the first frame update
    void Start()
    {
        DisplayLives();
    }

    // Update is called once per frame
    void Update()
    {
        // Input move
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Input shoot
        shoot.x = Input.GetAxisRaw("Horizontal_shoot");
        shoot.y = Input.GetAxisRaw("Vertical_shoot");

        // Move animation
        if (hp > 0)
        {
            animator.SetFloat("Speed", movement.sqrMagnitude);

            if (movement.x != 0 || movement.y != 0)
            {
                animator.SetFloat("Horizontal", movement.x);
                animator.SetFloat("Vertical", movement.y);
            }
        }

        // Dead animation
        if (hp <= 0)
        {
            rb.mass = 9999;
            animator.SetBool("Dead", true);
            StartCoroutine(WaitingToMenu(2f));
        }
    }

    void FixedUpdate()
    {
        // Movement
        if (hp > 0)
        {
            if (collision == false)
            {
                rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
            }
            else
            {
                rb.MovePosition(rb.position + distance * 10 * speed * Time.fixedDeltaTime);
                collision = false;
            }
        }
    }

    public void Damage()
    {
        hp = hp - 1;
        DisplayLives();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Damage player
        if ("Enemy" == col.gameObject.tag)
        {
            collision = true;
            distance = (rb.position - col.gameObject.GetComponent<Rigidbody2D>().position).normalized;
        }
    }

    public IEnumerator Knockback(float duration, float power, Vector2 direction)
    {
        float timer = 0;
        Debug.Log(power);
        while (duration > timer)
        {
            timer += Time.deltaTime;
            rb.AddForce(new Vector2(direction.x * -2, direction.y * -2), ForceMode2D.Impulse);
        }

        yield return 0;
    }

    IEnumerator WaitingToMenu(float wait)
    {
        yield return new WaitForSeconds(wait);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    void DisplayLives()
    {
        for (int i = 0; i < 10; i++)
        {
            if (hp == 0)
            {
                lives[i].gameObject.SetActive(false);
            }
            else
            {
                if (i < hp)
                {
                    lives[i].gameObject.SetActive(true);
                }
                else
                {
                    lives[i].gameObject.SetActive(false);
                }
            }

        }
    }
}
