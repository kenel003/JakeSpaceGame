using UnityEngine;
using System.Collections;
/*1.) Enemy will follow player but not hit them
 * 2.) If aggro, will shoot lasers at player if in sight
 * 3.) If hits asteroid, go boom
 * 4.) Need cool vfx explosion
 */
public class EnemyController : MonoBehaviour
{
    private bool inRange, canSeePlayer, canShoot = true, isAggro = true;
    [SerializeField]
    private float maxRange, currentRangeToPlayer, fireRate = 1f, speed = 100f, rotationRate = 10f, laserSpeed = 150f;
    private Vector3 dirToPlayer;
    private GameObject player, enemyLaser;
    private Rigidbody rb;
    [SerializeField]
    private GameObject rightLaserSpawnPoint, leftLaserSpawnPoint;
    [SerializeField]
    private AudioSource engineSoundSource, laserSoundSource;
    private AudioClip laserSoundClip;
    void Start()
    {
        enemyLaser = Resources.Load<GameObject>("EnemyLaser");
        laserSoundClip = Resources.Load<AudioClip>("Audio/laser");
        player = GameObject.Find("Player Ship");
        rb = GetComponent<Rigidbody>();
        StartCoroutine(ImmaFirinMahLazor());
    }

    // Update is called once per frame
    void Update()
    {
        dirToPlayer = player.transform.position - transform.position;
        Debug.DrawRay(transform.position, dirToPlayer, Color.red);   //Draws line to player
        rb.AddRelativeForce(Vector3.forward * speed * Time.deltaTime, ForceMode.VelocityChange);
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, dirToPlayer, rotationRate * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    IEnumerator ImmaFirinMahLazor()
    {
        yield return new WaitForSeconds(2f);

        GameObject laser1, laser2;

        laser1 = Instantiate(enemyLaser, leftLaserSpawnPoint.transform.position, leftLaserSpawnPoint.transform.rotation);
        laser1.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + leftLaserSpawnPoint.transform.forward * laserSpeed;

        laser2 = Instantiate(enemyLaser, rightLaserSpawnPoint.transform.position, rightLaserSpawnPoint.transform.rotation);
        laser2.GetComponent<Rigidbody>().linearVelocity = rb.linearVelocity + rightLaserSpawnPoint.transform.forward * laserSpeed;

        laserSoundSource.PlayOneShot(laserSoundClip);

        StartCoroutine(ImmaFirinMahLazor());
    }
}
