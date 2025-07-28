using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float minY = -3f;
    public float maxY = 3f;

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval) {
            SpawnEnemy();
            timer = 0f;
        }
    }
    void SpawnEnemy() {
        Vector3 spawnPos = new Vector3(300f,Random.Range(minY,maxY),-5f);
        Instantiate(enemyPrefab,spawnPos,Quaternion.identity);
    }
}
