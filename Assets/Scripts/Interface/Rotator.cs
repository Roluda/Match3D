﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    GameObject targetObject;
    [SerializeField, Range(0, 1000)]
    float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) {
            Debug.Log("rotation");
            float rotX = Input.GetAxis("Mouse X") * rotationSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotationSpeed * Mathf.Deg2Rad;

            targetObject.transform.Rotate(Vector3.up, -rotX, Space.World);
            targetObject.transform.Rotate(Vector3.right, rotY, Space.World);
        }
    }
}
