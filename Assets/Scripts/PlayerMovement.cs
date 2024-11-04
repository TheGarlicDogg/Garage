using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{

    [Header("Assignables")]
    public Transform playerCam;
    public Camera mainCam;
    public Transform orientation;
    private Rigidbody rb;


    [Header("Movement")]
    public float moveSpeed = 50;

    float x, y;

    private float xRotation;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;
    private float desiredX;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void FixedUpdate()
    {
        Movement();
    }

    private void Update()
    {
        MyInput();
        Look();

    }
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        Vector3 rot = playerCam.transform.localRotation.eulerAngles;
        desiredX = rot.y + mouseX;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -89.8f, 89.8f);



        playerCam.transform.localRotation = Quaternion.Euler(xRotation, desiredX, 0);
        orientation.transform.localRotation = Quaternion.Euler(0, desiredX, 0);

    }


    private void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }
    private void Movement()
    {

        rb.AddForce(orientation.transform.forward * y * moveSpeed);
        rb.AddForce(orientation.transform.right * x * moveSpeed);
    }
}
