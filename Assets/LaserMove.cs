using UnityEngine;

public class LaserMove : MonoBehaviour
{
    public int speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
