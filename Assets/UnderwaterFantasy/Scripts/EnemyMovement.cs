using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float moveSpd = 2f;
    private Vector2 moveDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //random move 
        float y = Random.Range(-0.5f,0.5f);
        moveDirection = new Vector2(-1f, y).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(moveDirection * moveSpd * Time.deltaTime);
        //destory enemy out camera
        if (transform.position.x < -150f) {
            Destroy(gameObject);
        }
    }
}
