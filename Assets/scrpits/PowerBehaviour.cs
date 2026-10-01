using UnityEngine;
using Random = UnityEngine.Random;

public class PowerBehaviour : MonoBehaviour
{
    public float speed = 5.0f;
    public float rotateSpeed = 5.0f;
    public Transform PowerSpawn;
    
    public BoxCollider Spawn;   // HZ_CollA
    public BoxCollider Target;  // HZ_CollB

    private Vector3 targetPosition;

    void Start()
    {
        SetNewTarget(Target);
        transform.position = PowerSpawn.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.Rotate(rotateSpeed * Time.deltaTime, 0, 0);
        
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

        if (other.name == "rocket")
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            
            Debug.Log("PowerUP");
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