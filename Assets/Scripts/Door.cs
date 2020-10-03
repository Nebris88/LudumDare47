using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum DoorState { Open, Opening, Closed, Closing }

public class Door : MonoBehaviour
{
    public SpriteRenderer sr;
    public BoxCollider2D bc;
    public float openDuration = 5f;
    public float transDuration = 1.5f;

    public DoorState doorState = DoorState.Closed;
    public float transTime;
    public float openTime;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (doorState == DoorState.Closed && !col.isTrigger && col.GetComponentInParent<PlayerMovement>() != null) trigger();
    }

    void Update()
    {
        switch (doorState)
        {
            case DoorState.Opening:
                if (transTime > 0)
                {
                    transTime -= Time.deltaTime;
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Lerp(0.3f, 1f, transTime / transDuration));
                }
                else
                {
                    doorState = DoorState.Open;
                    openTime = openDuration;
                }
                break;
            case DoorState.Open:
                if (openTime > 0)
                {
                    openTime -= Time.deltaTime;
                }
                else
                {
                    doorState = DoorState.Closing;
                    transTime = transDuration;
                }
                break;
            case DoorState.Closing:
                if (transTime > 0)
                {
                    transTime -= Time.deltaTime;
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Lerp(1f, 0.3f, transTime / transDuration));
                }
                else
                {
                    bc.enabled = true;
                    doorState = DoorState.Closed;
                }
                break;
        }
    }

    public void trigger()
    {
        bc.enabled = false;
        doorState = DoorState.Opening;
        transTime = transDuration;
    }
}
