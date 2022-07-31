using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRB;


    public float horizontalSpeed = 5.0f;
    public float horizontalInput;
    private float jumpForce = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * horizontalSpeed);

        if (Input.GetKeyDown(KeyCode.W))
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("W");
        }


    }
}
