using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IItemCarrier
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float playerRadius = .55f;
    [SerializeField] private float throwStrength = 9f;
    private float maxThrowDistance = 8f;
    private float turnSmoothVelocity;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private PlayerInput input;
    [SerializeField] private Transform mountingPoint;
    private ICarryableItem carryableItem;
    private bool isAiming;

    /*
        TODO: 
        Rigidbody physics is processed within FixedUpdate() => Mixing it with transform.postion modification from Update()
        causes visuals jitters in player's movement. 

        Solution 1: Handle movement in FixedUpdate(). (Not a scalable solution in terms of the frames per second)
        Solution 2: Write a simple gravitational script. 
    */

    private void Start()
    {
        isAiming = false;
        // Subscribing to the publisher (player input sysytem)
        input.AddPlayersAction(PlayersActionType.Interact, InteractionHandler);
        input.AddPlayersAction(PlayersActionType.Aim, AimingHandler);
        input.AddPlayersAction(PlayersActionType.Main, MainActionHandler);
    }
    private void Update()
    {
        MovementHandler();
    }

    // TODO: Create a separate point for shooting:
    private void MainActionHandler(InputAction.CallbackContext context)
    {
        if (isAiming && !IsEmpty())
        {
            Throw();
        }
        else if (isAiming)
        {
            RayCastShoot.Shoot(mountingPoint.position, transform.forward);
        }
    }

    private void InteractionHandler(InputAction.CallbackContext context)
    {
        IInteractable selectedInteractable = PlayerInput.selectedInteractable;

        if (selectedInteractable != null)
        {
            selectedInteractable.Interact(transform);
            Debug.Log(selectedInteractable.getInteractHint());
        }
    }

    private void AimingHandler(InputAction.CallbackContext context)
    {
        isAiming = !isAiming;
    }

    private void RotationHandler(Vector3 moveDirection)
    {
        Vector3 direction = moveDirection;
        float rotationalTime = 0.1f;
        if (isAiming)
        {
            direction = input.GetCursorPosition() - transform.position;
            rotationalTime = 0.05f;
        }

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        if (moveDirection.magnitude != 0 || isAiming)
        {
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, rotationalTime);
        }
    }

    private void MovementHandler()
    {
        float moveDistance = moveSpeed * Time.deltaTime;

        // Calculating the direction of movement relative to the camera
        Vector3 inputDirection = input.GetInputDirectionNormalized();
        Vector3 camForward = new Vector3(cameraPosition.forward.x, 0, cameraPosition.forward.z);
        Vector3 camRight = new Vector3(cameraPosition.right.x, 0, cameraPosition.right.z);
        Vector3 moveDirection = (inputDirection.z * camForward + inputDirection.x * camRight).normalized;

        int obstaclesLayerMask = LayerMask.GetMask("Obstacles");
        bool canMove = !Physics.CapsuleCast(
            transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance, obstaclesLayerMask);

        RotationHandler(moveDirection);
        if (moveDirection.magnitude != 0)
        {
            transform.position += Convert.ToInt16(canMove) * moveDirection * moveDistance;
        }
    }

    private void Throw()
    {
        IThrowable throwable = carryableItem as IThrowable;
        carryableItem.RemoveCarrier();
        // throwable?.Throw((transform.forward + transform.up) * throwStrength);
        throwable?.Throw(ThrowDirection(throwable.GetMass()) * throwStrength);
    }

    private Vector3 ThrowDirection(float throwableMass)
    {
        // TODO: Refactor
        float startVelocity = throwStrength / throwableMass;
        float horizontalDestination = Mathf.Min(Vector3.Distance(mountingPoint.position, input.GetCursorPosition()), maxThrowDistance);
        float initialHeight = mountingPoint.position.y;

        //Calculating the angle:
        float phaseShift = Mathf.Atan(horizontalDestination / initialHeight);
        float aConstant = 9.8f * Mathf.Pow(horizontalDestination, 2) / (2 * Mathf.Pow(startVelocity, 2));
        float angleTimes2 = Mathf.Acos((2 * aConstant - initialHeight) / Mathf.Sqrt(Mathf.Pow(initialHeight, 2) + Mathf.Pow(horizontalDestination, 2))) + phaseShift;
        
        return Quaternion.AngleAxis(-(angleTimes2 / 2) * Mathf.Rad2Deg, transform.right) * transform.forward;
    }

    public (float, float, Vector3) GetThrowingObjectData()
    {
        IThrowable throwable = carryableItem as IThrowable;
        if (throwable != null)
        {
            return (throwable.GetMass(), throwStrength, ThrowDirection(throwable.GetMass()));
        }
        return (0f, 0f, Vector3.zero);
    }

    public Transform GetMountingPoint()
    {
        return mountingPoint;
    }

    public bool Inject(ICarryableItem item)
    {
        carryableItem = item;
        return true;
    }

    public void Eject()
    {
        carryableItem = null;
    }

    public bool IsEmpty()
    {
        return carryableItem == null;
    }

    public ICarryableItem GetItem()
    {
        return carryableItem;
    }
}