using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlamController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
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
    private float hp = 50f;
    private bool playerInsideRoom;
    private float nextFire = 0.0f;
    private float nextShoot = 0.0f;
    private bool dead = false;

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
                Shoot();
            }

            nextShoot += Time.deltaTime;
            if (hp < 25 && nextShoot > Time.deltaTime + 1.2f)
            {
                nextShoot = 0;
                Shoot2();
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
                PlayerPrefs.SetInt("Flam", PlayerPrefs.GetInt("Flam") + 1);
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
                GameObject bullet = Instantiate(bulletPrefab, firePointLeft.transform.position, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = (player.position - bullet.transform.position).normalized * bulletForce;
                Destroy(bullet, range);
            }
        }
        else
        {
            if (objective.y > rb.position.y)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePointUp.transform.position, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = (player.position - bullet.transform.position).normalized * bulletForce;
                Destroy(bullet, range);
            }
            else if (objective.y < rb.position.y)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePointDown.transform.position, Quaternion.Euler(0f, 0f, 0));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = (player.position - bullet.transform.position).normalized * bulletForce;
                Destroy(bullet, range);
            }
        }
        
    }

    void Shoot2()
    {
        GameObject bullet1 = Instantiate(bulletPrefab, firePointRight.transform.position, Quaternion.Euler(0f, 0f, 0));
        Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
        rb1.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet1, range);

        GameObject bullet2 = Instantiate(bulletPrefab, firePointLeft.transform.position, Quaternion.Euler(0f, 0f, 0));
        Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        rb2.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet2, range);

        GameObject bullet3 = Instantiate(bulletPrefab, firePointUp.transform.position, Quaternion.Euler(0f, 0f, 0));
        Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
        rb3.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet3, range);

        GameObject bullet4 = Instantiate(bulletPrefab, firePointDown.transform.position, Quaternion.Euler(0f, 0f, 0));
        Rigidbody2D rb4 = bullet4.GetComponent<Rigidbody2D>();
        rb4.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet4, range);
    }
}
