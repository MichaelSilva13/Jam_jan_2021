using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    private Rigidbody rb;

    public float maxVelocity = 3;

    public float rotationSpeed = 3;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float zAxis = Input.GetAxis("Vertical");
        float xAxis = Input.GetAxis("Horizontal");

        ThrustForward(zAxis);
        Rotate(transform, xAxis * rotationSpeed);
        ClampVelocity();
    }


    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float z = Mathf.Clamp(rb.velocity.z, -maxVelocity, maxVelocity);

        rb.velocity = new Vector3(x, rb.velocity.y, z);

    }

    private void ThrustForward(float amount)
    {
        Vector3 force = transform.forward * amount;

        rb.AddForce(force);
    }

    private void Rotate(Transform t, float amount)
    {
        t.Rotate(0, amount, 0);
    }
}
