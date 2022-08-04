using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultArm : MonoBehaviour
{
    [SerializeField] float maxspeed = 10;
    Animator anm;
    Rigidbody2D rb;
    PlayerCtr pcr;
    // Start is called before the first frame update
    void Start()
    {
        anm = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();
        pcr = GetComponentInParent<PlayerCtr>();
    }

    // Update is called once per frame
    void Update()
    {
        anm.SetBool("wallRun", pcr.isWallRunning);
    }

    private void FixedUpdate()
    {
        anm.SetFloat("speed", Mathf.Abs(rb.velocity.x) / maxspeed);
        anm.SetFloat("yspeed", rb.velocity.y);
    }

    public void Damage()
    {
        anm.SetTrigger("damage");
    }

}
