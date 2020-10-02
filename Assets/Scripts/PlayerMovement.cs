using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    Vector3 initialScale;

    void Start()
    {
        initialScale = this.transform.localScale;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector3(-1f,0f,0f), ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector3(1f, 0f, 0f), ForceMode.Acceleration);
        } 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(new Vector3(0f, 11.5f, 0f), ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.localScale = new Vector3(0.85f,0.5f,1f);
        } 
        else
        {
            this.transform.localScale = initialScale;
        }
    }
}
