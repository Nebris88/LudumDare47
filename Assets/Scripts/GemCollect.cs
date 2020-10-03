using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCollect : MonoBehaviour
{
    public Gems gem;

    bool triggered = false;
    float doomTime = 3f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!triggered && !col.isTrigger && col.GetComponentInParent<PlayerMovement>() != null) trigger();
    }

    void Update()
    {
        if (triggered)
        {
            if (doomTime > 0)
            {
                doomTime -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void trigger()
    {
        triggered = true;
        MasterManager.Instance.collectGem(gem);
    }
}
