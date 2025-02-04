using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign in Inspector
    public float spawnInterval = 2f; // Initial spawn interval
    public float enemySpeed = 5f; // Speed of enemy movement
    public float intervalDecrease = 0.2f; // How much the interval decreases
    public float decreaseFrequency = 5f; // Time between decreases
    public float minSpawnInterval = 0.5f; // Prevents it from becoming too fast

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(DecreaseSpawnInterval());
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private IEnumerator DecreaseSpawnInterval()
    {
        while (true)
        {
            yield return new WaitForSeconds(decreaseFrequency);
            spawnInterval = Mathf.Max(spawnInterval - intervalDecrease, minSpawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = transform.position;
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        //enemy.AddComponent<EnemyMovement>().speed = enemySpeed;
    }
}

//public class EnemyMovement : MonoBehaviour
//{
//    public float speed;

//    private void Update()
//    {
//        transform.position += Vector3.back * speed * Time.deltaTime;
//    }
//}
