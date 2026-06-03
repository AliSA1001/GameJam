using UnityEngine;
using UnityEngine.InputSystem;

public class Shifting : MonoBehaviour
{
    [SerializeField] private float currentCharge;
    [SerializeField] private float maxCharge;
    [SerializeField] private bool isCharging = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShift(InputAction.CallbackContext context)
    {
        isCharging = context.ReadValueAsButton();// it will be true if he is Charging
    }

}
