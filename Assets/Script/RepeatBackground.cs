using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;


    private float repeatWidth = 50;

  
    void Start()
    {
        startPos = transform.position;

        repeatWidth = GetComponent<BoxCollider>().size.z / 2;
    }

    void Update()
    {
      
        if (transform.position.z < startPos.z - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}