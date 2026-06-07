using UnityEngine;
using TMPro;


public class HealthSystems : MonoBehaviour , IDamgeable
{

    public GameObject loseScreen;
    public float health = 100;
    float damage = 5;
    int heal = 10;
    public TextMeshProUGUI blood;
    public AudioSource audioSource;
    [SerializeField] private TextMeshProUGUI HpNumber;

    void Health()
    {
        blood.text = " " + health;

    }

    private void Update()
    {

        HpNumber.text = ((int)health).ToString() +"%";
    }

    /*void Damage()
    {
        health -= damage;
        blood.text = " " + health;
        audioSource.Play();


    }*/

    void Heal()
    {
        health += 10;
        blood.text = " " + health;
        audioSource.Play();

    }

   /* void OnTriggerEnter(Collider other)
    {
        //
        if (other.CompareTag("Enemy"))
        {

        }
        if (other.CompareTag("Medicine"))
        {
            Heal();
        }
    }*/

    public void TakeDamage(float amount)
    {

        health -= amount;

        if (health <= 0)
        {
            loseScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

}