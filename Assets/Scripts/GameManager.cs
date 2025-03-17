using UnityEngine;
using System.Collections;


public class GameController : MonoBehaviour
{
    public GameObject[] asteroids;
    public GameObject player;
    public int minAsteroids = 100, maxAsteroids = 500, minSpawnTime = 30, maxSpawnTime = 60,
        minAsteroidFieldSize = 50, maxAsteroidFieldSize = 100;
    private int randomNumOfAsteroids, randomAsteroid, randSpawnTime;
    private Vector3 randomSpawnLocation, randomLocationOffset;
    private float randX, randY, randZ;
    void Start()
    {
        //for loop to spawn in random number of starter asteroids
        randomNumOfAsteroids = Random.Range(minAsteroids, maxAsteroids + 1);
        randSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        for (int i = 0; i < randomNumOfAsteroids; i++)
        {
            randomAsteroid = Random.Range(0, asteroids.Length); //pick a random asteroid to spawn
            randX = player.transform.position.x + Random.Range(-500f, 500f);
            randY = player.transform.position.y + Random.Range(-500f, 500f);
            randZ = player.transform.position.z + Random.Range(-500f, 500f);
            randomSpawnLocation = new Vector3(randX, randY, randZ);
            Instantiate(asteroids[randomAsteroid], randomSpawnLocation, transform.rotation);
        }
        StartCoroutine(SpawnAsteroidField());
    }
    
    IEnumerator SpawnAsteroidField()
    {
        yield return new WaitForSeconds(randSpawnTime);
        int randomNumOfAsteroidField = Random.Range(minAsteroidFieldSize, maxAsteroidFieldSize);
        //Find random point in space ahead of player
        randX = player.transform.position.x + Random.Range(-200f, 200f);
        randY = player.transform.position.y + Random.Range(-200f, 200f);
        randZ = player.transform.position.z + Random.Range(100f, 200f);
        randomSpawnLocation = new Vector3(randX, randY, randZ);

        for (int i = 0; i < randomNumOfAsteroidField; i++)
        {
            randomAsteroid = Random.Range(0, asteroids.Length); //pick a random asteroid to spawn
            randX = randomSpawnLocation.x + Random.Range(100f, 200f);
            randY = randomSpawnLocation.y + Random.Range(100f, 200f);
            randZ = randomSpawnLocation.z + Random.Range(100f, 200f);
            randomLocationOffset = new Vector3(randX, randY, randZ);
            Instantiate(asteroids[randomAsteroid], randomLocationOffset, transform.rotation);
        }
        StartCoroutine(SpawnAsteroidField());
    }
}
