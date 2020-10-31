using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DeathWall : MonoBehaviour
{
    public GameObject player;
    public Transform teleportTarget;
    public GameObject help;
    public GameObject obstacle;
    public Transform obstaclePosition;

    void Start()
    {

        Assert.IsNotNull(GetComponent<Collider>());
        GetComponent<Checkpoint>();
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            Debug.Log("Collision enter");
            player.transform.position = teleportTarget.position;
            help.SetActive(false);
            obstacle.transform.position = obstaclePosition.position;
        }
    }
}

