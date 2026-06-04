using Unity.VisualScripting;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] private int score = 0;


    public void AddScore(int amount)
    {
        score += amount;
    }
}
