using UnityEngine;

public class SimpleNavMeshEnemy : MonoBehaviour , IDamgeable
{
    [Header("Movement")]
    public Transform playerTarget;

    [Header("Combat Settings")]
    public int damageAmount = 10;
    public float attackCooldown = 1.5f;
    [SerializeField]private float hp;

    private UnityEngine.AI.NavMeshAgent agent;
    private bool canHit = true;
    private float cooldownTimer = 0f;

    void Start()
    {
        // Automatically get the NavMeshAgent component attached to this GameObject
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        // If you forgot to assign the player in the inspector, try to find them by tag
        if (playerTarget == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) playerTarget = playerObj.transform;
        }
    }

    void Update()
    {
        // 1. Pathfinding: Chase the player if they exist
        if (playerTarget != null)
        {
            agent.SetDestination(playerTarget.position);
        }

        // 2. Cooldown Timer: Handle the attack delay
        if (!canHit)
        {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= attackCooldown)
            {
                canHit = true;
                cooldownTimer = 0f;
            }
        }
    }

    // 3. Collision: Triggers when the enemy (moving via CharacterController/NavMesh) bumps into the player
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Check if we can hit, and if the object we bumped into has an IDamageable component
        if (canHit && hit.gameObject.TryGetComponent(out IDamgeable damageableTarget))
        {
            damageableTarget.TakeDamage(damageAmount);
            Debug.Log($"Hit player! Dealt {damageAmount} damage.");

            // Start cooldown
            canHit = false;
        }
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        if(hp <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
