using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;

    [Header("Debug Target")]
    public Vector3 debugTarget;

    [Header("Gizmo")]
    public Color gizmoColor = Color.green;

    public float checkRadius = 0.5f;

    void Update()
    {
        MoveTo(target.position);
    }

    public void MoveTo(Vector3 destination)
    {
        debugTarget = destination;

        if (GridManager.Instance.IsBlocked(destination))
        {
            return;
        }

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
}