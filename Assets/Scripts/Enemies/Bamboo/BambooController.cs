using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
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
    private float hp = 15f;
    private bool playerInsideRoom;
    private float nextFire = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rb.mass = 9999;
    }

    // Update is called once per frame
    void Update()
    {
        // Dead
        if (hp <= 0)
        {
            col.isTrigger = true;
            animator.SetBool("Dead", true);
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
            nextFire += Time.deltaTime;
            if (playerDistance < visionRange && playerInsideRoom == true && nextFire > Time.deltaTime + fireRate)
            {
                nextFire = 0;
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
            }
            // Idle Animation
            else
            {
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
            PlayerPrefs.SetInt("bamboo", PlayerPrefs.GetInt("bamboo") + 1);
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
                Vector2 bullet1pos = new Vector2(firePointRight.transform.position.x, firePointRight.transform.position.y + 0.25f);
                Vector2 bullet2pos = new Vector2(firePointRight.transform.position.x + 0.25f, firePointRight.transform.position.y);
                Vector2 bullet3pos = new Vector2(firePointRight.transform.position.x, firePointRight.transform.position.y - 0.25f);

                Vector3 position1 = new Vector2(rb.position.x + 4, rb.position.y + 3);
                Vector3 position2 = new Vector2(rb.position.x + 4, rb.position.y);
                Vector3 position3 = new Vector2(rb.position.x + 4, rb.position.y - 3);

                GameObject bullet1 = Instantiate(bulletPrefab, bullet1pos, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                rb1.velocity = (position1 - bullet1.transform.position).normalized * bulletForce;
                Destroy(bullet1, range);

                GameObject bullet2 = Instantiate(bulletPrefab, bullet2pos, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                rb2.velocity = (position2 - bullet2.transform.position).normalized * bulletForce;
                Destroy(bullet2, range);

                GameObject bullet3 = Instantiate(bulletPrefab, bullet3pos, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                rb3.velocity = (position3 - bullet3.transform.position).normalized * bulletForce;
                Destroy(bullet3, range);
            }
            else if (objective.x < rb.position.x)
            {
                Vector2 bullet1pos = new Vector2(firePointLeft.transform.position.x, firePointLeft.transform.position.y + 0.25f);
                Vector2 bullet2pos = new Vector2(firePointLeft.transform.position.x - 0.25f, firePointLeft.transform.position.y);
                Vector2 bullet3pos = new Vector2(firePointLeft.transform.position.x, firePointLeft.transform.position.y - 0.25f);

                Vector3 position1 = new Vector2(rb.position.x - 4, rb.position.y + 3);
                Vector3 position2 = new Vector2(rb.position.x - 4, rb.position.y);
                Vector3 position3 = new Vector2(rb.position.x - 4, rb.position.y - 3);

                GameObject bullet1 = Instantiate(bulletPrefab, bullet1pos, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                rb1.velocity = (position1 - bullet1.transform.position).normalized * bulletForce;
                Destroy(bullet1, range);

                GameObject bullet2 = Instantiate(bulletPrefab, bullet2pos, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                rb2.velocity = (position2 - bullet2.transform.position).normalized * bulletForce;
                Destroy(bullet2, range);

                GameObject bullet3 = Instantiate(bulletPrefab, bullet3pos, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                rb3.velocity = (position3 - bullet3.transform.position).normalized * bulletForce;
                Destroy(bullet3, range);
            }
        }
        else
        {
            if (objective.y > rb.position.y)
            {
                Vector2 bullet1pos = new Vector2(firePointUp.transform.position.x - 0.25f, firePointUp.transform.position.y);
                Vector2 bullet2pos = new Vector2(firePointUp.transform.position.x, firePointUp.transform.position.y + 0.25f);
                Vector2 bullet3pos = new Vector2(firePointUp.transform.position.x + 0.25f, firePointUp.transform.position.y);

                Vector3 position1 = new Vector2(rb.position.x - 3, rb.position.y + 4);
                Vector3 position2 = new Vector2(rb.position.x, rb.position.y + 4);
                Vector3 position3 = new Vector2(rb.position.x + 3, rb.position.y + 4);

                GameObject bullet1 = Instantiate(bulletPrefab, bullet1pos, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                rb1.velocity = (position1 - bullet1.transform.position).normalized * bulletForce;
                Destroy(bullet1, range);

                GameObject bullet2 = Instantiate(bulletPrefab, bullet2pos, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                rb2.velocity = (position2 - bullet2.transform.position).normalized * bulletForce;
                Destroy(bullet2, range);

                GameObject bullet3 = Instantiate(bulletPrefab, bullet3pos, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                rb3.velocity = (position3 - bullet3.transform.position).normalized * bulletForce;
                Destroy(bullet3, range);
            }
            else if (objective.y < rb.position.y)
            {
                Vector2 bullet1pos = new Vector2(firePointDown.transform.position.x - 0.25f, firePointDown.transform.position.y);
                Vector2 bullet2pos = new Vector2(firePointDown.transform.position.x, firePointDown.transform.position.y - 0.25f);
                Vector2 bullet3pos = new Vector2(firePointDown.transform.position.x + 0.25f, firePointDown.transform.position.y);

                Vector3 position1 = new Vector2(rb.position.x - 3, rb.position.y - 4);
                Vector3 position2 = new Vector2(rb.position.x, rb.position.y - 4);
                Vector3 position3 = new Vector2(rb.position.x + 3, rb.position.y - 4);

                GameObject bullet1 = Instantiate(bulletPrefab, bullet1pos, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                rb1.velocity = (position1 - bullet1.transform.position).normalized * bulletForce;
                Destroy(bullet1, range);

                GameObject bullet2 = Instantiate(bulletPrefab, bullet2pos, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                rb2.velocity = (position2 - bullet2.transform.position).normalized * bulletForce;
                Destroy(bullet2, range);

                GameObject bullet3 = Instantiate(bulletPrefab, bullet3pos, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
                rb3.velocity = (position3 - bullet3.transform.position).normalized * bulletForce;
                Destroy(bullet3, range);
            }
        }
    }
}
