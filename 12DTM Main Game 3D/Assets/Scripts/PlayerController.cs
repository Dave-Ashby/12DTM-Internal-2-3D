using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Shoot Projectile script
    FlyForwards flyForwards;

    // Rigidbody
    private Rigidbody playerRB;

    // Movement floats
    public float horizontalSpeed = 5.0f;
    public float horizontalInput;
    private float jumpForce = 8.0f;

    // Combat floats
    public float spikeOffset;
    public float health = 3.0f;

    // Booleans
    public bool isTouchingGround;
    public bool isFacingForward;
    public bool isCrouching;

    // Projectile
    public GameObject spike;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = gameObject.GetComponent<Rigidbody>();
        flyForwards = spike.GetComponent<FlyForwards>();
        isFacingForward = true;
    }
    // Update is called once per frame
    void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime  * horizontalSpeed, Space.World);

        if (horizontalInput > 0)
        {
            gameObject.transform.localScale = new Vector3(25, 25, 25);
            isFacingForward = true;

        }
        if (horizontalInput < 0)
        {
            gameObject.transform.localScale = new Vector3(-25, 25, 25);
            isFacingForward = false;
        }

        if (Input.GetKeyDown(KeyCode.W) && isTouchingGround == true)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isCrouching == false)
        {
            Instantiate(spike, new Vector3(transform.position.x + spikeOffset, transform.position.y + 1f, transform.position.z), spike.transform.rotation);
        }

        if (isFacingForward == true)
        {
            spikeOffset = 0.75f;
            flyForwards.FlyRight();
        }
        if (isFacingForward == false)
        {
            spikeOffset = -0.75f;
            flyForwards.FlyLeft();
        }

    }

    // Used for movement
    public void TouchingGround()
    {
        isTouchingGround = true;
    }
    public void NotTouchingGround()
    {
        isTouchingGround = false;
    }

    // Used for movement
    public void Crouching()
    {
        if (isTouchingGround == true)
        {
            horizontalSpeed = 2.0f;
        }
        jumpForce = 4.0f;
        isCrouching = true;
    }
    public void NotCrouching()
    {
        horizontalSpeed = 5.0f;
        jumpForce = 8.0f;
        isCrouching = false;
    }

    // Health system
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health == 0)
        {
            //Insert Death
            Debug.Log("You Lose");
        }
    }

    //Collisions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
        }
        if (collision.gameObject.CompareTag("Water"))
        {
            //Insert Death
            Debug.Log("You Lose");
        }

    }

}
