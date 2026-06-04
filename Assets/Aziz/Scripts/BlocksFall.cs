using UnityEngine;
using Unity.AI.Navigation;
using System.Collections;

public class BlocksFall : MonoBehaviour
{
    [Header("References")]
    public NavMeshSurface surface;
    public Animator animator;

    [Header("Timing")]
    public float fallTime = 2f;
    public float riseTime = 2f;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(Cycle());
    }

    private IEnumerator Cycle()
    {
        while (true)
        {
            //--------------------------------------------------
            // RANDOM DELAY BEFORE FALL
            //--------------------------------------------------
            yield return new WaitForSeconds(Random.Range(3f, 8f));

            animator.SetBool("Fall", true);
            animator.SetBool("Back", false);

            yield return new WaitForSeconds(fallTime);

            // MOVE CUBE OUT OF NAVMESH (VISUAL FALL)
            transform.position = new Vector3(
                originalPosition.x,
                -500f,
                originalPosition.z
            );

            //--------------------------------------------------
            // REBUILD NAVMESH (CUBE REMOVED)
            //--------------------------------------------------
            surface.BuildNavMesh();

            //--------------------------------------------------
            // RANDOM HIDDEN TIME (5–10 SECONDS)
            //--------------------------------------------------
            yield return new WaitForSeconds(Random.Range(5f, 10f));

            animator.SetBool("Fall", false);
            animator.SetBool("Back", true);

            yield return new WaitForSeconds(riseTime);

            // RESTORE CUBE POSITION
            transform.position = originalPosition;

            //--------------------------------------------------
            // REBUILD NAVMESH AGAIN (CUBE RETURNS)
            //--------------------------------------------------
            surface.BuildNavMesh();

            animator.SetBool("Fall", false);
            animator.SetBool("Back", false);
        }
    }
}