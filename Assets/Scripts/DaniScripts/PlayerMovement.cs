// Some stupid rigidbody based movement by Dani

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public DispararBala DispararBala;
    private int arma = 1;

    //Assingables
    public Transform playerCam;
    public Transform orientation;

    //Other
    private Rigidbody rb;

    //Rotation and look
    private float xRotation;
    private float sensitivity = 100f;
    private float sensMultiplier = 1f;

    //Movement
    public float moveSpeed = 100000f;
    public float maxSpeed = 30f;
    public bool grounded;
    public LayerMask whatIsGround;

    public float counterMovement = 0.175f;
    private float threshold = 0.01f;
    public float maxSlopeAngle = 40f;

    //Crouch & Slide
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale;
    public float slideForce = 500;
    public float slideCounterMovement = 0.01f;

    //Jumping
    private bool readyToJump = true;
    private float jumpCooldown = 0.25f;
    private float jumpForce = 1000f;

    //Input
    float x, y;
    bool jumping, sprinting, crouching;

    //Sliding
    private Vector3 normalVector = Vector3.up;
    private Vector3 wallNormalVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        playerScale = transform.localScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            arma = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            arma = 1;
        }

        MyInput();
        Look();
        if (Input.GetMouseButtonDown(0))
        {
            DispararBala.Shoot(xRotation, orientation.transform.eulerAngles.y, arma);
        }
        if (Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("SampleScene");
        }

    }

    private void MyInput()
    {
        if (!crouching)
        {
            x = Input.GetAxisRaw("Horizontal");
            y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            x = 0;
            y = 0;
        }

        jumping = Input.GetButton("Jump");
        crouching = Input.GetKey(KeyCode.LeftControl);

        if (Input.GetKeyDown(KeyCode.LeftControl))
            StartCrouch();
        if (Input.GetKeyUp(KeyCode.LeftControl))
            StopCrouch();
    }

    private void StartCrouch()
    {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (rb.velocity.magnitude > 0.5f)
        {
            if (grounded)
            {
                rb.AddForce(orientation.transform.forward * slideForce);
            }
        }
    }

    private void StopCrouch()
    {
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private void Movement()
    {
        //Extra gravity
        if (Vector3.Angle(normalVector, Vector3.up) < maxSlopeAngle)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 10);
        }

        //Input y dirección
        Vector3 moveDir = orientation.transform.forward * y + orientation.transform.right * x;

        //Multiplicador según pendiente
        float slopeMultiplier = 1.2f;
        if (grounded)
        {
            float slopeAngle = Vector3.Angle(normalVector, Vector3.up);
            if (slopeAngle > 0f) slopeMultiplier = 1.1f / Mathf.Cos(slopeAngle * Mathf.Deg2Rad);
        }

        moveDir = Vector3.ProjectOnPlane(moveDir, normalVector);

        float multiplier = 1f;
        if (!grounded) multiplier = 0.75f;

        rb.AddForce(moveDir * moveSpeed * Time.deltaTime * multiplier * slopeMultiplier);

        if (readyToJump && jumping) Jump();

        float effectiveMaxSpeed = maxSpeed;
        if (crouching || normalVector != Vector3.up) effectiveMaxSpeed = float.MaxValue;

        Vector2 mag = FindVelRelativeToLook();
        CounterMovement(x, y, mag, effectiveMaxSpeed);

        //Sliding down ramp: solo aplicar velocidad extra si estás agachado
        if (crouching && grounded && readyToJump)
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 3000);

            float slopeAngle = Vector3.Angle(normalVector, Vector3.up);
            if (slopeAngle > 5f)
            {
                Vector3 slideDir = Vector3.ProjectOnPlane(Vector3.down, normalVector).normalized;
                rb.AddForce(slideDir * 5000f * Time.deltaTime);
            }

            return;
        }
    }

    private void Jump()
    {
        if (grounded && readyToJump)
        {
            readyToJump = false;

            rb.AddForce(Vector2.up * jumpForce * 1.5f);
            rb.AddForce(normalVector * jumpForce * 0.5f);

            Vector3 vel = rb.velocity;
            if (rb.velocity.y < 0.5f)
                rb.velocity = new Vector3(vel.x, 0, vel.z);
            else if (rb.velocity.y > 0)
                rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private float desiredX;
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
    }

    private void CounterMovement(float x, float y, Vector2 mag, float effectiveMaxSpeed)
    {
        if (!grounded || jumping) return;

        if (crouching)
        {
            rb.AddForce(moveSpeed * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            return;
        }

        if (Math.Abs(mag.x) > threshold && Math.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Math.Abs(mag.y) > threshold && Math.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0))
        {
            rb.AddForce(moveSpeed * orientation.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }

        if (Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > effectiveMaxSpeed)
        {
            float fallspeed = rb.velocity.y;
            Vector3 n = rb.velocity.normalized * effectiveMaxSpeed;
            rb.velocity = new Vector3(n.x, fallspeed, n.z);
        }
    }

    public Vector2 FindVelRelativeToLook()
    {
        float lookAngle = orientation.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);

        return new Vector2(xMag, yMag);
    }

    private bool IsFloor(Vector3 v)
    {
        float angle = Vector3.Angle(Vector3.up, v);
        return angle < maxSlopeAngle;
    }

    private bool cancellingGrounded;

    private void OnCollisionStay(Collision other)
    {
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;

        for (int i = 0; i < other.contactCount; i++)
        {
            Vector3 normal = other.contacts[i].normal;
            if (IsFloor(normal))
            {
                grounded = true;
                cancellingGrounded = false;
                normalVector = normal;
                CancelInvoke(nameof(StopGrounded));
            }
        }

        float delay = 3f;
        if (!cancellingGrounded)
        {
            cancellingGrounded = true;
            Invoke(nameof(StopGrounded), Time.deltaTime * delay);
        }
    }

    private void StopGrounded()
    {
        grounded = false;
    }

}
