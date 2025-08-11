using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float moveSpd = 2f;
    public float directionChangeInterval = 1.5f;  // 每隔多久随机一下方向    
    public float randomOffset = 0.5f;             // 上下偏移最大值
    public float chaseChance = 0.3f;              // 追击概率(0~1)
    public float destroyX = -150f;                // 出屏幕销毁位置

    private Vector2 moveDirection;
    private float timer;
    private Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        PickNewDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // 按当前方向移动
        transform.Translate(moveDirection * moveSpd * Time.deltaTime, Space.World);

        // 定时改变方向
        timer += Time.deltaTime;
        if (timer >= directionChangeInterval)
        {
            PickNewDirection();
            timer = 0f;
        }

        // 出界销毁
        if (transform.position.x < destroyX)
            Destroy(gameObject);
    }
    void PickNewDirection()
    {
        if (player != null && Random.value < chaseChance)
        {
            // 朝玩家位置方向移动
            moveDirection = ((Vector2)player.position - (Vector2)transform.position).normalized;
        }
        else
        {
            // 基础向左 + 随机上下偏移
            float yOffset = Random.Range(-randomOffset, randomOffset);
            moveDirection = new Vector2(-1f, yOffset).normalized;
        }
    }
}
