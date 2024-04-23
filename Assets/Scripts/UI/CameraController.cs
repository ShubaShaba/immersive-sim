using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    private Animator animator;
    private bool isThirdPersonCamera = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        input.AddPlayersAction(PlayersActionType.Aim, SwitchMode);
    }

    private void SwitchMode(InputAction.CallbackContext context)
    {
        if (isThirdPersonCamera)
        {
            animator.Play("Aiming Camera");
        }
        else
        {
            animator.Play("Third Person Camera");
        }
        isThirdPersonCamera = !isThirdPersonCamera;
    }
}
