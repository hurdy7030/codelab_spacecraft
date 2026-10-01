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
        // 使用 MoveTowards 直接朝 HZ_CollB 里的目标点移动
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 强行锁死 X 轴坐标为 0
        Vector3 currentPos = transform.position;
        currentPos.x = 0;
        transform.position = currentPos;
    }

    void OnTriggerEnter(Collider other)
    {
        // 撞到 HZ_CollB 时重生
        if (other == Target || other.name == "HZ_CollB")
        {
            RespawnInZone();
            Debug.Log("Touched");
        }
    }

    void RespawnInZone()
    {
        // 1. 在 HZ_CollA 内随机出生
        Bounds spawnBounds = Spawn.bounds;
        float randomY = Random.Range(spawnBounds.min.y, spawnBounds.max.y);
        float randomZ = Random.Range(spawnBounds.min.z, spawnBounds.max.z);
        
        transform.position = new Vector3(0, randomY, randomZ);

        // 2. 在 HZ_CollB 内随机选取目标点坐标
        Bounds targetBounds = Target.bounds;
        float targetY = Random.Range(targetBounds.min.y, targetBounds.max.y);
        float targetZ = Random.Range(targetBounds.min.z, targetBounds.max.z);
        
        targetPosition = new Vector3(0, targetY, targetZ);
    }
}