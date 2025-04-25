using UnityEngine;
using System.Collections;
/*1.) Enemy will follow player but not hit them
 * 2.) If aggro, will shoot lasers at player if in sight
 * 3.) If hits asteroid, go boom
 * 4.) Need cool vfx explosion
 */
public class EnemyController : MonoBehaviour
{
    private bool inRangeShoot, inRangeMove, canSeePlayer, canShoot = true, isAggro = true;
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

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > minRangeMove )
        {
            rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
            randomFlightDir = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0f); //generates a random strafe direction for the enemy if it is within minimum range
        }
        else
        {
            rb.AddForce(randomFlightDir * 0.3f * speed * Time.deltaTime, ForceMode.VelocityChange);
        }
        
        dirToPlayer = player.transform.position - transform.position;
        //Debug.DrawRay(transform.position, dirToPlayer, Color.red);   //Draws line to player
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, dirToPlayer, rotationRate * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    IEnumerator ImmaFirinMahLazor()
    {
        yield return new WaitForSeconds(fireRate);

        GameObject laser1, laser2;

        laser1 = Instantiate(enemyLaser, leftLaserSpawnPoint.transform.position, leftLaserSpawnPoint.transform.rotation);
        laser1.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + leftLaserSpawnPoint.transform.forward * laserSpeed;

        laser2 = Instantiate(enemyLaser, rightLaserSpawnPoint.transform.position, rightLaserSpawnPoint.transform.rotation);
        laser2.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + rightLaserSpawnPoint.transform.forward * laserSpeed;

        laserSoundSource.PlayOneShot(laserSoundClip);

        StartCoroutine(ImmaFirinMahLazor());
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
