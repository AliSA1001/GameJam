using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{


    public void EnemyHealth(float HP, GameObject gameObject)
    {
        if (HP <= 0) 
        {

            Object.Destroy(gameObject);

            // item = Instantiate(item);
            // item.SetActive(true);

            // item.transform.position = gameObject.transform.position;
        }


    }


}
