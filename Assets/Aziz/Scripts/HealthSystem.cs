using UnityEngine;
using TMPro;


public class HealthSystem : MonoBehaviour
{
    float health = 100;
    float damage = 1;
    float heal = 10;
    public TextMeshProUGUI blood;
    public AudioSource audioSource;

    void Health()
    {
        blood.text = " " + health;

    }

    void Damage()
    {
        health -= damage;
        blood.text = " " + health;
        audioSource.Play();


    }

    void Heal()
    {
        health += 10;
        blood.text = " " + health;
        audioSource.Play();

    }

    void OnTriggerEnter(Collider other)
    {
        //
        if (other.CompareTag("Enemy"))
        {
            Damage();
        }
        if (other.CompareTag("Medicine"))
        {
            Heal();
        }
    }

}