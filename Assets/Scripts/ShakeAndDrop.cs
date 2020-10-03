using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeAndDrop : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject visual;

    bool triggered = false;
    bool fallTriggered = false;
    float shakeTime = 0f;
    float graceTime = 1f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!triggered && !col.isTrigger && col.GetComponentInParent<PlayerMovement>() != null) trigger();
    }

    void Update()
    {
        if (triggered)
        {
            if (shakeTime > 0)
            {
                shakeTime -= Time.deltaTime;
                visual.transform.localPosition = new Vector2(Mathf.PingPong(Time.time*3, 0.2f)-0.1f, 0);
            }
            else if (!fallTriggered)
            {
                triggerFall();
            }
            else 
            {
                if (graceTime > 0)
                {
                    graceTime -= Time.deltaTime;
                }
                else if (Mathf.Abs(rb.velocity.y) < 0.05f)
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                }
            }
        }
    }

    public void trigger()
    {
        triggered = true;
        shakeTime = 1f;
    }

    public void triggerFall()
    {
        fallTriggered = true;
        visual.transform.localPosition = Vector2.zero;
        rb.gravityScale = 2f;
        rb.constraints -= RigidbodyConstraints2D.FreezePositionY;
    }
}
