using UnityEngine;
using Random = UnityEngine.Random;

public class PowerBehaviour : MonoBehaviour
{
    public float speed = 5.0f;
    
    public BoxCollider Spawn;   // HZ_CollA
    public BoxCollider Target;  // HZ_CollB

    private Vector3 targetPosition;

    void Start()
    {
        SetNewTarget(Target);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
        Vector3 currentPos = transform.position;
        currentPos.x = 0;
        transform.position = currentPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other == Target || other.name == "HZ_CollB")
        {
            SetNewTarget(Spawn);
            Debug.Log("To A");
        }
        else if (other == Spawn || other.name == "HZ_CollA")
        {
            SetNewTarget(Target);
            Debug.Log("To B");
        }
    }
    
    void SetNewTarget(BoxCollider targetZone)
    {
        Bounds bounds = targetZone.bounds;
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        targetPosition = new Vector3(0, randomY, randomZ);
    }
}