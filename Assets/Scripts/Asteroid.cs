using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    public int checkPlayerDistanceTime = 5;
    public float maxDistanceFromPlayer = 1000f;
    public Transform playerTransform;
    void Start()
    {
        playerTransform = GameObject.Find("Player Ship").transform;
        StartCoroutine(CheckPlayerDistance());
    }

    IEnumerator CheckPlayerDistance()
    {
        yield return new WaitForSeconds(checkPlayerDistanceTime);
        if (Vector3.Distance(playerTransform.position, transform.position) > maxDistanceFromPlayer)
        {
            Destroy(gameObject);
        }
        StartCoroutine(CheckPlayerDistance());
    }
    
}
/*
 * Random time --> spawn a bunch of asteroids at random distances/locations --> asteroids check distance from player and delete if needed
 */