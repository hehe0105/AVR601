using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float moveSpd = 2f;
    public float directionChangeInterval = 1.5f;  // ÿ��������һ�·���    
    public float randomOffset = 0.5f;             // ����ƫ�����ֵ
    public float chaseChance = 0.3f;              // ׷������(0~1)
    public float destroyX = -150f;                // ����Ļ����λ��

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
        // ����ǰ�����ƶ�
        transform.Translate(moveDirection * moveSpd * Time.deltaTime, Space.World);

        // ��ʱ�ı䷽��
        timer += Time.deltaTime;
        if (timer >= directionChangeInterval)
        {
            PickNewDirection();
            timer = 0f;
        }

        // ��������
        if (transform.position.x < destroyX)
            Destroy(gameObject);
    }
    void PickNewDirection()
    {
        if (player != null && Random.value < chaseChance)
        {
            // �����λ�÷����ƶ�
            moveDirection = ((Vector2)player.position - (Vector2)transform.position).normalized;
        }
        else
        {
            // �������� + �������ƫ��
            float yOffset = Random.Range(-randomOffset, randomOffset);
            moveDirection = new Vector2(-1f, yOffset).normalized;
        }
    }
}
