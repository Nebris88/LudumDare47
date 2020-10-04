using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemWobble : MonoBehaviour
{
    float initialY;
    void Start()
    {
        initialY = transform.position.y - 0.1f;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, initialY + Mathf.PingPong(Time.time/5f, 0.2f), 0);
    }
}
