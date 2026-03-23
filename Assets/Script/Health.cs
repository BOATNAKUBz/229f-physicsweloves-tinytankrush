using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHp = 100f;
    public float currentHp;

    void Start()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float dmg)
    {
        currentHp -= dmg;
        Debug.Log(gameObject.name + " HP: " + currentHp);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (CompareTag("Player"))
        {
            Debug.Log("Game Over!");
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetPercent()
    {
        return currentHp / maxHp;
    }
}