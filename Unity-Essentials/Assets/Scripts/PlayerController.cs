using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8;
    public float turnSpeed = 10;
    public float jumpForce = 8;
    public float jumpFreeSpace = 1.1f;
    
    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 movement;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, jumpFreeSpace);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        movement = new Vector3(x, 0, z).normalized;

        if(movement.magnitude > 0.1f){
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        if(Input.GetButtonDown("Jump") && isGrounded){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = movement * moveSpeed;

        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }


                
}
