using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;         // Forward movement speed
    public float rotationSpeed = 200f;   // Rotation speed
    public float driftRotationSpeed = 100f; // Drift rotation speed

    private Rigidbody2D rb;
    private bool isDrifting = false;
    private Vector2 driftAnchorPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        // Apply constant forward movement only if not drifting
        if (!isDrifting)
        {
            rb.velocity = transform.up * moveSpeed;
        }
    }

    void HandleInput()
    {
        // Handle left and right rotation with arrow keys
        float rotateInput = Input.GetAxis("Horizontal"); // Left/Right arrow keys
        if (!isDrifting)
        {
            rb.angularVelocity = -rotateInput * rotationSpeed;
        }

        // Check for left drift with A key
        if (Input.GetKey(KeyCode.A))
        {
            if (!isDrifting)
            {
                StartDrift(true); // Drift to the left, anchored on the left corner
            }
            RotateAroundAnchor(-1);
        }
        // Check for right drift with D key
        else if (Input.GetKey(KeyCode.D))
        {
            if (!isDrifting)
            {
                StartDrift(false); // Drift to the right, anchored on the right corner
            }
            RotateAroundAnchor(1);
        }
        else
        {
            // Stop drifting if A or D is released
            if (isDrifting)
            {
                StopDrift();
            }
        }
    }

    void StartDrift(bool isLeftDrift)
    {
        // Enter drift mode
        isDrifting = true;
        rb.velocity = Vector2.zero;  // Stop forward movement
        rb.angularVelocity = 0;      // Stop any existing rotation
        driftAnchorPoint = GetCornerPosition(isLeftDrift);
    }

    void StopDrift()
    {
        // Exit drift mode and resume forward movement
        isDrifting = false;
        rb.velocity = transform.up * moveSpeed; // Continue forward in new direction
    }

    Vector2 GetCornerPosition(bool isLeftCorner)
    {
        // Calculate the left or right corner based on current position and rotation
        Vector2 offset = isLeftCorner ? new Vector2(-0.5f, 0.5f) : new Vector2(0.5f, 0.5f);
        return (Vector2)transform.position + (Vector2)transform.TransformDirection(offset);
    }

    void RotateAroundAnchor(int direction)
    {
        // Rotate around the anchor point without repositioning
        transform.RotateAround(driftAnchorPoint, Vector3.forward, direction * driftRotationSpeed * Time.deltaTime);
    }
}

    // public float maxSpeed;
    // public float acc;
    // public float steering;

    // private Rigidbody2D rb;

    // float x;
    // float y = -1;

    // private void Start()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    // }

    // private void Update()
    // {
    //     x = Input.GetAxis("Horizontal");
    //     Vector2 speed = transform.up * (y * acc);
    //     rb.AddForce(speed);

    //     float direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));

    //     if(acc > 0)
    //     {
    //         if(direction > 0)
    //         {
    //             rb.rotation -= x * steering * (rb.velocity.magnitude / maxSpeed);
    //         }
    //         else
    //         {
    //             rb.rotation += x * steering * (rb.velocity.magnitude / maxSpeed);
    //         }
    //     }

    //     float driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left)) * 2.0f;

    //     Vector2 relativeForce = Vector2.right * driftForce;

    //     rb.AddForce(rb.GetRelativeVector(relativeForce));

    //     if(rb.velocity.magnitude > maxSpeed)
    //     {
    //         rb.velocity = rb.velocity.normalized * maxSpeed;
    //     }
    // }


    // Adjustable speed and rotation variables
    // public float moveSpeed = 5f;        // Forward/backward speed
    // public float rotationSpeed = 200f;  // Rotation speed
    // public float driftIntensity = 0.5f; // Controls the intensity of the drift

    // private Rigidbody2D rb;
    // private bool isDrifting = false;
    // private Vector2 savedVelocity;

    // private Vector2 driftAnchorPoint;
    // void Start()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    // }

    // void Update()
    // {
    //     HandleInput();
    // }

    // void HandleInput()
    // {
    //     // Get input for movement and rotation
    //     float moveInput = Input.GetAxis("Vertical"); // Up/Down arrow keys
    //     float rotateInput = Input.GetAxis("Horizontal"); // Left/Right arrow keys

    //     // apply force and backward movement if not drifting
    //     if (!isDrifting)
    //     {

    //         Vector2 movement = transform.up * moveInput * moveSpeed;
    //         rb.AddForce(movement);
    //         rb.angularVelocity = -rotateInput * rotationSpeed;
    //     }

    //     // Drift and spin effect
    //     if (Input.GetKey(KeyCode.Space) && Mathf.Abs(rotateInput) > 0.1f)
    //     {
    //         if(!isDrifting)
    //         {
    //             isDrifting = true;
    //             savedVelocity = rb.velocity;
    //             rb.velocity = Vector2.zero;
    //             rb.angularVelocity = 0;
    //         }
    //         //RotateAroundAnchor(rotateInput);
    //         RotateInPlace(rotateInput);
    //     }
    //     else
    //     {
    //         isDrifting = false;
    //         rb.velocity = transform.up * savedVelocity.magnitude;
    //     }
    // }

    // void RotateInPlace(float rotateInput)
    // {
    //     transform.Rotate(Vector3.forward, -rotateInput * rotationSpeed * Time.deltaTime);

    // }

    // void SetDriftAnchorPoint(float rotateInput)
    // {
    //     // determine which side to use as anchor point
    //     Vector2 offset = rotateInput > 0 ? new Vector2(transform.localScale.x / 2, transform.localScale.y / 2) : new Vector2(-transform.localScale.x / 2, transform.localScale.y / 2);
    //     driftAnchorPoint = (Vector2)transform.position + (Vector2)transform.TransformPoint(offset);

    //     // zero out velocity to rotate in place
    //     rb.velocity = Vector2.zero;
    //     rb.angularVelocity = 0;
    // }

    // void RotateAroundAnchor(float rotateInput)
    // {
    //     //lock in position and rotate around anchor point
    //     transform.position = driftAnchorPoint;
    //     transform.RotateAround(driftAnchorPoint, Vector3.forward, -rotateInput * rotationSpeed * Time.deltaTime);
    // }
