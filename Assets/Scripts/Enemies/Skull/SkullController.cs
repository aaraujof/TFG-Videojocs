using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float speed;
    public float playerDistance;
    public float visionRange;
    public Vector2 room;
    public Transform firePointUp;
    public Transform firePointDown;
    public Transform firePointLeft;
    public Transform firePointRight;
    public GameObject bulletPrefab;
    public float bulletForce;
    public float fireRate;
    public float range;

    private Transform player;
    private float hp = 8f;
    private bool playerInsideRoom;
    private float nextFire = 0.0f;

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
            col.isTrigger = true;
            animator.SetBool("Dead", true);
            rb.mass = 999;
            Destroy(gameObject, 0.8f);
        }
        else
        {
            animator.SetBool("Dead", false);
        }

        playerDistance = Vector2.Distance(player.position, rb.position);
        playerInsideRoom = playerInRoom(player.position, room);
        if (hp > 0)
        {
            if (playerDistance < visionRange && playerInsideRoom == true && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }
        }
    }

    void FixedUpdate()
    {
        playerDistance = Vector2.Distance(player.position, rb.position);
        playerInsideRoom = playerInRoom(player.position, room);

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
        if (hp <= 0)
        {
            PlayerPrefs.SetInt("skull", PlayerPrefs.GetInt("skull") + 1);
            PlayerPrefs.Save();
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

    void Shoot()
    {
        Vector2 objective = new Vector2(player.position.x, player.position.y);

        if (Mathf.Abs(objective.x - rb.position.x) > Mathf.Abs(objective.y - rb.position.y))
        {
            if (objective.x > rb.position.x)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePointRight.transform.position, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = (player.position - bullet.transform.position).normalized * bulletForce;
                Destroy(bullet, range);
            }
            else if (objective.x < rb.position.x)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePointLeft.transform.position, Quaternion.Euler(0f, 0f, 180));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = (player.position - bullet.transform.position).normalized * bulletForce;
                Destroy(bullet, range);
            }
        }
        else
        {
            if (objective.y > rb.position.y)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePointUp.transform.position, Quaternion.Euler(0f, 0f, 90));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = (player.position - bullet.transform.position).normalized * bulletForce;
                Destroy(bullet, range);
            }
            else if (objective.y < rb.position.y)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePointDown.transform.position, Quaternion.Euler(0f, 0f, -90));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = (player.position - bullet.transform.position).normalized * bulletForce;
                Destroy(bullet, range);
            }
        }
    }
}
