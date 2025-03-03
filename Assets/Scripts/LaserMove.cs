using UnityEngine;

public class LaserMove : MonoBehaviour
{
    public int speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if asteroid, make asteroid go boom
        if (other.CompareTag("Asteroid"))
        {
            other.GetComponent<Fracture>().FractureObject();
            Destroy(gameObject); //this is the laser
            Debug.Log("HIT!");
        }
        
    }

}
