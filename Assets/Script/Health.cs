using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHp = 100f;
    public float currentHp;
    public int scoreValue = 10;

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
            ScoreManager sm = FindObjectOfType<ScoreManager>();
            if (sm != null)
            {
                sm.AddScore(scoreValue);
            }
                Destroy(gameObject);
        }
    }

    public float GetPercent()
    {
        return currentHp / maxHp;
    }
}