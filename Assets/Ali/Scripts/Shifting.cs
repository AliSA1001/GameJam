using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shifting : MonoBehaviour
{
    [SerializeField] private float currentCharge;
    [SerializeField] private float maxCharge;
    [SerializeField] private float perfectShiftBegin;
    [SerializeField] private float perfectShiftEnd;
    [SerializeField] private bool canCharge;
    [SerializeField] private bool isCharging = false;
    [SerializeField] private bool isShifting;
    [SerializeField] private float currentShiftingTime;
    [SerializeField] private float shiftingTime;
    [SerializeField] private float currentShiftCoolDown = 0;

    [Header("Shift Numbers")]
    [SerializeField] private float shiftSpeed;
    [SerializeField] private float shiftCoolDown;

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

        if(isShifting)
        {
            Vector3 diraction = gameObject.transform.forward;
            characterController.Move(diraction * shiftSpeed * Time.deltaTime);
            currentShiftingTime -= Time.deltaTime;
            if(currentShiftingTime < 0)
            {
                isShifting = false;
                

            }
        }
        if (shiftCoolDown <= 0 )
        {
            canCharge = true;
        }


        if (isCharging)
        {
            Movement2.SwitchMoveState(false);
            currentCharge += Time.deltaTime;
          
        }

        if(!canCharge)
        {
            currentShiftCoolDown -= Time.deltaTime;
            if(currentShiftCoolDown <= 0)
            {
                canCharge = true;
                currentShiftCoolDown = shiftCoolDown;
            }
        }

    }

    private void ShiftingNow(bool isPerfectShift)
    {
        currentShiftingTime = shiftingTime;
        isShifting = true;
        Movement2.SwitchMoveState(true);
        currentCharge = 0;
        canCharge = isPerfectShift;
    }

    public void OnShift(InputAction.CallbackContext context)
    {
        if (canCharge && context.performed)
        {
            isCharging = true;
            
        }
        if(context.canceled && isCharging)
        {
            isCharging= false;
            if (currentCharge <= perfectShiftEnd && currentCharge >= perfectShiftBegin)
            {
                ShiftingNow(true);
            }

            else
            {
                ShiftingNow(false);
            }
                  
        }
    }

}
