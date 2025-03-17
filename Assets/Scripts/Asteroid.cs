using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private Vector3 randomSpin;
    private float speed;
    void Start()
    {
        randomSpin = new Vector3(
                                 Random.Range(-359f, 359f), 
                                 Random.Range(-359f, 359f), 
                                 Random.Range(-359f, 359f)
                                 ); //gets a random direction for rotation
        speed = Random.Range(0f, .25f);
    }

    void Update()
    {
        transform.Rotate(randomSpin * Time.deltaTime * speed);
    }
}
/*
 * Random time --> spawn a bunch of asteroids at random distances/locations --> asteroids check distance from player and delete if needed
 */