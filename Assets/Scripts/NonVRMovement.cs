using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    public int moveSpeed = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = Vector3.zero;
        Vector3 r = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            v = moveSpeed * transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            v = -moveSpeed * transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            v = moveSpeed * transform.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            v = -moveSpeed * transform.right;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            r.y = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            r.y = -1;
        }

        if (r == Vector3.zero)
        {
            rb.angularVelocity = r;
        }
        if (v == Vector3.zero)
        {
            rb.velocity = v;
        }
        rb.AddForce(v);
        rb.AddTorque(r);
    }
}
