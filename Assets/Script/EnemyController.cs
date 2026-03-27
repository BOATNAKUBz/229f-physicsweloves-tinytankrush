using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;

    [Header("HP")]


    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    private float fireTimer;

    [Header("Bullet Settings")]
    public float bulletSpeed = 10f;
    public int bulletDamage = 5;


    void Update()
    {
        MoveForward();
        Shoot();
    }

    void MoveForward()
    {
      
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void Shoot()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            
            Vector3 shootDir = firePoint.forward;

            Projectile proj = bullet.GetComponent<Projectile>();
            proj.Init(bulletSpeed, bulletDamage, "Enemy", shootDir);

            fireTimer = 0f;
        }
    }

}