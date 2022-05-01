using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    public Transform firePointUp;
    public Transform firePointDown;
    public Transform firePointLeft;
    public Transform firePointRight;
    public GameObject bulletPrefab;
    public Animator animator;
    public float bulletForce;
    public float fireRate;
    public float damage;
    public float range;
    public bool onigiri = false;
    public bool calamari = false;
    public bool shrimp = false;

    private GameObject player;
    private PlayerController playerController;
    private float nextFire = 0.0f;
    private float onigiriSize = 1.5f;
    private int hp;

    Vector2 shoot;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        hp = playerController.hp;
        if (hp > 0)
        {
            // Input shoot
            shoot.x = Input.GetAxisRaw("Horizontal_shoot");
            shoot.y = Input.GetAxisRaw("Vertical_shoot");

            // Shooting animation
            if ((Input.GetButton("Horizontal_shoot") || Input.GetButton("Vertical_shoot")))
            {
                animator.SetFloat("Horizontal_shoot", shoot.x);
                animator.SetFloat("Vertical_shoot", shoot.y);
                animator.SetBool("Shooting", true);
            }
            else
            {
                animator.SetFloat("Horizontal_shoot", 0f);
                animator.SetFloat("Vertical_shoot", 0f);
                animator.SetBool("Shooting", false);
            }

            // Shooting
            nextFire += Time.deltaTime;
            if ((Input.GetButton("Horizontal_shoot") || Input.GetButton("Vertical_shoot")) && nextFire > Time.deltaTime + fireRate)
            {
                nextFire = 0;
                if (calamari == false)
                {
                    Shoot();
                }
                else
                {
                    shootCalamari();
                }
            }
        }
    }

    void Shoot()
    {
        GameObject bullet;

        // Fire points
        if (shoot.x < 0)
        {
            if (onigiri == true)
            {
                if (shrimp == true)
                {
                    Vector2 left1 = new Vector2(firePointLeft.transform.position.x - 0.25f, firePointLeft.transform.position.y + 0.3f);
                    GameObject bullet1 = Instantiate(bulletPrefab, left1, Quaternion.Euler(0f, 0f, 180));
                    bullet1.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                    rb1.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet1, range);

                    Vector2 left2 = new Vector2(firePointLeft.transform.position.x - 0.25f, firePointLeft.transform.position.y - 0.3f);
                    GameObject bullet2 = Instantiate(bulletPrefab, left2, Quaternion.Euler(0f, 0f, 180));
                    bullet2.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                    rb2.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet2, range);
                }
                else
                {
                    Vector2 left = new Vector2(firePointLeft.transform.position.x - 0.25f, firePointLeft.transform.position.y + 0.1f);
                    bullet = Instantiate(bulletPrefab, left, Quaternion.Euler(0f, 0f, 180));
                    bullet.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet, range);
                }
            }
            else
            {
                if (shrimp == true)
                {
                    Vector2 left1 = new Vector2(firePointLeft.transform.position.x - 0.1f, firePointLeft.transform.position.y + 0.2f);
                    GameObject bullet1 = Instantiate(bulletPrefab, left1, Quaternion.Euler(0f, 0f, 180));
                    Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                    rb1.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet1, range);

                    Vector2 left2 = new Vector2(firePointLeft.transform.position.x - 0.1f, firePointLeft.transform.position.y - 0.2f);
                    GameObject bullet2 = Instantiate(bulletPrefab, left2, Quaternion.Euler(0f, 0f, 180));
                    Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                    rb2.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet2, range);
                }
                else
                {
                    bullet = Instantiate(bulletPrefab, firePointLeft.transform.position, Quaternion.Euler(0f, 0f, 180));
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet, range);
                } 
            }
        }
        else if (shoot.x > 0)
        {
            if (onigiri == true)
            {
                if (shrimp == true)
                {
                    Vector2 right1 = new Vector2(firePointRight.transform.position.x + 0.25f, firePointRight.transform.position.y + 0.3f);
                    GameObject bullet1 = Instantiate(bulletPrefab, right1, Quaternion.Euler(0f, 0f, 0));
                    bullet1.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                    rb1.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet1, range);

                    Vector2 right2 = new Vector2(firePointRight.transform.position.x + 0.25f, firePointRight.transform.position.y - 0.3f);
                    GameObject bullet2 = Instantiate(bulletPrefab, right2, Quaternion.Euler(0f, 0f, 0));
                    bullet2.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                    rb2.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet2, range);
                }
                else
                {
                    Vector2 right = new Vector2(firePointRight.transform.position.x + 0.25f, firePointRight.transform.position.y + 0.1f);
                    bullet = Instantiate(bulletPrefab, right, Quaternion.Euler(0f, 0f, 0));
                    bullet.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet, range);
                }
            }
            else
            {
                if (shrimp == true)
                {
                    Vector2 right1 = new Vector2(firePointRight.transform.position.x + 0.1f, firePointRight.transform.position.y + 0.2f);
                    GameObject bullet1 = Instantiate(bulletPrefab, right1, Quaternion.Euler(0f, 0f, 0));
                    Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                    rb1.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet1, range);

                    Vector2 right2 = new Vector2(firePointRight.transform.position.x + 0.1f, firePointRight.transform.position.y - 0.2f);
                    GameObject bullet2 = Instantiate(bulletPrefab, right2, Quaternion.Euler(0f, 0f, 0));
                    Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                    rb2.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet2, range);
                }
                else
                { 
                    bullet = Instantiate(bulletPrefab, firePointRight.transform.position, Quaternion.Euler(0f, 0f, 0));
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet, range);
                }
            }
        }
        else if (shoot.y < 0)
        {
            if (onigiri == true)
            {
                if (shrimp == true)
                {
                    Vector2 down1 = new Vector2(firePointDown.transform.position.x + 0.3f, firePointDown.transform.position.y - 0.25f);
                    GameObject bullet1 = Instantiate(bulletPrefab, down1, Quaternion.Euler(0f, 0f, -90));
                    bullet1.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                    rb1.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet1, range);

                    Vector2 down2 = new Vector2(firePointDown.transform.position.x - 0.3f, firePointDown.transform.position.y - 0.25f);
                    GameObject bullet2 = Instantiate(bulletPrefab, down2, Quaternion.Euler(0f, 0f, -90));
                    bullet2.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                    rb2.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet2, range);

                }
                else
                {
                    Vector2 down = new Vector2(firePointDown.transform.position.x, firePointDown.transform.position.y - 0.25f);
                    bullet = Instantiate(bulletPrefab, down, Quaternion.Euler(0f, 0f, -90));
                    bullet.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet, range);
                }
                
            }
            else
            {
                if (shrimp == true)
                {
                    Vector2 down1 = new Vector2(firePointDown.transform.position.x + 0.2f, firePointDown.transform.position.y - 0.1f);
                    GameObject bullet1 = Instantiate(bulletPrefab, down1, Quaternion.Euler(0f, 0f, -90));
                    Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                    rb1.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet1, range);

                    Vector2 down2 = new Vector2(firePointDown.transform.position.x - 0.2f, firePointDown.transform.position.y - 0.1f);
                    GameObject bullet2 = Instantiate(bulletPrefab, down2, Quaternion.Euler(0f, 0f, -90));
                    Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                    rb2.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet2, range);
                }
                else
                {
                    bullet = Instantiate(bulletPrefab, firePointDown.transform.position, Quaternion.Euler(0f, 0f, -90));
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet, range);
                }
            }
        }
        else if (shoot.y > 0)
        {
            if (onigiri == true)
            {
                if (shrimp == true)
                {
                    Vector2 up1 = new Vector2(firePointUp.transform.position.x + 0.3f, firePointUp.transform.position.y + 0.25f);
                    GameObject bullet1 = Instantiate(bulletPrefab, up1, Quaternion.Euler(0f, 0f, 90));
                    Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                    bullet1.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    rb1.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet1, range);

                    Vector2 up2 = new Vector2(firePointUp.transform.position.x - 0.3f, firePointUp.transform.position.y + 0.25f);
                    GameObject bullet2 = Instantiate(bulletPrefab, up2, Quaternion.Euler(0f, 0f, 90));
                    bullet2.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                    rb2.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet2, range);
                }
                else
                {
                    Vector2 up = new Vector2(firePointUp.transform.position.x, firePointUp.transform.position.y + 0.25f);
                    bullet = Instantiate(bulletPrefab, up, Quaternion.Euler(0f, 0f, 90));
                    bullet.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet, range);
                }
            }
            else
            {
                if (shrimp == true)
                {
                    Vector2 up1 = new Vector2(firePointUp.transform.position.x + 0.2f, firePointUp.transform.position.y + 0.1f);
                    GameObject bullet1 = Instantiate(bulletPrefab, up1, Quaternion.Euler(0f, 0f, 90));
                    Rigidbody2D rb1 = bullet1.GetComponent<Rigidbody2D>();
                    rb1.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet1, range);

                    Vector2 up2 = new Vector2(firePointUp.transform.position.x - 0.2f, firePointUp.transform.position.y + 0.1f);
                    GameObject bullet2 = Instantiate(bulletPrefab, up2, Quaternion.Euler(0f, 0f, 90));
                    Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
                    rb2.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet2, range);
                }
                else
                {
                    bullet = Instantiate(bulletPrefab, firePointUp.transform.position, Quaternion.Euler(0f, 0f, 90));
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
                    Destroy(bullet, range);
                }
            }
        }
    }

    void shootCalamari()
    {
        GameObject bulletU, bulletD, bulletL, bulletR;

        if (onigiri == true)
        {
            Vector2 left = new Vector2(firePointLeft.transform.position.x - 0.25f, firePointLeft.transform.position.y + 0.1f);
            bulletL = Instantiate(bulletPrefab, left, Quaternion.Euler(0f, 0f, 180));
            bulletL.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);

            Vector2 right = new Vector2(firePointRight.transform.position.x + 0.25f, firePointRight.transform.position.y + 0.1f);
            bulletR = Instantiate(bulletPrefab, right, Quaternion.Euler(0f, 0f, 0));
            bulletR.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);

            Vector2 down = new Vector2(firePointDown.transform.position.x, firePointDown.transform.position.y - 0.25f);
            bulletD = Instantiate(bulletPrefab, down, Quaternion.Euler(0f, 0f, -90));
            bulletD.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);

            Vector2 up = new Vector2(firePointUp.transform.position.x, firePointUp.transform.position.y + 0.25f);
            bulletU = Instantiate(bulletPrefab, up, Quaternion.Euler(0f, 0f, 90));
            bulletU.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);

            if (shrimp == true)
            {
                if (shoot.x < 0)
                {
                    Vector2 left2 = new Vector2(firePointLeft.transform.position.x - 0.25f, firePointLeft.transform.position.y + 0.4f);
                    GameObject bulletL2 = Instantiate(bulletPrefab, left2, Quaternion.Euler(0f, 0f, 180));
                    bulletL2.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rbL2 = bulletL2.GetComponent<Rigidbody2D>();
                    rbL2.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
                    Destroy(bulletL2, range);
                }
                else if (shoot.x > 0)
                {
                    Vector2 right2 = new Vector2(firePointRight.transform.position.x + 0.25f, firePointRight.transform.position.y + 0.4f);
                    GameObject bulletR2 = Instantiate(bulletPrefab, right2, Quaternion.Euler(0f, 0f, 0));
                    bulletR2.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rbR2 = bulletR2.GetComponent<Rigidbody2D>();
                    rbR2.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
                    Destroy(bulletR2, range);
                }
                else if (shoot.y < 0)
                {
                    Vector2 down2 = new Vector2(firePointDown.transform.position.x + 0.4f, firePointDown.transform.position.y - 0.25f);
                    GameObject bulletD2 = Instantiate(bulletPrefab, down2, Quaternion.Euler(0f, 0f, -90));
                    bulletD2.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rbD2 = bulletD2.GetComponent<Rigidbody2D>();
                    rbD2.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
                    Destroy(bulletD2, range);
                }
                else if (shoot.y > 0)
                {
                    Vector2 up2 = new Vector2(firePointUp.transform.position.x + 0.4f, firePointUp.transform.position.y + 0.25f);
                    GameObject bulletU2 = Instantiate(bulletPrefab, up2, Quaternion.Euler(0f, 0f, 90));
                    bulletU2.transform.localScale = new Vector3(onigiriSize, onigiriSize, onigiriSize);
                    Rigidbody2D rbU2 = bulletU2.GetComponent<Rigidbody2D>();
                    rbU2.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
                    Destroy(bulletU2, range);
                }
            }
        }
        else
        {
            bulletL = Instantiate(bulletPrefab, firePointLeft.transform.position, Quaternion.Euler(0f, 0f, 180));
            bulletR = Instantiate(bulletPrefab, firePointRight.transform.position, Quaternion.Euler(0f, 0f, 0));
            bulletD = Instantiate(bulletPrefab, firePointDown.transform.position, Quaternion.Euler(0f, 0f, -90));
            bulletU = Instantiate(bulletPrefab, firePointUp.transform.position, Quaternion.Euler(0f, 0f, 90));

            if (shrimp == true)
            {
                if (shoot.x < 0)
                {
                    Vector2 left2 = new Vector2(firePointLeft.transform.position.x, firePointLeft.transform.position.y + 0.2f);
                    GameObject bulletL2 = Instantiate(bulletPrefab, left2, Quaternion.Euler(0f, 0f, 180));
                    Rigidbody2D rbL2 = bulletL2.GetComponent<Rigidbody2D>();
                    rbL2.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
                    Destroy(bulletL2, range);

                }
                else if (shoot.x > 0)
                {
                    Vector2 right2 = new Vector2(firePointRight.transform.position.x, firePointRight.transform.position.y + 0.2f);
                    GameObject bulletR2 = Instantiate(bulletPrefab, right2, Quaternion.Euler(0f, 0f, 0));
                    Rigidbody2D rbR2 = bulletR2.GetComponent<Rigidbody2D>();
                    rbR2.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
                    Destroy(bulletR2, range);
                }
                else if (shoot.y < 0)
                {
                    Vector2 down2 = new Vector2(firePointDown.transform.position.x + 0.2f, firePointDown.transform.position.y );
                    GameObject bulletD2 = Instantiate(bulletPrefab, down2, Quaternion.Euler(0f, 0f, -90));
                    Rigidbody2D rbD2 = bulletD2.GetComponent<Rigidbody2D>();
                    rbD2.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
                    Destroy(bulletD2, range);
                }
                else if (shoot.y > 0)
                {
                    Vector2 up2 = new Vector2(firePointUp.transform.position.x + 0.2f, firePointUp.transform.position.y);
                    GameObject bulletU2 = Instantiate(bulletPrefab, up2, Quaternion.Euler(0f, 0f, 90));
                    Rigidbody2D rbU2 = bulletU2.GetComponent<Rigidbody2D>();
                    rbU2.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
                    Destroy(bulletU2, range);
                }
            }
        }

        Rigidbody2D rbL = bulletL.GetComponent<Rigidbody2D>();
        rbL.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
        Destroy(bulletL, range);

        Rigidbody2D rbR = bulletR.GetComponent<Rigidbody2D>();
        rbR.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
        Destroy(bulletR, range);

        Rigidbody2D rbD = bulletD.GetComponent<Rigidbody2D>();
        rbD.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
        Destroy(bulletD, range);

        Rigidbody2D rbU = bulletU.GetComponent<Rigidbody2D>();
        rbU.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
        Destroy(bulletU, range);
    }
}
