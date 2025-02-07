using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10000f, forwardControl, verticalControl, horizontalControl;
    public Vector2 mouseChange, mouseDirection, shipDirection;
    
    // Update is called once per frame
    void Update()
    {
        forwardControl = Input.GetAxis("Forward");
        verticalControl = Input.GetAxis("Vertical");
        horizontalControl = Input.GetAxis("Horizontal");
        rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime * forwardControl, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.up * speed * Time.deltaTime * verticalControl, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.right * speed * Time.deltaTime * horizontalControl, ForceMode.VelocityChange);
        mouseChange = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mouseDirection += mouseChange; 
    }

}
