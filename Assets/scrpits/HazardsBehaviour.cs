using UnityEngine;
using Random = UnityEngine.Random;

public class HazardsBehaviour : MonoBehaviour
{
    public float speed = 5.0f;
    
    public BoxCollider Spawn;   // HZ_CollA
    public BoxCollider Target;  // HZ_CollB

    private Vector3 targetPosition;

    void Start()
    {
        RespawnInZone();
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
            RespawnInZone();
            Debug.Log("Touched");
        }
    }

    void RespawnInZone()
    {
        Bounds spawnBounds = Spawn.bounds;
        float randomY = Random.Range(spawnBounds.min.y, spawnBounds.max.y);
        float randomZ = Random.Range(spawnBounds.min.z, spawnBounds.max.z);
        
        transform.position = new Vector3(0, randomY, randomZ);
        
        Bounds targetBounds = Target.bounds;
        float targetY = Random.Range(targetBounds.min.y, targetBounds.max.y);
        float targetZ = Random.Range(targetBounds.min.z, targetBounds.max.z);
        
        targetPosition = new Vector3(0, targetY, targetZ);
    }
}