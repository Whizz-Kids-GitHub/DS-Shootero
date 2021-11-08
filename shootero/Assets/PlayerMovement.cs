using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed;
    public float minRange;

    private Vector2 oMoveInput; //original
    [HideInInspector]
    public Vector2 moveInput; //final
    private float aMoveInput; //x + y - 1
    
    private Joystick js;


    void Start()
    {
        js = FindObjectOfType<FixedJoystick>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        oMoveInput.y = js.Vertical;
        oMoveInput.x = js.Horizontal;

        if (oMoveInput.x < 0 && oMoveInput.y >= 0)
        {
            aMoveInput = -oMoveInput.x + oMoveInput.y;

            aMoveInput -= 1;
        }
        else if (oMoveInput.y < 0 && oMoveInput.x >= 0)
        {
            aMoveInput = -oMoveInput.y + oMoveInput.x;

            aMoveInput -= 1;
        }
        else if (oMoveInput.x < 0 && oMoveInput.y < 0)
        {
            aMoveInput = -oMoveInput.y - oMoveInput.x;

            aMoveInput -= 1;
        }
        else
        {
            aMoveInput = oMoveInput.y + oMoveInput.x;

            aMoveInput -= 1;
        }

        if (oMoveInput.x < 0 && oMoveInput.y >= 0)
        {
            moveInput.x = oMoveInput.x + aMoveInput * -oMoveInput.x / ((aMoveInput + 1));
            moveInput.y = oMoveInput.y - aMoveInput * oMoveInput.y / ((aMoveInput + 1));
        }
        else if (oMoveInput.y < 0 && oMoveInput.x >= 0)
        {
            moveInput.x = oMoveInput.x - aMoveInput * oMoveInput.x / ((aMoveInput + 1));
            moveInput.y = oMoveInput.y + aMoveInput * -oMoveInput.y / ((aMoveInput + 1));
        }
        else
        {
            moveInput.x = oMoveInput.x - aMoveInput * oMoveInput.x / ((aMoveInput + 1));
            moveInput.y = oMoveInput.y - aMoveInput * oMoveInput.y / ((aMoveInput + 1));
        }

        if (oMoveInput.x > minRange || oMoveInput.y > minRange || oMoveInput.x < -minRange || oMoveInput.y < -minRange)
        {
            rb.MovePosition(rb.position + moveInput * speed); 
        }
    }
}
