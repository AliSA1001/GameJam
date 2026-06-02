using UnityEngine;
using UnityEngine.AI;

public class EnemySpawning : MonoBehaviour
{
    public GameObject agentPrefab;
    public float spawnRadius = 20f;

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    System.Collections.IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            EnemySpawn();
        }
    }


    void EnemySpawn()
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
                Instantiate(agentPrefab, hit.position, Quaternion.identity);
                return;
            }
        }

    }
}