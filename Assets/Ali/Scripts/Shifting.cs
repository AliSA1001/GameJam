using UnityEngine;
using UnityEngine.InputSystem;

public class Shifting : MonoBehaviour
{
    [SerializeField] private float currentCharge;
    [SerializeField] private float maxCharge;
    [SerializeField] private float perfectShiftBegin;
    [SerializeField] private float perfectShiftEnd;
    [SerializeField] private bool isCharging = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnShift(InputAction.CallbackContext context)
    {
        isCharging = context.ReadValueAsButton();// it will be true if he is Charging
        isCharging = true;
    }

}
