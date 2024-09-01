using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody rig;

    void Awake()
    {
        // get the rigidbody component attatched to this object
        rig = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
}
