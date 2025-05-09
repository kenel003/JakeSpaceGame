using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
/*1.) Enemy will follow player but not hit them
 * 2.) If aggro, will shoot lasers at player if in sight
 * 3.) If hits asteroid, go boom
 * 4.) Need cool vfx explosion
 */
public class EnemyController : MonoBehaviour
{
    private bool inRangeShoot, inRangeMove, canSeePlayer, canShoot = false, isAggro = false;
    [SerializeField]
    private float aggroRange = 150f , maxRangeShoot, minRangeMove = 25f, currentRangeToPlayer, fireRate, casualSpeed = 10f,
        attackSpeed = 25f, rotationRate = 10f, laserSpeed = 150f, minTimeToChangeDirection = 15f,
        maxTimeToChangeDirection = 45f;
    private Vector3 dirToPlayer, randomFlightDir, randomCasualDirection;
    private Quaternion randomLookRotation;
    private GameObject player, enemyLaser;
    private Rigidbody rb;
    [SerializeField]
    private GameObject rightLaserSpawnPoint, leftLaserSpawnPoint;
    [SerializeField]
    private AudioSource engineSoundSource, laserSoundSource;
    private AudioClip laserSoundClip;
    [SerializeField]
    private ParticleSystem splodeyBits;
    private List<Vector3> strafeDirections = new List<Vector3>();

    void Start()
    {
        strafeDirections.Add(Vector3.up);
        strafeDirections.Add(Vector3.down);
        strafeDirections.Add(Vector3.right);
        strafeDirections.Add(Vector3.left);
        strafeDirections.Add(new Vector3(1, 1, 0));
        strafeDirections.Add(new Vector3(1, -1, 0));
        strafeDirections.Add(new Vector3(-1, 1, 0));
        strafeDirections.Add(new Vector3(-1, -1, 0));
        randomCasualDirection = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        randomFlightDir = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0f);
        fireRate = Random.Range(0.15f, 1f);
        enemyLaser = Resources.Load<GameObject>("EnemyLaser");
        laserSoundClip = Resources.Load<AudioClip>("Audio/laser");
        player = GameObject.Find("Player Ship");
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ImmaFirinMahLazor());
        StartCoroutine(ChangeFlightDirection());
    }

    
    void Update()
    {
        currentRangeToPlayer = Vector3.Distance(transform.position, player.transform.position);
        dirToPlayer = player.transform.position - transform.position;

        //MOVEMENT SECTION
        if (currentRangeToPlayer > minRangeMove && isAggro) //Moves the enemy TOWARD the player when outside of minimum range
        {
    
            rb.AddRelativeForce(Vector3.forward * attackSpeed * Time.deltaTime, ForceMode.VelocityChange);
            
            randomFlightDir = strafeDirections[Random.Range(0, strafeDirections.Count)]; //generates a random strafe direction for the enemy if it is within minimum range
        }
        else if (currentRangeToPlayer <= minRangeMove && isAggro)//moves enemy 30% of full speed in random direction in order to strafe around the player
        {
            
            rb.AddRelativeForce(randomFlightDir * 0.3f * attackSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
        else if (!isAggro) //enemy is not aggro, should fly casual
        {
            randomLookRotation = Quaternion.Euler(randomCasualDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, randomLookRotation, .5f * Time.deltaTime);
            rb.AddRelativeForce(Vector3.forward * casualSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }

        if (isAggro)
        {
            //Looks at the player, but keeps enemy's own 'up value
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, dirToPlayer, rotationRate * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection, transform.up);
        }

        if(currentRangeToPlayer <= aggroRange)
        {
            isAggro = true;
        }
        else
        {
            isAggro = false;
        }

        //SHOOTING SECTION
        //Decides if player is in range and turns aggro on or off
        if (currentRangeToPlayer <= maxRangeShoot)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }

        
    }
    
    IEnumerator ImmaFirinMahLazor() //very mature, much adult joke
    {
        yield return new WaitForSeconds(fireRate); //waits before doing anything 

        if (canShoot) //checks whether we are in range to shoot based on the range-check in Update()
        {
            GameObject laser1, laser2; //creates two variables to store our created lasers so that we can force them to move once we instantiate them. 
            laser1 = Instantiate(enemyLaser, leftLaserSpawnPoint.transform.position, leftLaserSpawnPoint.transform.rotation); //creates a laser and places on the left turret
            laser1.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + leftLaserSpawnPoint.transform.forward * laserSpeed; //shoves laser forward at high speed
            laser2 = Instantiate(enemyLaser, rightLaserSpawnPoint.transform.position, rightLaserSpawnPoint.transform.rotation);//creates a laser and places on the right turret
            laser2.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + rightLaserSpawnPoint.transform.forward * laserSpeed; //shoves laser forward at high speed
            laserSoundSource.PlayOneShot(laserSoundClip); //plays the pew pew noise
        }
        StartCoroutine(ImmaFirinMahLazor()); //starts a new co-routine that will restart this whole process, allowing continual enemy shooting as needed
    }

    IEnumerator ChangeFlightDirection()
    {
        yield return new WaitForSeconds(Random.Range(minTimeToChangeDirection, maxTimeToChangeDirection));
        randomCasualDirection = new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        StartCoroutine(ChangeFlightDirection());
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("PlayerLaser"))
        {
            splodeyBits.Play();
            Debug.Log("HIT!");
        }
    }
}
