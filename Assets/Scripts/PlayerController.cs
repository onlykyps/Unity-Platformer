using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private Rigidbody rig;
    private AudioSource audioSource;


    void Awake()
    {
        // get the rigidbody component attatched to this object
        rig = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetButtonDown("Jump"))
        {
            TryJump();
        }
    }

    void Move()
    {
        // getting our inputs from keyboard
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        
        // calculate the direction we need to move in
        Vector3 dir = new Vector3(xInput, 0, zInput) * moveSpeed;
        dir.y = rig.velocity.y;

        // set the rigidbody velocity
        rig.velocity = dir;

        Vector3 facingDir = new Vector3(xInput, 0, zInput);

        // magnitude is the length of a vector
        // when player is stationary facingDir becomes 0,0,0 as Move() is called repeatedly
        // prevents last movement value from changing when player is stationary
        if(facingDir.magnitude > 0)
        {
            transform.forward = facingDir;
        }
    }

    // called when we press the jump button
    void TryJump()
    {
        // create rays pointing down from the player
        Ray ray1 = new Ray(transform.position + new Vector3(0.5f,0,0.5f), Vector3.down);
        Ray ray2 = new Ray(transform.position + new Vector3(-0.5f,0,0.5f), Vector3.down);
        Ray ray3 = new Ray(transform.position + new Vector3(0.5f,0,-0.5f), Vector3.down);
        Ray ray4 = new Ray(transform.position + new Vector3(-0.5f,0,-0.5f), Vector3.down);

        bool cast1 = Physics.Raycast(ray1, 0.7f);
        bool cast2 = Physics.Raycast(ray2, 0.7f);
        bool cast3 = Physics.Raycast(ray3, 0.7f);
        bool cast4 = Physics.Raycast(ray4, 0.7f);


        // shoot the raycast
        if(cast1 || cast2 || cast3 || cast4)
        {
            // add force upwards
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("enemy"))
        {
            GameManager.instance.GameOver();
            
        }
        else if(other.CompareTag("coin"))
        {
            // add score 
            GameManager.instance.AddScore(1);

            // destroy coin
            Destroy(other.gameObject);
            audioSource.Play();
        }
        else if(other.CompareTag("goal"))
        {
            GameManager.instance.LevelEnd();
        }
    }
}
