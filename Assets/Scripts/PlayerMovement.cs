using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Player Controls
    [SerializeField] GameObject playerCamera;
    [SerializeField]
    [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] public bool cursorLock = true;
    [SerializeField] public float mouseSensitivity;
    [SerializeField] public float speed = 6.0f;
    [SerializeField] public float walkSpeed = 6.0f;
    [SerializeField] public float sprintSpeed = 12.0f;
    [SerializeField]
    [Range(0.0f, 0.5f)] float moveSmoothTime = 0.0f;
    [SerializeField] float gravity = -30f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    public float jumpHeight = 6f;
    float velocityY;
    bool isGrounded;

    float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    CharacterController controller;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    Vector3 velocity;

    // Scripts --
    GameManager gameManager;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        cursorLock = true;
    }

    // Update is called once per frame
    void Update()
    {


        UpdateMove();
        UpdateMouse();
        CursorControl();

    }

    void UpdateMouse()
    {
        if (gameManager.isPaused)
            return;

        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);
        playerCamera.transform.localEulerAngles = Vector3.right * cameraCap;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    void UpdateMove()
    {
        if (gameManager.isPaused)
            return;
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        velocityY += gravity * 2f * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * speed + Vector3.up * velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded! && controller.velocity.y < -1f)
        {
            velocityY = -8f;
        }
    }

    void CursorControl()
    {
        if (!gameManager.isPaused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }


    }
}
