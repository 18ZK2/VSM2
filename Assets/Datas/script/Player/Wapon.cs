using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wapon : MonoBehaviour
{
    public bool fireing;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePos;
    [SerializeField] SpriteRenderer[] sps;
    [SerializeField] Animator anm;
    [SerializeField] AnimationClip defanm;
    [SerializeField] bool canAuto,canFinish,canSwitchAnyTime;
    [SerializeField] GameObject[] effects;


    AudioSource ass;
    public void Fire()
    {
        anm.SetTrigger("fire");
    }
    public void AutoFire()
    {
        if (canAuto)
        {
            Debug.Log("auto");
            anm.SetBool("Auto", true);
        }
    }
    public void Finish()
    {
        if (canFinish)
        {
            Debug.Log("finish");
            anm.SetBool("Auto", false);
        }
    }
    public void MakeBullet()
    {
        if (bullet == null)
        {
            return;
        }
        GameObject b = Instantiate(bullet, firePos.position,firePos.rotation);
        b.transform.parent = null;
        Destroy(b, 3f);
    }
    public void Hide()
    {
        foreach(SpriteRenderer s in sps)
        {
            s.enabled = false;
        }
        foreach(GameObject g in effects)
        {
            g.SetActive(false);
        }
        anm.enabled = false;
    }
    public void Appear()
    {
        foreach (SpriteRenderer s in sps)
        {
            s.enabled = true;
        }
        foreach (GameObject g in effects)
        {
            g.SetActive(true);
        }
        anm.enabled = true;
    }
    public bool CheckisAnimation()
    {
        if (canSwitchAnyTime)
        {
            return true;
        }
        AnimatorStateInfo info = anm.GetCurrentAnimatorStateInfo(0);
        Debug.Log(defanm.name);
        //ÉåÉCÉÑÅ[Ç‡Ç¬ÇØÇÈÇ±Ç∆
        int defaultHash = Animator.StringToHash("Base Layer."+defanm.name);
        Debug.Log("def:" + defaultHash.ToString() + " now:" + info.fullPathHash);
        return info.fullPathHash == defaultHash;
    }


    private void Start()
    {
        anm = GetComponent<Animator>();
        sps = GetComponentsInChildren<SpriteRenderer>();
        ass = GetComponent<AudioSource>();
    }
}
