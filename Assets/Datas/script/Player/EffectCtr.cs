using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectCtr : MonoBehaviour
{
    [SerializeField] ParticleSystem zanzou;
    [SerializeField] GameObject[] bodies;
    [SerializeField] GameObject body;
    PlayerAnmWthoutAimCtr aimCtr;
    ParticleSystemRenderer psr;

    bool ir;
    // Start is called before the first frame update
    void Start()
    {
        aimCtr = GetComponent<PlayerAnmWthoutAimCtr>();
        psr = zanzou.GetComponent<ParticleSystemRenderer>();
    }

    public void Nozanzo()
    {
        psr.enabled = false;
    }

    public void Setzanzo()
    {
        psr.enabled = true;
    }

    public void Dead()
    {
        foreach(GameObject g in bodies)
        {
            g.SetActive(false);
        }
        GameObject b;
        b = Instantiate(body, transform.position, transform.rotation);
        b.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;

    }

    // Update is called once per frame
    void Update()
    {
        if (ir==true)
        {
            var rend = zanzou.main;
            ParticleSystem.MinMaxCurve curve = rend.startRotationY;
            curve.constant = 0f;
            rend.startRotationY = curve;
        }
        else
        {
            var rend = zanzou.main;
            ParticleSystem.MinMaxCurve curve = rend.startRotationY;
            curve.constant = Mathf.PI;
            rend.startRotationY = curve;
        }

        ir = aimCtr.isRight;
    }
}
