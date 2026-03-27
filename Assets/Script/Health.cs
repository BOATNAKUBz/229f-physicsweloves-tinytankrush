using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHp = 100f;
    public float currentHp;
    public int scoreValue = 10;
    public ParticleSystem deadEffect;
    private ScoreManager scoreManager;



    void Start()
    {
        currentHp = maxHp;

        
        scoreManager = FindObjectOfType<ScoreManager>();
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
            if (scoreManager != null)
                scoreManager.GameOver();

            gameObject.SetActive(false);
        }
        else
        {
            if (scoreManager != null)
                scoreManager.AddScore(scoreValue);
            Instantiate(deadEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public float GetPercent()
    {
        return currentHp / maxHp;
    }
}