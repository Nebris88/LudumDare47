using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    public bool grounded = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        grounded = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        grounded = false;
    }

    
}
