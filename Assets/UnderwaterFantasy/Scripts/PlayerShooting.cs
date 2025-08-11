using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public GameObject bulletPrefab;//挂有bullet子弹预制体
    public Transform firePoint;//子弹生成位置
    public float fireRate = 0.15f;//射速(越小越快)
    public bool aimWithMouse = true;//用鼠标瞄准；关掉则用firepoint.right发射

    float cd = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cd -= Time.deltaTime;

        if (Input.GetButton("Fire1") && cd <= 0f) {
            Shoot();
            cd = fireRate;
        }
        if (aimWithMouse) {
            Vector3 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            m.z = 0f;
            Vector2 dir = (m - firePoint.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0,0,angle);
        }
    }
    void Shoot() {
        var go = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var b = go.GetComponent<Bullet>();
        b.direction = firePoint.right;//朝firepoint指向
        b.targetTag = "Enemy";//打敌人
        
    }
}
