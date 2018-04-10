using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public void Start()
    {
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }
}