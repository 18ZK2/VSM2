using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{

    [SerializeField] float damage;
    [SerializeField] float immTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.tag == "Player")
        {
            PlayerParam p;
            if(hit.TryGetComponent<PlayerParam>(out p))
            {
                p.Damage(damage);
                p.SetIm(immTime);
            }
        }
    }
}
