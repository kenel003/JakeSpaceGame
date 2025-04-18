using UnityEngine;

public class TesterStarter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tester theirVersion = new testerChild();
        theirVersion.Act();
        Debug.Log("________________");
        testerChild myVersion = new testerChild();
        myVersion.Act();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
