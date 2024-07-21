using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Input System
    private PlayerInput playerInput;
    private PlayerInput.RoamingActions roaming;

    // Player Components
    private CharacterController controller;
    private PlayerInteract playerInteract;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private SpriteRenderer playerSprite;
    private Vector3 playerVelocity;

    // Player Stats
    public float speed = 5f;
    public float gravity = -9.8f;

    // Player Status
    private bool isGrounded;

    // Animation Names
    private const string WALKINGANIM = "IsWalking";

    private void Awake()
    {
        playerInput = new PlayerInput();
        roaming = playerInput.Roaming;
        controller = gameObject.GetComponent<CharacterController>();
        playerInteract = gameObject.GetComponent<PlayerInteract>();

        roaming.Interact.performed += ctx => playerInteract.InteractWithSelected();
        roaming.ToggleInteract.performed += ctx => playerInteract.CycleInteractable();
    }

    private void Update()
    {
        isGrounded = controller.isGrounded;
    }

    private void FixedUpdate()
    {
        ProcessMove();
    }

    private void ProcessMove()
    {
        Vector2 input = roaming.Movement.ReadValue<Vector2>();
        playerAnimator.SetBool(WALKINGANIM, input != Vector2.zero);
        Vector3 moveDirection = Vector3.zero;

        moveDirection.x = input.x;
        moveDirection.z = input.y;

        if (input.x > 0)
            playerSprite.flipX = false;
        else if (input.x < 0)
            playerSprite.flipX = true;

        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        //Apply gravity, set to constant force if grounded
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;

        controller.Move(playerVelocity * Time.deltaTime);

    }

    private void OnEnable()
    {
        roaming.Enable();
    }

    private void OnDisable()
    {
        roaming.Disable();
    }
}
