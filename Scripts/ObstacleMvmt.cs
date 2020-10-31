using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMvmt : MonoBehaviour
{

    public Vector3 force;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity += force;
    }
}
