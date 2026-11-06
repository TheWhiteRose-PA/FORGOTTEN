using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class TPM : MonoBehaviour
{
    public float Acceleration;
    public float TopSpeed;

    private Vector2 Movement;

    private Rigidbody RB;
    private PlayerInput Input;
    private Animator Anim;

    private float rotationY;

    private void Awake()
    {
        RB = GetComponent<Rigidbody>();
        Input = GetComponent<PlayerInput>();
        Anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Vector3 Direction = transform.forward * Movement.y + transform.right * Movement.x;

        RB.AddForce(Direction * Acceleration);

        Vector3 horizontalMovement = new(RB.linearVelocity.x, 0, RB.linearVelocity.z);
        horizontalMovement = Vector3.ClampMagnitude(horizontalMovement, TopSpeed);

        Vector3 updatedVelocity = horizontalMovement + new Vector3(0, RB.linearVelocity.y, 0);

        RB.linearVelocity = updatedVelocity;

        if (Input.devices[0] is Gamepad)
        {
            Vector2 input = Input.actions["Look"].ReadValue<Vector2>();
            rotationY += input.x * 1;

            transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        Movement = input;
        Anim.SetFloat("Speed", input.magnitude);
    }

    public void Look(InputAction.CallbackContext ctx)
    {
        Vector2 input = ctx.ReadValue<Vector2>();
        rotationY += input.x * 1;

        transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {

        }
    }

    public void Sprint(InputAction.CallbackContext ctx)
    {

    }
}
