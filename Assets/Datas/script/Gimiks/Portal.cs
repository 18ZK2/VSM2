using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour,IRecieveMessage
{
    [SerializeField] Transform exitPos;
    [SerializeField] string colTag = "Player";
    [SerializeField] string destination;

    GameObject tpObj;
    public void OnRecieve()
    {
        Debug.Log("entry to " + destination);
        if (tpObj != null)
        {
            tpObj.transform.position = exitPos.position;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == colTag)
        {
            tpObj = col.gameObject;
        }
    }
}
