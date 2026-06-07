using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyNow", 3);
    }

    private void DestroyNow()
    {
        Destroy(gameObject);
    }
}
