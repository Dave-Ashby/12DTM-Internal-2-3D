using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRB;


    public float horizontalSpeed = 5.0f;
    public float horizontalInput;
    private float jumpForce = 5.0f;

    public bool isTouchingGround;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();

    }
    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime  * horizontalSpeed, Space.World);

        if (horizontalInput > 0)
        {
            gameObject.transform.localScale = new Vector3(25, 25, 25);
        }
        if (horizontalInput < 0)
        {
            gameObject.transform.localScale = new Vector3(-25, 25, 25);
        }

            if (Input.GetKeyDown(KeyCode.W) && isTouchingGround == true)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void TouchingGround()
    {
        isTouchingGround = true;
    }
    public void NotTouchingGround()
    {
        isTouchingGround = false;
    }
    public void Crouching()
    {
        horizontalSpeed = 2.0f;
        jumpForce = 3.0f;
    }
    public void NotCrouching()
    {
        horizontalSpeed = 5.0f;
        jumpForce = 5.0f;
    }
}
