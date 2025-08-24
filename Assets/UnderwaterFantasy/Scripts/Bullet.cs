using UnityEngine;

public class Bullet : MonoBehaviour
{

    public int damage = 1;
    public float speed = 12f;
    public float lifeTime = 3f;
    
    
    public Vector2 direction = Vector2.right;
    public string targetTag = "Enemy"; //����ӵ�Ĭ�ϴ�Enemy

    public AudioClip shootClip;
    public float shootVolume = 1f;
    public bool playOnlyForPlayerBullet = true;
    public AudioSource twoDSFXSource;

    void Awake()//ȷ��2d�ӵ�������������ײ�ô�����
    {
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.gravityScale = 0f;

        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);//��ֹ��֡����
        if(shootClip != null)
        {
            bool isPlayBullet = (targetTag == "Enemy");
            if(!playOnlyForPlayerBullet || isPlayBullet)
            {
                if(twoDSFXSource != null)
                {
                    twoDSFXSource.PlayOneShot(shootClip, shootVolume);
                }
                else
                {
                    var listenerPos = Camera.main ? Camera.main.transform.position : transform.position;
                    AudioSource.PlayClipAtPoint(shootClip, listenerPos, shootVolume);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3)direction * speed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(targetTag)) return;

        // �����
        if (targetTag == "Enemy" && other.TryGetComponent<EnemyStats>(out var es))
        {
            es.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // �����
        if (targetTag == "Player" && other.TryGetComponent<PlayerHP>(out var ph))
        {
            ph.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
