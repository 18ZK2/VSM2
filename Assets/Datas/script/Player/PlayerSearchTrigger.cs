using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearchTrigger : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerActionCtr>().gameObject;
        player.GetComponent<PlayerAnmWthoutAimCtr>().Search();
        Destroy(gameObject);

    }
}
