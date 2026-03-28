using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifeTime = 3f;

    public AudioClip hitSound;         // เสียงตอนโดน
    public string ownerTag;            // Player หรือ Enemy
    private Vector3 direction;

    [Header("Rotation / Magnus Effect")]
    public Vector3 angularVelocity = new Vector3(0, 20f, 0); // หมุน (deg/sec)
    public float magnusStrength = 2f;  // แรงย้อย Magnus Effect

    public ParticleSystem hitEffect;   // เอฟเฟกต์ตอนชน

    void Start()
    {
        Destroy(gameObject, lifeTime); // ตั้งเวลาออโต้ทำลาย
    }

    // ฟังก์ชันรับค่าตอนสร้างกระสุน
    public void Init(float _speed, int _damage, string _ownerTag, Vector3 shootDir)
    {
        speed = _speed;
        damage = _damage;
        ownerTag = _ownerTag;
        direction = shootDir.normalized; // ทิศทางเริ่มต้น
    }

    void Update()
    {
        // 1) หมุนกระสุน (ภาพลักษณ์)
        transform.Rotate(angularVelocity * Time.deltaTime);

        // 2) คำนวณ Magnus Effect
        Vector3 magnusForce = Vector3.Cross(angularVelocity, direction).normalized;
        direction += magnusForce * magnusStrength * Time.deltaTime;
        direction.Normalize();

        // 3) เคลื่อนกระสุน
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        // ไม่ชนเจ้าของกระสุน
        if (other.CompareTag(ownerTag)) return;

        // ชน Player หรือ Enemy
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            // ทำดาเมจ
            Health hp = other.GetComponent<Health>();
            if (hp != null)
                hp.TakeDamage(damage);

            // เล่นเสียง - Detached (ไม่หายแม้กระสุนโดนลบ)
            if (hitSound != null)
                AudioSource.PlayClipAtPoint(hitSound, transform.position);

            // เอฟเฟกต์ตอนโดน
            if (hitEffect != null)
                Instantiate(hitEffect, transform.position, Quaternion.identity);

            // ทำลายกระสุนทันที
            Destroy(gameObject);
        }
    }
}