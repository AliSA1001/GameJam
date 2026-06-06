using JetBrains.Annotations;
using MoreMountains.Feedbacks;
using System.Collections.Generic;
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
    [SerializeField] private bool canHit; 
    [SerializeField] private float currentShiftCoolDown = 0;
    private List<IDamgeable> hitTargetsList = new List<IDamgeable>();

    [Header("Shift Numbers")]
    [SerializeField] private float shiftSpeed;
    [SerializeField] private float shiftCoolDown;
    [SerializeField] private float damageAmount;
    [SerializeField] private int ammoToAdd;

    [Header("Conection")]
    [SerializeField] private Movement2 Movement2; // we can conect it in start but we will do the inspector thing
    [SerializeField] private CharacterController characterController;
    [SerializeField] private GunsAli guns;
    [SerializeField] private GameObject powerUp;
    [SerializeField] private GameObject shiftTrail;

    [Header("FeedBack")]
    [SerializeField] private MMF_Player PerfectShiftFeedBack;
    [SerializeField] private MMF_Player ChargeUpFeedBack;
    private bool FeedbackPlayed = false;

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
                Movement2.SwitchMoveState(true);
                canHit = false;
                hitTargetsList.Clear();
                shiftTrail.SetActive(false);
                FeedbackPlayed = false ;

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
            if(currentCharge >= perfectShiftBegin && !FeedbackPlayed )
            {
                PerfectShiftFeedBack.PlayFeedbacks();
                FeedbackPlayed=true;
            }
          powerUp.SetActive(true);
        }
        else if (!isCharging)
        {
            powerUp.SetActive(false);
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

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isShifting && hit.gameObject.TryGetComponent(out IDamgeable damageableTarget))
        {
            if (!hitTargetsList.Contains(damageableTarget))
            {
                damageableTarget.TakeDamage(damageAmount);

                if (guns != null)
                {
                    guns.AddAmmo(ammoToAdd);
                }

                hitTargetsList.Add(damageableTarget);
            }
        }
    }

    private void ShiftingNow(bool isPerfectShift)
    {
        currentShiftingTime = shiftingTime;
        isShifting = true;
        shiftTrail.SetActive(true );
        currentCharge = 0;
        canHit = true;
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
