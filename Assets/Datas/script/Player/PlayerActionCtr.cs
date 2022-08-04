using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class PlayerActionCtr : MonoBehaviour
{
    public bool searching;
    GameObject hitObj;

    public void SearchObject()
    {
        if (hitObj != null)
        {
            bool succcesed = ExecuteEvents.Execute<IRecieveMessage>(
                   target: hitObj,
                   eventData: null,
                   functor: (recieveTarget, y) => recieveTarget.OnRecieve());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        hitObj = collision.gameObject;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        hitObj = null;
    }
}
