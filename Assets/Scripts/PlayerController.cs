using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO:
 * 1.) Projectiles
 * 2.) Sound
 * 3.) Enemies/Stuff to shoot
 * 4.) Moving asteroids in random direction
 * 5.) Spawn asteroids randomly
 * 6.) Add hyperspace
 * 7.) Fleet carriers
 * 8.) Cockpit for ship and fleet carrier
 * 9.) More ships to choose from
 * 10.) Stars
 * 11.) Stations
 * 12.) Upgrades for ship
 * 13.) Exit ship and control human player
 * 14.) SRV (rover vehicle thingamajig)
 
  
 */

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 10000f, forwardControl, verticalControl, horizontalControl, mouseSensitivity = 2.0f;
    public Vector2 mouseChange, mouseDirection, shipDirection;
    public AudioSource engineSoundSource;
    public AudioClip engineSoundClip;

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
        rb.AddRelativeTorque(-mouseUp, mouseSide, 0, ForceMode.VelocityChange);

        engineSoundSource.volume = Mathf.Clamp( (Mathf.Abs(forwardControl) + 
                                    Mathf.Abs(verticalControl) +
                                    Mathf.Abs(horizontalControl)
                                    ), 0f, 1f);

        /*if (forwardControl != 0 || verticalControl != 0 || horizontalControl != 0)
        {
            engineSoundSource.PlayOneShot(engineSoundClip);
        }*/

    }

}
