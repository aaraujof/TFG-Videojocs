using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CyclopController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public float speed;
    public float playerDistance;
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
    private float hp = 60f;
    private bool playerInsideRoom;
    private bool dead = false;
    private bool inPoint = false;
    private float nextFire = 0.0f;

    Vector2 obj;
    Vector2 position1;
    Vector2 position2;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        position1 = new Vector2(room.x-6, room.y);
        position2 = new Vector2(room.x+6, room.y);
        obj = position1;
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

        playerDistance = Vector2.Distance(player.position, rb.position);
        playerInsideRoom = playerInRoom(player.position, room);
        if (hp > 0)
        {
            nextFire += Time.deltaTime;
            if (playerInsideRoom == true && nextFire > Time.deltaTime + fireRate)
            {
                nextFire = 0;
                if (hp > 30)
                {
                    Shoot();
                }
                else
                {
                    Shoot2();
                }
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
            if (playerInsideRoom == true)
            {
                rb.mass = 1;
                animator.SetBool("Move", true);

                if (hp < 30)
                {
                    Vector2 objective = new Vector2(player.position.x, player.position.y);
                    Vector2 newPosition = Vector2.MoveTowards(rb.position, objective, speed * Time.deltaTime);

                    // Movement Animation
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

                    rb.MovePosition(newPosition);

                }
                else
                {
                    if(inPoint == true)
                    {
                        if (rb.position.x == position1.x)
                        {
                            obj = position2;
                        }
                        else
                        {
                            obj = position1;
                        }
                        inPoint = false;
                    }
                    else
                    {
                        if (rb.position.x == obj.x)
                        {
                            inPoint = true;
                        }
                    }

                    Vector2 newPosition = Vector2.MoveTowards(rb.position, obj, speed * Time.deltaTime);

                    // Movement Animation
                    if (player.position.y > rb.position.y)
                    {
                        animator.SetFloat("Horizontal", 0);
                        animator.SetFloat("Vertical", 1);
                    }
                    if (player.position.y < rb.position.y)
                    {
                        animator.SetFloat("Horizontal", 0);
                        animator.SetFloat("Vertical", -1);
                    }

                    rb.MovePosition(newPosition);

                }
                
            }

            // Idle Animation
            else
            {
                rb.mass = 999;
                animator.SetBool("Move", false);
            }
        }
    }

    public void Damage(float damage)
    {
        hp = hp - damage;
        if (hp <= 0)
        {
            if (dead == false)
            {
                dead = true;
                PlayerPrefs.SetInt("Win", PlayerPrefs.GetInt("Win") + 1);
                PlayerPrefs.SetInt("Runs", PlayerPrefs.GetInt("Runs") + 1);
                PlayerPrefs.SetInt("Cyclop", PlayerPrefs.GetInt("Cyclop") + 1);
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

    void Shoot2()
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
