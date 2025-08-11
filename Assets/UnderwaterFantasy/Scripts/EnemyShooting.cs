using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireInterval = 2f;//开火间隔
    public float aimLead = 0f;//简单判断
    public int bonusDamage = 0;//额外伤害

    Transform player;
    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var p = GameObject.FindWithTag("Player");
        if (p) player = p.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player || !firePoint || !bulletPrefab) return;

        timer += Time.deltaTime;
        if (timer >= fireInterval) {
            FireAtPlayer();
            timer = 0f;
        }
    }
    void FireAtPlayer()
    {
        Vector3 target = player.position;
        if (aimLead > 0f && player.TryGetComponent<Rigidbody2D>(out var prb))
            target += (Vector3)(prb.linearVelocity * aimLead);

        Vector2 dir = (target - firePoint.position).normalized;

        var go = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        var b = go.GetComponent<Bullet>();
        b.direction = dir;
        b.targetTag = "Player";//敌人子弹攻击玩家

        //敌人攻击力设置
        if (TryGetComponent<EnemyStats>(out var es))
            b.damage = Mathf.Max(1, es.atk + bonusDamage);

        //子弹对准
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
