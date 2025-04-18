using UnityEngine;

public class tester : MonoBehaviour
{
    public void Act()
    {
        Debug.Log("run ");
        Eat();
    }
    public void Eat()
    {
        Debug.Log("eat ");
    }
}
public class testerChild : tester
{
    public void Act()
    {
        base.Act();
        Debug.Log("sleep ");
    }
    public void Eat()
    {
        base.Eat();
        Debug.Log("bark "); 
    }
   }
