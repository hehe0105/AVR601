using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("����Ԥ�����б���ͬ��ɫ/���ԣ�")]
    public GameObject[] enemyPrefabs;//���Ǳͧprefab
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

        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject selectedEnemy = enemyPrefabs[randomIndex];
        Vector3 spawnPos = new Vector3(300f,Random.Range(minY,maxY),-5f);
        Instantiate(selectedEnemy,spawnPos,Quaternion.identity);

    }
}
