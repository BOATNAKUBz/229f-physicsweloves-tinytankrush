using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    public int damageToPlayer = 20;
    [Header("HP")]


    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    private float fireTimer;

    [Header("Bullet Settings")]
    public float bulletSpeed = 10f;
    public int bulletDamage = 5;

    private Transform player;
    public ParticleSystem hitEffect;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            HitPlayer();
        }
    }

    void HitPlayer()
    {
        Health hp = player.GetComponent<Health>();
        if (hp != null)
        {
            hp.TakeDamage(damageToPlayer);
        }
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}