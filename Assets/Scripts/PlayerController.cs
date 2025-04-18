using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* TODO:
 * 1.) Projectiles
 * 2.) Sound for explosions, enemy ship with diff lasers?, collisions
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
    private Rigidbody rb;

    [SerializeField]
    private float speed = 10000f, 
        mouseSensitivity = 2.0f, rollControl, rollSensitivity = 100f, boost = 30f, 
        maxBoostSpeed = 300f, minBoostSpeed = 30f, boostAcceleration = 120f, laserSpeed = 150f;
    private float forwardControl, verticalControl, horizontalControl;

    private Vector2 mouseChange, mouseDirection, shipDirection;

    [SerializeField]
    private AudioSource engineSoundSource, laserSoundSource;
    private AudioClip laserSoundClip;
    private GameObject rightLaserSpawnPoint, leftLaserSpawnPoint, laser;
    private Camera playerCam;

    private void Start()
    {
        rightLaserSpawnPoint = GameObject.Find("RightLaserSpawnPoint");
        leftLaserSpawnPoint = GameObject.Find("LeftLaserSpawnPoint");
        rb = GetComponent<Rigidbody>();
        playerCam = GetComponent<Camera>();
        laser = Resources.Load<GameObject>("Laser");
        laserSoundClip = Resources.Load<AudioClip>("Audio/laser"); // will find 'Resources/Audio/laser'
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        
        forwardControl = Input.GetAxis("Forward");
        verticalControl = Input.GetAxis("Vertical");
        horizontalControl = Input.GetAxis("Horizontal");
        rollControl = Input.GetAxis("Roll");
        
        rb.AddRelativeForce(Vector3.forward * boost * Time.deltaTime * forwardControl, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.up * speed * Time.deltaTime * verticalControl, ForceMode.VelocityChange);
        rb.AddRelativeForce(Vector3.right * speed * Time.deltaTime * horizontalControl, ForceMode.VelocityChange);
       
        float mouseUp = mouseSensitivity * Input.GetAxis("Mouse Y");
        float mouseSide = mouseSensitivity * Input.GetAxis("Mouse X");
        
        rb.AddRelativeTorque(-mouseUp, mouseSide, 0, ForceMode.VelocityChange);
        rb.AddRelativeTorque(Vector3.forward * rollControl * rollSensitivity * Time.deltaTime, ForceMode.Acceleration);
        engineSoundSource.volume = Mathf.Clamp( (Mathf.Abs(forwardControl) + 
                                    Mathf.Abs(verticalControl) +
                                    Mathf.Abs(horizontalControl)
                                    ), 0f, 1f);

        if(forwardControl > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            boost = Mathf.Lerp(minBoostSpeed, maxBoostSpeed, Time.deltaTime * boostAcceleration);
            
        }
        else
        {
            boost = Mathf.Lerp(maxBoostSpeed, minBoostSpeed, Time.deltaTime * boostAcceleration);
            
        }

        //FIRE THE LAZORS
        if (Input.GetMouseButtonDown(0)) //create the laser if left mouse is pressed
        {
            GameObject laser1, laser2;
            
            laser1 = Instantiate(laser, leftLaserSpawnPoint.transform.position, leftLaserSpawnPoint.transform.rotation);
            laser1.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + leftLaserSpawnPoint.transform.forward * laserSpeed;

            laser2 = Instantiate(laser, rightLaserSpawnPoint.transform.position, rightLaserSpawnPoint.transform.rotation);
            laser2.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + rightLaserSpawnPoint.transform.forward * laserSpeed;

            laserSoundSource.PlayOneShot(laserSoundClip);
        }

        

    }

}
