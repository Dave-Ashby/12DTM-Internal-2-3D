using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusPotCollisions : MonoBehaviour
{
    //Detect when the cactus pot comes into contact
    void OnTriggerEnter(Collider other)
    {
        //Detect collisions with the ground
        if (other.CompareTag("Ground"))
        {
            //Run a void on the player controller script
            GameObject.Find("Player").GetComponent<PlayerController>().TouchingGround();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            if (other.CompareTag("Ground"))
            {
                GameObject.Find("Player").GetComponent<PlayerController>().NotTouchingGround();
            }
        }
    }

}
