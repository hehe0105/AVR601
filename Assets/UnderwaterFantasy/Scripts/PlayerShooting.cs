using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public GameObject bulletPrefab;//����bullet�ӵ�Ԥ����
    public Transform firePoint;//�ӵ�����λ��
    public float fireRate = 0.15f;//����(ԽСԽ��)
    public bool aimWithMouse = true;//�������׼���ص�����firepoint.right����

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
        if (aimWithMouse)
        {
            Vector3 mouseScreen = Input.mousePosition;
            mouseScreen.z = Mathf.Abs(Camera.main.transform.position.z - firePoint.position.z);
            Vector3 m = Camera.main.ScreenToWorldPoint(mouseScreen);

            Vector2 dir = (m - firePoint.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            firePoint.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
    void Shoot() {

        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = Mathf.Abs(Camera.main.transform.position.z - firePoint.position.z);
        Vector3 m = Camera.main.ScreenToWorldPoint(mouseScreen);

        Vector2 dir = (m - firePoint.position).normalized;

        var go = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        var b = go.GetComponent<Bullet>();
        b.direction = dir;                 // �����ӵ�
        b.targetTag = "Enemy";

        float ang = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        go.transform.rotation = Quaternion.Euler(0, 0, ang);
    }
}
