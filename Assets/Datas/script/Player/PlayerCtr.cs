using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerCtr : MonoBehaviour
{

    public bool isWallRunning;

    [SerializeField] float floatingRayL, stopV, floatingP;
    [SerializeField] float jumpStaminaMax;
    [SerializeField] float jumpStaminaRecoverSpeed,jumpStaminaEat;
    [SerializeField] float jumpStamina;
    [SerializeField] float jumpEps;

    [SerializeField] float slideTh;
    [SerializeField] float slideStamina;
    [SerializeField] float slidePow;
    [SerializeField] [Range(0f, 1f)] float jumpGauge;
    [SerializeField] AnimationCurve jumpCurve;
    [SerializeField] float speed;
    [SerializeField] Slider jumpStambar;
    [SerializeField] float wallJumpSpeed;
    bool canWallJump;
    bool jumpHolding;
    bool crouching = false;
    Vector2 moveVec;
    Rigidbody2D rb;

    Gamepad gp;

    void FloatUP(float coefficient)
    {
        float yspeed = rb.velocity.y;
        float absYS = Mathf.Abs(yspeed);
        rb.AddForce(Vector2.up * absYS * floatingP * coefficient);
    }
    IEnumerator EatJumpStamina()
    {
        Debug.Log("enter eatjumpstamina");
        while (true)
        {
            jumpStamina -= jumpStaminaEat * 0.02f;
            jumpGauge += 0.02f;
            yield return new WaitForSeconds(0.02f);
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        //ゲージは1より大きくなると0 ボタンを押す間ゲージはたまる
        //ゲージがたまる間スタミナは消費される
        //スタミナが0より小さいとジャンプできない
        float dt = Time.deltaTime;
        if (context.started)
        {
            rb.velocity = Vector2.right * rb.velocity;
        }
        else if (context.performed)
        {
            jumpHolding = true;
        }
        else if (jumpGauge > 1f || context.canceled)
        {
            jumpGauge = 0f;
            jumpHolding = false;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            moveVec = context.ReadValue<Vector2>();
        }
        else
        {
            moveVec = Vector2.zero;
        }
    }
    public void Slide(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            crouching = true;
            //スタミナ十分　かつ　一定の速度以上で動いている
            float s = (jumpStamina > slideStamina && Mathf.Abs(rb.velocity.x) > slideTh) ? 1f : 0f;
            if (moveVec.x != 0)
            {
                rb.AddForce((moveVec*Vector2.right) * slidePow * s, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce((transform.right) * slidePow * s, ForceMode2D.Impulse);
            }
            jumpStamina -= slideStamina * s;
        }
        else if (context.canceled)
        {
            crouching = false;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gp = Gamepad.current;
    }
    // Update is called once per frame
    void Update()
    {
        
        float dt = Time.deltaTime;
        if (jumpHolding && jumpStamina > 0f)
        {
            jumpStamina -= jumpStaminaEat * dt;
            jumpGauge += dt;
            jumpStambar.value = jumpStamina / jumpStaminaMax;
        }
        else if (jumpStamina < jumpStaminaMax)
        {
            jumpStamina += jumpStaminaRecoverSpeed * dt;
            jumpStambar.value = jumpStamina / jumpStaminaMax;
        }

    }
    private void FixedUpdate()
    {

        //浮遊
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D hit2Ds = Physics2D.Raycast(origin, Vector2.down, floatingRayL, LayerMask.GetMask("Stage"));
        Debug.DrawRay(origin, Vector2.down * floatingRayL, Color.green);

        //地面に当たった
        if (hit2Ds.collider != null)
        {
            float yspeed = rb.velocity.y;
            float absYS = Mathf.Abs(yspeed);
            if (stopV > yspeed)
            {
                Debug.Log("Player yspeed = stopV");
                Vector2 v = new Vector2(rb.velocity.x, stopV);
                rb.velocity = v;
            }
            if (jumpEps < absYS) FloatUP(1f);
            if (!crouching)
            {
                rb.AddForce(Vector2.up * rb.mass * rb.gravityScale * 9.81f);
            }
        }
       
        //ジャンプ
        float val = jumpCurve.Evaluate(jumpGauge);
        if (jumpStamina > 0f && !isWallRunning)
        {
            rb.AddForce(transform.up * val);
        }

        //壁　駆け上がり
        RaycastHit2D wallHit = Physics2D.Raycast(origin+Vector2.down*0.5f, transform.right, 0.3f, LayerMask.GetMask("Stage"));
        Debug.DrawRay(origin+Vector2.down * 0.5f, transform.right * 0.3f, Color.blue);

        if (wallHit.collider != null)
        {
            if(Mathf.Abs(moveVec.x) < 0.1f)
            {
                isWallRunning = false;
            }
            else if (Mathf.Abs(moveVec.x) > 0.1f && !crouching)
            {
                isWallRunning = true;
                rb.AddForce(wallJumpSpeed * transform.up);
            }
        }
        else
        {
            isWallRunning = false;
        }
        

        //移動
        if (crouching)
        {
            Debug.Log("enter crouch walk");
            rb.AddForce(speed * Vector2.right * moveVec * 0.6f);
        }
        else
        {
            rb.AddForce(speed * Vector2.right * moveVec);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            canWallJump = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Stage")
        {
            canWallJump = false;
        }
    }
}
