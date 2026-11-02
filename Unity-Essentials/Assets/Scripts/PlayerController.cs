using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8;
    public float turnSpeed = 10;
    public float jumpForce = 8;
    public float jumpFreeSpace = 1.1f;

    [Header("Input Actions")]
    public InputActionReference moveAction;
    public InputActionReference jumpAction;
    
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 movement;

    private Transform mainCamera;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; 
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        mainCamera = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;
    }

    private void OnEnable(){
        if (moveAction != null) moveAction.action.Enable();
        if (jumpAction != null) jumpAction.action.Enable();
    }

    private void OnDisable(){
        if (moveAction != null) moveAction.action.Disable();
        if (jumpAction != null) jumpAction.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, jumpFreeSpace);

        Vector2 inputDir = moveAction.action.ReadValue<Vector2>();
        float x = inputDir.x;
        float z = inputDir.y;
        //float x = Input.GetAxisRaw("Horizontal");
        //float z = Input.GetAxisRaw("Vertical");

        Vector3 camForward = mainCamera.forward;
        Vector3 camRight = mainCamera.right;

        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();
        
        //movement = new Vector3(x, 0, z).normalized;
        movement = (camForward * z + camRight * x).normalized;

        if(movement.magnitude > 0.1f){
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        if(/*Input.GetButtonDown("Jump")*/jumpAction.action.WasPressedThisFrame() && isGrounded){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = movement * moveSpeed;

        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }


                
}
