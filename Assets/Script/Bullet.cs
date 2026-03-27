using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;
    public float lifeTime = 3f;

    public string ownerTag;

    private Vector3 direction;

     public ParticleSystem hitEffect;


    public void Init(float _speed, int _damage, string _ownerTag, Vector3 shootDir)
    {
        speed = _speed;
        damage = _damage;
        ownerTag = _ownerTag;

        direction = shootDir.normalized;

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ownerTag)) return;

        if (other.CompareTag("Player"))
        {
            Health hp = other.GetComponent<Health>();
            if (hp != null)
                hp.TakeDamage(damage);

            Destroy(gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            Health hp = other.GetComponent<Health>();
            if (hp != null)
                hp.TakeDamage(damage);

            Destroy(gameObject);

            ParticleSystem effect = Instantiate(hitEffect, other.transform.position, Quaternion.identity);

            effect.Play();
        }
    }
}