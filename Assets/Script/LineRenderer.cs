using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LaserAim : MonoBehaviour
{
    public Transform firePoint;
    public float maxDistance = 50f;
  
    private LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    
    }

    void Update()
    {
        Vector3 start = firePoint.position;
        Vector3 direction = firePoint.forward; 

        RaycastHit hit;

        if (Physics.Raycast(start, direction, out hit, maxDistance))
        {
            line.SetPosition(0, start);
            line.SetPosition(1, hit.point);
        }
        else
        {
            line.SetPosition(0, start);
            line.SetPosition(1, start + direction * maxDistance);
        }
    }
}