using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyForwards : MonoBehaviour
{
    public float flightDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(new Vector3(flightDirection, 0, 0) * Time.deltaTime, Space.World);
    }

    public void FlyRight()
    {
        flightDirection = 20f;
    }
    public void FlyLeft()
    {
        flightDirection = -20f;
    }
}
