using UnityEngine;
using UnityEngine.AI;

public class EnemySpawning : MonoBehaviour
{
    public GameObject spawnAgentBasicEnemy;
    public GameObject spawnAgentBigEnemy;
    public GameObject spawnAgentTinyEnemy;
    public float spawnRadius = 20f;
    public Camera cam;

    void Start()
    {
        spawnAgentBasicEnemy.SetActive(false);
        spawnAgentBigEnemy.SetActive(false);
        spawnAgentTinyEnemy.SetActive(false);

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
            yield return new WaitForSeconds(Random.Range(10f, 15f));
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
        if (enemy == null)
        {
            Debug.LogError("Enemy prefab is null!", this);
            return;
        }

        Camera cam = Camera.main;

        for (int i = 0; i < 100; i++)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPos.y = 0f;

            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                Vector3 viewportPos = cam.WorldToViewportPoint(hit.position);
                bool isVisible = viewportPos.z > 0 && viewportPos.x > 0 && viewportPos.x < 1 && viewportPos.y > 0 && viewportPos.y < 1;

                if (!isVisible)
                {
                    GameObject spawnedEnemy = Instantiate(enemy, hit.position, Quaternion.identity);
                    spawnedEnemy.SetActive(true);
                    return;
                }
            }
        }

        // Fallback: spawn anywhere on NavMesh regardless of visibility
        for (int i = 0; i < 100; i++)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
            randomPos.y = 0f;

            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 5f, NavMesh.AllAreas))
            {
                GameObject spawnedEnemy = Instantiate(enemy, hit.position, Quaternion.identity);
                spawnedEnemy.SetActive(true);
                Debug.LogWarning("Spawned in visible area — no off-screen position found.");
                return;
            }
        }

        Debug.LogError("No NavMesh found within spawn radius! Increase spawnRadius or check NavMesh bake.", this);
    }





    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}