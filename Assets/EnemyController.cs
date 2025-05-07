using System.Collections;
using UnityEngine;
/*1.) Enemy will follow player but not hit them
 * 2.) If aggro, will shoot lasers at player if in sight
 * 3.) If hits asteroid, go boom
 * 4.) Need cool vfx explosion
 */
public class EnemyController : MonoBehaviour
{
    private bool inRangeShoot, inRangeMove, canSeePlayer, canShoot = true, isAggro = false;
    [SerializeField]
    private float maxRangeShoot, minRangeMove = 25f, currentRangeToPlayer, fireRate, speed = 100f, rotationRate = 10f, laserSpeed = 150f;
    private Vector3 dirToPlayer, randomFlightDir;
    private GameObject player, enemyLaser;
    private Rigidbody rb;
    [SerializeField]
    private GameObject rightLaserSpawnPoint, leftLaserSpawnPoint;
    [SerializeField]
    private AudioSource engineSoundSource, laserSoundSource;
    private AudioClip laserSoundClip;
    [SerializeField]
    private ParticleSystem splodeyBits;
    void Start()
    {
        randomFlightDir = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0f);
        fireRate = Random.Range(0.15f, 1f);
        enemyLaser = Resources.Load<GameObject>("EnemyLaser");
        laserSoundClip = Resources.Load<AudioClip>("Audio/laser");
        player = GameObject.Find("Player Ship");
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ImmaFirinMahLazor());
    }

    
    void Update()
    {
        currentRangeToPlayer = Vector3.Distance(transform.position, player.transform.position);
        dirToPlayer = player.transform.position - transform.position;

        if (currentRangeToPlayer > minRangeMove ) //Moves the enemy TOWARD the player when outside of minimum range
        {
            rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
            randomFlightDir = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0f); //generates a random strafe direction for the enemy if it is within minimum range
        }
        else //moves enemy 30% of full speed in random direction in order to strafe around the player
        {
            rb.AddForce(randomFlightDir * 0.3f * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        
        //Decides if player is in range and turns aggro on or off
        if(currentRangeToPlayer <= maxRangeShoot)
        {
            isAggro = true;
        }
        else
        {
            isAggro = false;
        }

        //???????????
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, dirToPlayer, rotationRate * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
    
    IEnumerator ImmaFirinMahLazor() //very mature, much adult joke
    {
        yield return new WaitForSeconds(fireRate); //waits before doing anything 

        if (isAggro) //checks whether we are in range to shoot based on the range-check in Update()
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("PlayerLaser"))
        {
            splodeyBits.Play();
            Debug.Log("HIT!");
        }
    }
}
