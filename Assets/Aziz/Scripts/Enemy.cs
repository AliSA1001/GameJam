using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour , IDamgeable
{
    // hidden systems
    private EnemyHealthSystem EnemyHPValues;

    [Header("Targets")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] public GameObject enemyObject;
    // [SerializeField] public GameObject enemyItemDrop;
    [Header("Debug Target")]
    [SerializeField] private Vector3 debugTarget;

    [SerializeField] private float checkRadius = 0.5f;

    [Header("Gizmo")]
    [SerializeField] private Color gizmoColor = Color.green;

    [Header("Health")]
    [SerializeField] private float numEnemyHP;


    [Header("player score system")]
    [SerializeField] private Score playerScore;

    [Header("EffectManager")]
    [SerializeField] private EffectSpawner effectManager;

    void Awake()
    {
        agent.areaMask = NavMesh.AllAreas;
    }

    void Start()
    {
        EnemyHPValues = gameObject.AddComponent<EnemyHealthSystem>();
    }


    void Update()
    {
        MoveTo(target.position);


        EnemyHPValues.EnemyHealth(numEnemyHP, enemyObject);

        // enemyItemDrop.transform.position = enemyObject.transform.position;
    }

    public void MoveTo(Vector3 destination)
    {
        debugTarget = destination;

        /*if (GridManager.Instance.IsBlocked(destination))
        {
            return;
        }
        */
        agent.SetDestination(destination);

    }

    


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawSphere(debugTarget, 0.2f);

        if (Application.isPlaying)
        {
            Gizmos.DrawLine(transform.position, debugTarget);
        }
    }

    public void TakeDamage(float amount)
    {
        numEnemyHP -= amount;
        if(numEnemyHP <= 0)
        {
            playerScore.AddScore(100);
            effectManager.DeathEffect(gameObject.transform);
            Destroy(gameObject);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // deal damage every frame
            other.GetComponent<HealthSystems>().TakeDamage(10f * Time.deltaTime);
        }
    }
}