using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyNow", 1);
    }

    private void DestroyNow()
    {
        Destroy(gameObject);
    }
}
