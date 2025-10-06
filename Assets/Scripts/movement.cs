using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class movement : MonoBehaviour
{


    [SerializeField] float sensibilidad = 300f;
    float verticalRotation;
    float verticalClampAngle = 90f;
    [SerializeField] GameObject playerCamera;



    public float baseSpeed = 8f;
    public float jumpForce = 10f;
    public float slideSpeedDampen = 0.99f;


    [SerializeField] bool isGrounded;
    [SerializeField] bool isSliding;
    bool jumpInitiated = false;
    bool slideInitiated = false;

    [HideInInspector] public Rigidbody rb;
    public CollisionDetectorRaycast bottomCollider;
    public CollisionDetectorRaycast rightCollider;
    public CollisionDetectorRaycast leftCollider;


    float lastSpeedBeforeJump;
    Vector3 lastPosition;


    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
        lastPosition = transform.position;
    }
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) jumpInitiated = true;
        DireccionCamaraHorizontal();
        DireccionCamaraVertical();
    }

    void FixedUpdate()
    {
        Move();
        Jump(jumpForce);

        SetIsGrounded(bottomCollider.IsColliding);

    }

    void DireccionCamaraHorizontal()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        transform.Rotate(0f, mouseX, 0f);
    }

    void DireccionCamaraVertical()
    {
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClampAngle, verticalClampAngle);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 movementDirection = transform.right * x + transform.forward * 2;

        if (movementDirection == Vector3.zero) {
            if (isGrounded) rb.velocity = rb.velocity * 0.6f;
            return;
        }

        float horizontalSpeed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        float speedToAply = Mathf.Max(baseSpeed, horizontalSpeed);
        if (!isGrounded) speedToAply = lastSpeedBeforeJump;

        if (speedToAply > baseSpeed) speedToAply *= 0.985f;

        Vector3 newVelocity = movementDirection.normalized * speedToAply;
        newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;
    }

    public void Jump(float force)
    {
        if (jumpInitiated)
        {
            jumpInitiated = false;
            if (isGrounded) return;

            lastSpeedBeforeJump = rb.velocity.magnitude;

            rb.velocity += Vector3.up * force;
        }
    }
    void SetIsGrounded(bool state)
    {
        isGrounded = state;
    }

    void Slide()
    {
        if (slideInitiated)
        { 
        if (!isGrounded) return;

        slideInitiated = false;

         if (isSliding) return;

         StartSliding();
        }

        if(isSliding)
        {
            Vector3 newVelocity = rb.velocity * slideSpeedDampen;
        }
    }
    void StartSliding()
    {
        slideInitiated = false;
    }








}

