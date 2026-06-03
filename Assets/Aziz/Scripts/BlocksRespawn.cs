using UnityEngine;
using System.Collections;

public class BlocksRespawn : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform spawnPoint;

    public float respawnDelay = 5f;

    private void Start()
    {
        SpawnCube();
    }

    public void SpawnCube()
    {
        GameObject cube = Instantiate(
            cubePrefab,
            spawnPoint.position,
            spawnPoint.rotation);

        StartCoroutine(WaitForRespawn(cube));
    }

    private IEnumerator WaitForRespawn(GameObject cube)
    {
        yield return new WaitUntil(() => cube == null);

        yield return new WaitForSeconds(respawnDelay);

        SpawnCube();
    }
}
