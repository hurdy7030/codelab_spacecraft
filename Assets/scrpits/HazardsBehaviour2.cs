using UnityEngine;
using Random = UnityEngine.Random;

public class HazardSpawner : MonoBehaviour
{
    public float speed = 5.0f;
    
    public BoxCollider Spawn;   // HZ_CollA
    public BoxCollider Target;  // HZ_CollB

    private Vector3 targetPosition; // 保存目标点坐标

    void Start()
    {
        // 游戏开始时，直接在 HZ_CollB 内随机挑一个点飞过去
        SetNewTarget(Target);
    }

    void Update()
    {
        // 从当前实际位置直接朝着目标点移动
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // 强行锁死 X 轴坐标为 0
        Vector3 currentPos = transform.position;
        currentPos.x = 0;
        transform.position = currentPos;
    }

    void OnTriggerEnter(Collider other)
    {
        // 撞到 HZ_CollB 时：不移动当前位置，直接以当前坐标飞向 HZ_CollA 里的随机点
        if (other == Target || other.name == "HZ_CollB")
        {
            SetNewTarget(Spawn);
            Debug.Log("To A");
        }
        // 撞到 HZ_CollA 时：不移动当前位置，直接以当前坐标飞向 HZ_CollB 里的随机点
        else if (other == Spawn || other.name == "HZ_CollA")
        {
            SetNewTarget(Target);
            Debug.Log("To B");
        }
    }

    // 仅更新目标点坐标，完全不改变物体的 transform.position
    void SetNewTarget(BoxCollider targetZone)
    {
        Bounds bounds = targetZone.bounds;
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        targetPosition = new Vector3(0, randomY, randomZ);
    }
}