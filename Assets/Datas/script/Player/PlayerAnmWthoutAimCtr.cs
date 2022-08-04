using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnmWthoutAimCtr : MonoBehaviour
{

    public bool isRight;
    [SerializeField] float maxspeed;
    [SerializeField] DefaultArm arm;
    Rigidbody2D rb;
    Animator anm;
    PlayerParam pp;
    PlayerCtr pcr;


    bool canstandup = true;
    bool trystundup = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anm = GetComponent<Animator>();
        pp = GetComponent<PlayerParam>();
        pcr = GetComponent<PlayerCtr>();
        nowrot = transform.rotation;
        beforerot = nowrot;
    }

    // Update is called once per frame
    void Update()
    {
        float absx = Mathf.Abs(rb.velocity.x);

        anm.SetFloat("speed", absx / maxspeed);
        anm.SetBool("immortal", pp.immortal);

        if (trystundup && canstandup)
        {
            trystundup = false;
            anm.SetBool("crouch", false);
        }
    }

    Quaternion nowrot, beforerot;
    private void FixedUpdate()
    {
        //体の回転制御
        if (rb.velocity.x < -0.1f)
        {
            isRight = false;
            nowrot = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (rb.velocity.x > 0.1f)
        {
            isRight = true;
            nowrot = Quaternion.Euler(0f, 0f, 0f);
        }
        if (nowrot != beforerot)
        {
            transform.rotation = nowrot;
            beforerot = nowrot;
        }

        //しゃがみスタック
        Vector2 origin = new Vector2(transform.position.x, transform.position.y);
        RaycastHit2D[] hitHead = Physics2D.RaycastAll(origin, Vector2.up, 0.4f, LayerMask.GetMask("Stage"));
        Debug.DrawRay(origin, Vector2.up * 0.4f, Color.red);
        if (hitHead.Length != 0)
        {
            canstandup = false;
        }
        else
        {
            canstandup = true;
        }

        //壁走り
        anm.SetBool("wallRun", pcr.isWallRunning);

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anm.SetTrigger("jump");
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anm.SetBool("crouch", true);
            pcr.isWallRunning = false;
        }
        else if ((context.canceled))
        {
            trystundup = true;
            
        }
    }

    public void Damage()
    {
        anm.SetTrigger("damage");
        arm.Damage();
    }

    public void Search()
    {
        anm.SetTrigger("search");
    }

}
