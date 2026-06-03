using UnityEngine;
using UnityEngine.AI;

public class EnemySpawning : MonoBehaviour
{
    public GameObject spawnAgentBasicEnemy;
    public GameObject spawnAgentBigEnemy;
    public GameObject spawnAgentTinyEnemy;
    public float spawnRadius = 20f;

    void Start()
    {
        StartCoroutine(SpawnLoopBasicEnemy());
        StartCoroutine(SpawnLoopBigEnemy());
        StartCoroutine(SpawnLoopTinyEnemy());
    }

    System.Collections.IEnumerator SpawnLoopBasicEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            EnemySpawn(spawnAgentBasicEnemy);
        }
    }

    System.Collections.IEnumerator SpawnLoopBigEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(60f, 65f));
            EnemySpawn(spawnAgentBigEnemy);
        }
    }

    System.Collections.IEnumerator SpawnLoopTinyEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(15f, 30f));
            EnemySpawn(spawnAgentTinyEnemy);
            EnemySpawn(spawnAgentTinyEnemy);
            EnemySpawn(spawnAgentTinyEnemy);
            EnemySpawn(spawnAgentTinyEnemy);
            EnemySpawn(spawnAgentTinyEnemy);
        }
    }


    void EnemySpawn(GameObject enemy)
    {
        Camera cam = Camera.main;

        for (int i = 0; i < 100; i++)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPos.y = 0f;

            Vector3 viewportPos = cam.WorldToViewportPoint(randomPos);
            bool isVisible = viewportPos.z > 0 && viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1;

            if (!isVisible && NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 2f, NavMesh.AllAreas))
            {
                Instantiate(enemy, hit.position, Quaternion.identity);
                return;
            }
        }

    }
}