using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    [HideInInspector]
    public Vector2 moveInput;
    public float speed;
    private Joystick js;

    void Start()
    {
        js = FindObjectOfType<FixedJoystick>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (js.Vertical != 0)
        {
            moveInput.y = js.Vertical;
        }

        if (js.Horizontal != 0)
        {
            moveInput.x = js.Horizontal;
        }

        if (moveInput.x != 0 || moveInput.x != 0)
        {
            rb.MovePosition(moveInput * speed);
        }
    }
}
