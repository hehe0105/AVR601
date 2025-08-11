using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 1;
    public float speed = 12f;
    public float lifeTime = 3f;
    
    
    public Vector2 direction = Vector2.right;
    public string targetTag = "Enemy"; //玩家子弹默认打Enemy

    void Awake()//确保2d子弹不受重力，碰撞用触发器
    {
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.gravityScale = 0f;

        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);//防止掉帧残留
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3)direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(targetTag)) return;

        // 打敌人
        if (targetTag == "Enemy" && other.TryGetComponent<EnemyStats>(out var es))
        {
            es.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // 打玩家
        if (targetTag == "Player" && other.TryGetComponent<PlayerHP>(out var ph))
        {
            ph.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
