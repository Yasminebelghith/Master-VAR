﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    LineRenderer laserLine;

    // Start is called before the first frame update
    void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.SetWidth(.5f, .5f);
    }

    // Update is called once per frame
    void Update()
    {
        laserLine.SetPosition(0, startPoint.position);
        laserLine.SetPosition(1, endPoint.position);
    }

}
