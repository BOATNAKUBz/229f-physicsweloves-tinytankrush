using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public float moveSpeed = 3f;      
    public int damageToPlayer = 20;   
    public float stopDistance = 0.3f; 

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

       
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            moveSpeed * Time.deltaTime
        );

       
        transform.LookAt(player);

        
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= stopDistance)
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

        
        Destroy(gameObject);
    }
}