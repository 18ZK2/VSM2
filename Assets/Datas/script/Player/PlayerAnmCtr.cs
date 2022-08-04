using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnmCtr : MonoBehaviour
{
    public bool canaim;
    [SerializeField] GameObject cursor;
    [SerializeField] GameObject rightarm;

    SpriteRenderer cursurSR;
    PlayerWaponCtr wc;

    bool isRight;
    Vector2 aimVec;
    Quaternion armrot;
    Rigidbody2D rb;
    Animator anm;

    PlayerParam pp;

    void RotBothArm(Quaternion q)
    {
        cursor.transform.rotation = q;
        rightarm.transform.rotation = q;
    }
    void FlipBothArm()
    {
        cursurSR.flipY = isRight;
    }
    void FlipWapon(bool f)
    {
        //wc.wapons[wc.waponnum].GetComponent<SpriteRenderer>().flipY = f;
        if (f)
        {
            wc.wapons[wc.waponnum].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            wc.wapons[wc.waponnum].transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
        }
    }

    //ÉAÉjÉÅÅ[ÉVÉáÉìÇ…ÇƒïêäÌÇÃå¸Ç´Çê›íË
    public void FlipWaponNowState(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            FlipWapon(isRight);
        }
    }
    public void Aim(InputAction.CallbackContext context)
    {
        if (!canaim)
        {
            return;
        }
        bool beforeR = isRight;
        //ëÃÇÃîΩì]
        Vector2 v = context.ReadValue<Vector2>();
        if (v != Vector2.zero)
        {
            aimVec = v;
        }
        else
        {
            return;
        }
        if (aimVec.x > 0)
        {
            isRight = true;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (aimVec.x < 0)
        {
            isRight = false;

            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }

        //òrâÒì]
        if (beforeR != isRight)
        {
            FlipBothArm();
            FlipWapon(isRight);
        }
        Quaternion q = Quaternion.FromToRotation(Vector2.right, aimVec);
        RotBothArm(q);

    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anm.SetTrigger("jump");
        }
    }
    public void Search(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            anm.SetTrigger("search");
        }
    }
    public void ResetArm()
    {
        //òrÇÃå≈íË
        if (isRight)
        {
            Quaternion q = Quaternion.Euler(0f, 0f, 0f);
            RotBothArm(q);
        }
        else
        {
            cursurSR.flipY = true;
            FlipWapon(true);
            Quaternion q = Quaternion.Euler(0f, -180f, 0f);
            RotBothArm(q);
        }
    }
    public void DamageTrigger()
    {
        anm.SetTrigger("damage");
        armrot = cursor.transform.rotation;
        ResetArm();
    }
    public void AimArm()
    {
        FlipBothArm();
        FlipWapon(isRight);
        RotBothArm(armrot);
    }
    // Start is called before the first frame update
    void Start()
    {
        isRight = true;
        
        cursurSR = cursor.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anm = GetComponent<Animator>();
        //defaultType = new AnimatorOverrideController(anm.runtimeAnimatorController);
        pp = GetComponent<PlayerParam>();
        wc = GetComponent<PlayerWaponCtr>();
    }

    // Update is called once per frame
    void Update()
    {
        anm.SetBool("immortal", pp.immortal);
    }
    private void FixedUpdate()
    {
        float frontV = rb.velocity.x;
        if (!isRight)
        {
            frontV *= -1f;
        }
        anm.SetFloat("speed", frontV);
    }

    
}
