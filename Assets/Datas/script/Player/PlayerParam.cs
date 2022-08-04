using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParam : MonoBehaviour
{

    public float hp,maxhp;
    public bool immortal;
    [SerializeField] float deadtime;
    [SerializeField] Slider hpbar;
    PlayerAnmCtr pac;
    PlayerAnmWthoutAimCtr pac2;
    EffectCtr effect;

    void ChangeHPval(float val)
    {
        hp += val;
        if (hp <= 0)
        {
            effect.Dead();
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<Collider2D>().isTrigger = true;
            Destroy(gameObject, deadtime);
        }
        hpbar.value = hp / maxhp;
    }
    IEnumerator SetImmortal(float immtime)
    {
        if (!immortal)
        {
            immortal = true;
            yield return new WaitForSeconds(immtime);
            immortal = false;
        }
        else
        {
            yield break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ChangeHPval(maxhp);
        effect = GetComponent<EffectCtr>();
        pac = GetComponent<PlayerAnmCtr>();
        pac2 = GetComponent<PlayerAnmWthoutAimCtr>();
        hpbar.maxValue = maxhp;
    }

    public void Damage(float damage)
    {
        if (!immortal)
        {
            ChangeHPval(-damage);
            if (pac != null)
            {
                pac.DamageTrigger();
            }
            if (pac2 != null)
            {
                pac2.Damage();
            }
            
        }
    }

    public void SetIm(float imt)
    {
        Debug.Log("enter imm");
        StartCoroutine(SetImmortal(imt));
    }
}
