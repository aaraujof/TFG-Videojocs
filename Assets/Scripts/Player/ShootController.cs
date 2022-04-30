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

    private float nextFire = 0.0f;

    Vector2 shoot;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        // Input shoot
        shoot.x = Input.GetAxisRaw("Horizontal_shoot");
        shoot.y = Input.GetAxisRaw("Vertical_shoot");

        // Input move
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

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
        if ( (Input.GetButton("Horizontal_shoot") || Input.GetButton("Vertical_shoot")) && nextFire > Time.deltaTime + fireRate)
        {
            nextFire = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        // Fire points
        if (shoot.x < 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePointLeft.transform.position, Quaternion.Euler(0f, 0f, 180));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.left * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet, range);
        }
        else if (shoot.x > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePointRight.transform.position, Quaternion.Euler(0f, 0f, 0));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.right * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet, range);
        }
        else if (shoot.y < 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePointDown.transform.position, Quaternion.Euler(0f, 0f, -90));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.down * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet, range);
        }
        else if (shoot.y > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePointUp.transform.position, Quaternion.Euler(0f, 0f, 90));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.up * bulletForce, ForceMode2D.Impulse);
            Destroy(bullet, range);
        }
    }
}
