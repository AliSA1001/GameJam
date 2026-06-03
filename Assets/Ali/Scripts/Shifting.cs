using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shifting : MonoBehaviour
{
    [SerializeField] private float currentCharge;
    [SerializeField] private float maxCharge;
    [SerializeField] private float perfectShiftBegin;
    [SerializeField] private float perfectShiftEnd;
    [SerializeField] private bool isCharging = false;

    [Header("Shift Numbers")]
    [SerializeField] private float shiftSpeed;

    [Header("Conection")]
    [SerializeField] private Movement2 Movement2; // we can conect it in start but we will do the inspector thing
    [SerializeField] private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Movement2 = GetComponent<Movement2>();
    }

    void Update()
    {
        if (isCharging)
        {
            Movement2.SwitchMoveState(false);
            currentCharge -= Time.deltaTime;
            if(currentCharge < 0)
            {

            }
        }
    }



    public void OnShift(InputAction.CallbackContext context)
    {
        isCharging = context.ReadValueAsButton();// it will be true if he is Charging
        isCharging = true;
    }

}
