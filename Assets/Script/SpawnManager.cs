using UnityEngine;

public class SpawnManager : MonoBehaviour
{
  
    public GameObject[] EnemyPrefabs;

    private int EnemyIndex;
    public float spawnRangeX = 4;

    void Start()
    {
        InvokeRepeating(nameof(Spawn), 1, 1f);
    }

    void Spawn()
    {
        EnemyIndex = Random.Range(0, EnemyPrefabs.Length);
        Vector3 spawnPos = new(
            Random.Range(-spawnRangeX, spawnRangeX),
            transform.position.y,
            transform.position.z
        );
        Instantiate(
            EnemyPrefabs[EnemyIndex],
            spawnPos,
            EnemyPrefabs[EnemyIndex].transform.rotation
        );
    }
}