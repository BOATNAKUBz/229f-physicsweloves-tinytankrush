using UnityEngine;
using UnityEngine.InputSystem;

public class TurretController : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public float maxAngle = 90f;

    public AudioSource audioSource;
    public AudioClip shootSound;

    private float currentRotation = 0f;

    private InputAction moveAction;
    private InputAction shootAction;

    [Header("Effects")]
    public ParticleSystem dirtParticle;
    public ParticleSystem hitEffect;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;

    [Header("Bullet Settings")]
    public float bulletSpeed = 15f;
    public int bulletDamage = 10;

    [Header("Cooldown")]
    public float fireRate = 0.3f;   // เวลาที่ต้องรอก่อนยิงครั้งต่อไป
    private float fireTimer = 0f;   // ตัวจับเวลา

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Shoot");
    }

    void Update()
    {
        RotateTurret();
        Shoot();

        // เพิ่มเวลาเสมอ
        fireTimer += Time.deltaTime;
    }

    void RotateTurret()
    {
        float verticalInput = moveAction.ReadValue<Vector2>().y;
        float rotation = verticalInput * rotationSpeed * Time.deltaTime;

        float newRotation = currentRotation + rotation;
        newRotation = Mathf.Clamp(newRotation, -maxAngle, maxAngle);

        float delta = newRotation - currentRotation;
        transform.Rotate(Vector3.up, delta);

        currentRotation = newRotation;
    }

    void Shoot()
    {
       
        if (fireTimer < fireRate)
            return;

        
        if (shootAction.triggered)
        {
            fireTimer = 0f;  // รีเซ็ตคูลดาวน์

            audioSource.PlayOneShot(shootSound);

            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Vector3 shootDir = firePoint.forward;

            Projectile proj = bullet.GetComponent<Projectile>();
            proj.Init(bulletSpeed, bulletDamage, "Player", shootDir);
        }
    }
}