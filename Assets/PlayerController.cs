using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10000f, forwardControl, verticalControl, horizontalControl, mouseSensitivity = 2.0f;
    public Vector2 mouseChange, mouseDirection, shipDirection;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        forwardControl = Input.GetAxis("Forward");
        verticalControl = Input.GetAxis("Vertical");
        horizontalControl = Input.GetAxis("Horizontal");
        rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime * forwardControl, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.up * speed * Time.deltaTime * verticalControl, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.right * speed * Time.deltaTime * horizontalControl, ForceMode.VelocityChange);
        //mouseChange = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //mouseDirection += mouseChange; 
        float mouseUp = mouseSensitivity * Input.GetAxis("Mouse Y");
        float mouseSide = mouseSensitivity * Input.GetAxis("Mouse X");
        //transform.Rotate(-mouseUp, mouseSide, 0);
        rb.AddTorque(-mouseUp, mouseSide, 0);


    }

}
