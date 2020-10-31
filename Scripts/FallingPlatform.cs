using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Assertions;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 startPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Assert.IsNotNull(rb);
        rb.isKinematic = true;
        rb.useGravity = false;
        startPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(DelayFallTimer());
            StartCoroutine(DelayRespawn());
        }
    }

    IEnumerator DelayFallTimer()
    {
        yield return new WaitForSeconds(1);
        Fall();
    }


    void Fall()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    void Respawn()
    {
        transform.position = startPosition;
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    IEnumerator DelayRespawn()
    {
        yield return new WaitForSeconds(5);
        Respawn();
    }

}

