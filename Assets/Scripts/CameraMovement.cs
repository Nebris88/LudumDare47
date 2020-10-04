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

    private void Start()
    {
        MasterManager.Instance.registerOnPlayerSpawned(onPlayerSpawned);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target != null)
        {
            this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            //this.transform.position = new Vector3(Mathf.Clamp(target.transform.position.x, xMin, xMax), Mathf.Clamp(target.transform.position.y, yMin, yMax), this.transform.position.z);
        }
    }

    private void onPlayerSpawned(GameObject playerObject)
    {
        target = playerObject;
    }
}
