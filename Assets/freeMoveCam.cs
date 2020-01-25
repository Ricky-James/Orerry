using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class freeMoveCam : MonoBehaviour
{

    private float forward;
    private float strafe;
    private int vertical;
    private Vector3 moveDirection;

    private bool fastSpeed;
    private const float Speed = 200f;
    private const float fastSpeedMultiplier = 2.5f;



    private float mouseX;
    private float mouseY;
    private float sensitivty = 100f;
    private float xRotation = 0f;
    private float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        fastSpeed = false;
    }

    private void OnEnable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        /* Keyboard Code */
        fastSpeed = Input.GetButton("Fast");
        forward = Input.GetAxisRaw("Vertical");
        strafe = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump"))
        {
            vertical = 1;
        }
        else if (Input.GetButton("Crouch"))
        {
            vertical = -1;
        }
        else
        {
            vertical = 0;
        }

        moveDirection = new Vector3(strafe, vertical, forward);
        moveDirection = moveDirection.normalized * Time.deltaTime;

        if (fastSpeed)
        {
            transform.Translate(moveDirection * Speed * fastSpeedMultiplier);
        }
        else
        {
            transform.Translate(moveDirection * Speed);
        }
        /* Keyboard Ends*/

        /* Mouse look starts */

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        mouseX = Input.GetAxis("Mouse X") * sensitivty * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * sensitivty * Time.deltaTime;

        //Camera rotation up/down is X axis (input is Y axis)
        xRotation -= mouseY;
        yRotation += mouseX;

        xRotation = Mathf.Clamp(xRotation, -90, 90);

        transform.localEulerAngles = new Vector3(xRotation, yRotation, 0);


        /* Mouse look ends */

    }

    
}
