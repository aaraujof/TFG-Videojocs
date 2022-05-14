using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FrogController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float speed;
    public float playerDistance;
    public float visionRange;
    public float hp;
    public Vector2 room;
    public Image HealthBar;
    public GameObject health;

    private Transform player;
    private bool playerInsideRoom;
    private bool dead = false;
    private float initialHealth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        initialHealth = hp;
        HealthBar.fillAmount = hp / initialHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Dead
        if (hp <= 0)
        {
            col.isTrigger = true;
            animator.SetBool("Dead", true);
            rb.mass = 999;
            StartCoroutine(WaitingToMenu(2f));
        }
        else
        {
            animator.SetBool("Dead", false);
        }
    }

    void FixedUpdate()
    {
        playerDistance = Vector2.Distance(player.position, rb.position);
        playerInsideRoom = playerInRoom(player.position, room);

        if (playerInsideRoom == true)
        {
            health.SetActive(true);
        }
        else 
        {
            health.SetActive(false);
        }

        HealthBar.fillAmount = hp / initialHealth;

        if (hp > 0)
        {
            // Movement
            if (playerDistance < visionRange && playerInsideRoom == true)
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
                        spriteRenderer.flipX = true;
                    }
                    if (objective.x < rb.position.x)
                    {
                        animator.SetFloat("Horizontal", -1);
                        animator.SetFloat("Vertical", 0);
                        spriteRenderer.flipX = false;
                    }
                }
                else
                {
                    if (objective.y > rb.position.y)
                    {
                        animator.SetFloat("Horizontal", 0);
                        animator.SetFloat("Vertical", 1);
                        spriteRenderer.flipX = true;
                    }
                    if (objective.y < rb.position.y)
                    {
                        animator.SetFloat("Horizontal", 0);
                        animator.SetFloat("Vertical", -1);
                        spriteRenderer.flipX = false;
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

    public void Damage(float damage, string name)
    {
        hp = hp - damage;
        if (hp <= 0)
        {
            if (dead == false)
            {
                dead = true;
                PlayerPrefs.SetInt("Win", PlayerPrefs.GetInt("Win") + 1);
                PlayerPrefs.SetInt("Runs", PlayerPrefs.GetInt("Runs") + 1);
                if (name == "Frog")
                {
                    PlayerPrefs.SetInt("Frog", PlayerPrefs.GetInt("Frog") + 1);
                }
                if (name == "FrogGreen")
                {
                    PlayerPrefs.SetInt("FrogGreen", PlayerPrefs.GetInt("FrogGreen") + 1);
                }
                PlayerPrefs.Save();
            }
        }
    }

    private bool playerInRoom(Vector2 player, Vector2 enemyRoom)
    {
        bool inRoom = false;

        float roomMaxX = enemyRoom.x + 8;
        float roomMinX = enemyRoom.x - 8;
        float roomMaxY = enemyRoom.y + 4;
        float roomMinY = enemyRoom.y - 4;

        if (player.x < roomMaxX && player.x > roomMinX && player.y < roomMaxY && player.y > roomMinY)
        {
            inRoom = true;
        }

        return inRoom;
    }

    public void SetValues(Vector2 position)
    {
        this.room = position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Damage player
        if ("Player" == collision.gameObject.tag)
        {
            collision.gameObject.GetComponent<PlayerController>().Damage();
        }

        // Change color on hit
        if ("Bullet" == collision.gameObject.tag)
        {
            StartCoroutine(changeColorOnHit(0.1f));
        }
    }

    IEnumerator changeColorOnHit(float wait)
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(wait);

        spriteRenderer.color = Color.white;
    }

    IEnumerator WaitingToMenu(float wait)
    {
        yield return new WaitForSeconds(wait);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
