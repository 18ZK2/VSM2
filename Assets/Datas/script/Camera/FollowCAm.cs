using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCAm : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerCtr>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
