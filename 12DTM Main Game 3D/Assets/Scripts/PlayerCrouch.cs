using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    float crouchForce = 0.475f;
    bool isCrouching;

    // Start is called before the first frame update
    void Start()
    {
        isCrouching = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) && isCrouching == false)
        {
            transform.Translate(Vector3.down * crouchForce);
            isCrouching = true;
            GameObject.Find("Player").GetComponent<PlayerController>().Crouching();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            transform.Translate(Vector3.up * crouchForce);
            isCrouching = false;
            GameObject.Find("Player").GetComponent<PlayerController>().NotCrouching();
        }
    }

}

