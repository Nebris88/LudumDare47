using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    public float xMin = -10f;
    public float xMax = 10f;
    public float yMin = -10f;
    public float yMax = 10f;

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = new Vector3(Mathf.Clamp(target.transform.position.x, xMin, xMax), Mathf.Clamp(target.transform.position.y, yMin, yMax), this.transform.position.z);
    }
}
