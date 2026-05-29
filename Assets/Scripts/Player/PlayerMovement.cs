using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    [Header("Movimiento")]
    public float moveSpeed = 5f;

    [Header("Salto")]
    public float jumpHeight = 2f;
    public float gravity = -20f;
    public int maxJumps = 2;

    [Header("Mouse")]
    public float mouseSensitivity = 200f;
    public Transform cameraPivot;

    private float xRotation;
    private float verticalVelocity;
    private int jumpCount;

    [Header("Cover")]
    public float coverMoveSpeed = 2f;
    private bool isInCover;
    private CoverPoint currentCover;

    [Header("Crouch")]
    public float crouchSpeed = 2.5f;

    public float standingHeight = 2f;
    public float crouchHeight = 1f;

    private bool isCrouching;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //Camara con Mouse
        float mouseX = Input.GetAxis("Mouse X") *
                       mouseSensitivity *
                       Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") *
                       mouseSensitivity *
                       Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraPivot.localRotation =
            Quaternion.Euler(xRotation, 0, 0);

        //Movimiento
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move;

        if (isInCover)
        {
            move = transform.right * x;
        }
        else
        {
            move =
                (transform.right * x +
                 transform.forward * z).normalized;
        }

        //Vector3 move =
        //(transform.right * x +
        //transform.forward * z).normalized;

        //controller.Move(move * moveSpeed * Time.deltaTime);

        //Detectar Suelo
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = -2f;
            }

            jumpCount = 0;
        }

        //Salto
        if (Input.GetKeyDown(KeyCode.Space)
            && jumpCount < maxJumps)
        {
            verticalVelocity =
                Mathf.Sqrt(
                    jumpHeight *
                    -2f *
                    gravity
                );

            jumpCount++;
        }

        //Gravedad
        verticalVelocity += gravity * Time.deltaTime;

        float currentSpeed =
            isInCover ? coverMoveSpeed : moveSpeed;

        // Separar horizontal y vertical
        Vector3 finalMove =
            move * currentSpeed;

        finalMove.y = verticalVelocity;

        controller.Move(finalMove * Time.deltaTime);

        // Cobertura
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isInCover && currentCover != null)
            {
                EnterCover();
            }
            else if (isInCover)
            {
                ExitCover();
            }
        }

        //Agacharse
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCrouch();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        CoverPoint cover = other.GetComponent<CoverPoint>();

        if (cover != null)
        {
            currentCover = cover;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CoverPoint cover = other.GetComponent<CoverPoint>();

        if (cover != null && currentCover == cover)
        {
            currentCover = null;

            if (isInCover)
            {
                ExitCover();
            }
        }
    }

    void EnterCover()
    {
        isInCover = true;

        transform.position =
            currentCover.coverPosition.position;

        transform.rotation =
            currentCover.lookDirection.rotation;
    }

    void ExitCover()
    {
        isInCover = false;
    }

    void ToggleCrouch()
    {
        isCrouching = !isCrouching;

        if (isCrouching)
        {
            controller.height = crouchHeight;
        }
        else
        {
            controller.height = standingHeight;
        }
    }
}