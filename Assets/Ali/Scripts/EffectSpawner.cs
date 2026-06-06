using UnityEngine;

public class EffectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject SpawnEffect;


    public void DeathEffect(Transform spawnPostion)
    {
        Instantiate(SpawnEffect, spawnPostion.position , spawnPostion.rotation );
    }
}
