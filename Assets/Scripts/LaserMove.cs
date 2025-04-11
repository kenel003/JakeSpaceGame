using UnityEngine;

public class LaserMove : MonoBehaviour
{
    private float despawnTime = .25f, timer = 0f;
    
    
    void Update()
    {
        //Destroys laser after 0.25 seconds
        timer += Time.deltaTime;
        if(timer > despawnTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if asteroid, make asteroid go boom
        if (other.CompareTag("Asteroid"))
        {
            other.GetComponent<Fracture>().FractureObject();
            Destroy(gameObject); //this destroys the laser
        }
        
    }

}
