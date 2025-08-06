using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Speed")]
    public float walkSpeed = 4f;
    public float sprintSpeed = 14f;
    public float maxVelocityChange = 18f;

    [Space]
    public float airControl = .5f;

    [Space]
    public float jumpHeight = 5f;



    private Vector2 input;
    private Rigidbody rb;

    private bool sprinting;
    private bool jumping;
    private bool grounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        grounded = false;
    }
    private void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input.Normalize();

        sprinting = Input.GetButton("Sprint");
        jumping = Input.GetButton("Jump");

    }

    private void OnTriggerStay(Collider other)
    {
        //if (other.gameObject.tag == "Ground")
        //{
        grounded = true;
        //}
    }

    private void FixedUpdate()
    {
        if (grounded)
        {
            if (jumping)
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, y: jumpHeight, rb.linearVelocity.z);
            }

            else if (input.magnitude > 0.5f)
            {
                rb.AddForce(CalculateMovement(sprinting ? sprintSpeed : walkSpeed), ForceMode.VelocityChange);
            }

            else
            {
                Vector3 velocity1 = rb.linearVelocity;

                velocity1 = new Vector3(velocity1.x * .2f * Time.fixedDeltaTime, velocity1.y, velocity1.z * .2f * Time.fixedDeltaTime);

                rb.linearVelocity = velocity1;

            }
        }
        else
        {
            if (input.magnitude > 0.5f)
            {
                rb.AddForce(CalculateMovement(sprinting ? sprintSpeed * airControl : walkSpeed * airControl), ForceMode.VelocityChange);
            }

            else
            {
                // FIX: Proper velocity damping
                Vector3 velocity = rb.linearVelocity;
                velocity = new Vector3(
                    velocity.x * (1 - 5f * Time.fixedDeltaTime),
                    velocity.y,
                    velocity.z * (1 - 5f * Time.fixedDeltaTime)
                );
                rb.linearVelocity = velocity;
            }
        }
        grounded = false;

    }

    private Vector3 CalculateMovement(float _speed)
    {
        Vector3 targetVelocity = new(input.x, 0, input.y);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= _speed;
        Vector3 velocity = rb.linearVelocity;
        if (input.magnitude > 0.5f)
        {
            Vector3 velocityChange = targetVelocity - velocity;
            velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
            velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
            velocityChange.y = 0;
            return velocityChange;
        }

        else
        {
            return new Vector3();
        }



    }

}
