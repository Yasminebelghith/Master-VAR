using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPlayer : MonoBehaviour
{
    public GameObject player;
    public Transform teleportTarget;

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            Debug.Log("Collision enter");
            player.transform.position = teleportTarget.position;
        }
    }

    
}
