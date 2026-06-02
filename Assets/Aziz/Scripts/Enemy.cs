using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform player;


    void Update()
    {
        navMeshAgent.SetDestination(player.position);
    }
}
