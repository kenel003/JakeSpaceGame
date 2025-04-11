using UnityEngine;

public class tester : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int i = 1;
        i -= ++i;
        i += --i;
        Debug.Log(i);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
