using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWaponCtr : MonoBehaviour
{
    public int waponnum;
    public GameObject[] wapons;
    public List<Wapon> wp;

    void SetWapon(bool b)
    {
        if (b)
        {
            wp[waponnum].Appear();
        }
        else
        {
            wp[waponnum].Hide();
        }
    }
    public void Count(InputAction.CallbackContext context)
    {
        Vector2 v = Vector2.zero;
        if (!wp[waponnum].CheckisAnimation())
        {
            return;
        }
        if (context.started)
        {
            v = context.ReadValue<Vector2>();
        }
        
        //waponsSize‚æ‚èwaponnum‚ª‘å‚«‚­‚È‚Á‚½‚ç0
        //0‚æ‚èwaponnum‚ª¬‚³‚­‚È‚Á‚½‚çwaponsSize-1
        SetWapon(false);
        if (v.y > 0)
        {
            if (waponnum >= wapons.Length - 1)
            {
                waponnum = 0;
            }
            else
            {
                waponnum++;
            }
        }
        else if (v.y < 0)
        {
            if (waponnum <= 0)
            {
                waponnum = wapons.Length - 1;
            }
            else
            {
                waponnum--;
            }
        }
        SetWapon(true);

    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            wp[waponnum].Fire();
        }
        else if (context.performed)
        {
            wp[waponnum].AutoFire();
        }
        else if (context.canceled)
        {
            wp[waponnum].Finish();
        }
    }

    public void DisableNowWeapon()
    {
        SetWapon(false);
    }
    public void EnableNowWeapon()
    {
        SetWapon(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject w in wapons)
        {
            Wapon ws = w.GetComponent<Wapon>();
            wp.Add(ws);
            ws.Hide();

        }
        SetWapon(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
