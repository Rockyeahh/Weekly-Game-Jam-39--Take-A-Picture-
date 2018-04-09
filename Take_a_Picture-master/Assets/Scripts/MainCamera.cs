using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
    Transform target;
    Vector3 offset;
    public float ScrollScale = 5f;
    // Use this for initialization
    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - target.position ;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = target.position + offset;


        offset = offset.normalized * Mathf.Max(offset.magnitude - ScrollScale * Input.GetAxis("Mouse ScrollWheel"),1f);


    }
}
