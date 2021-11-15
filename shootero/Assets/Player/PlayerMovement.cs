using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed;
    [HideInInspector]
    public float speedModifier;
    public float minRange;

    private Vector2 oMoveInput; //original
    [HideInInspector]
    public Vector2 moveInput; //
    private float aMoveInput; //x + y - 1
    private Vector2 fMoveInput; //final
    
    private Joystick js;

    [Space]
    public GameObject RightThruster;
    public GameObject LeftThruster;

    [Space]
    public float pHealth;
    public float pMaxHealth;
    private float pLastHealth;
    public Text healthText; 
    public Image pHSlider;
    
    [Space]
    public int Damage;

    void Start()
    {
        healthText.text = (pHealth.ToString());
        pLastHealth = pHealth;
        pHSlider.fillAmount = pHSlider.fillAmount * pMaxHealth;

        js = FindObjectOfType<FixedJoystick>();
        rb = this.GetComponent<Rigidbody2D>();

        LeftThruster.SetActive(false);
        RightThruster.SetActive(false);
    }

    void Update()
    {
        if(Damage != 0)
        {
            pHealth -= Damage;
            Damage = 0;
        }
        
        if (pLastHealth != pHealth)
        {
            pLastHealth = pHSlider.fillAmount * pMaxHealth;
            healthText.text = (pHealth.ToString());

            if (pHealth / pMaxHealth < pHSlider.fillAmount)
            {
                pHSlider.fillAmount -= 0.01f;
            }
            
            if (pHealth / pMaxHealth > pHSlider.fillAmount)
            {
                pHSlider.fillAmount += 0.01f;
            } 
        }
        
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
            if(moveInput.x > 0)
            {
                fMoveInput.x = moveInput.x * oMoveInput.x * speed;
            }
            else
            {
                fMoveInput.x = moveInput.x * -oMoveInput.x * speed;
            }

            if (moveInput.y > 0)
            {
                fMoveInput.y = moveInput.y * oMoveInput.y * speed;
            }
            else
            {
                fMoveInput.y = moveInput.y * -oMoveInput.y * speed;
            }

            rb.MovePosition(rb.position + fMoveInput * speedModifier); 
        }

        if (oMoveInput.x > minRange)
        {
            LeftThruster.SetActive(true);
        }
        else
        {
            LeftThruster.SetActive(false);
        }

        if (oMoveInput.x < -minRange)
        {
            RightThruster.SetActive(true);
        }
        else
        {
            RightThruster.SetActive(false);
        }

        if (oMoveInput.x < -minRange)
        {
            RightThruster.SetActive(true);
        }
        else
        {
            RightThruster.SetActive(false);
        }
    }
}
