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
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isCrouching == false)
            {
                transform.Translate(Vector3.down * crouchForce);
                isCrouching = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {

            transform.Translate(Vector3.up * crouchForce);
            isCrouching = false;

        }
    }

}

