using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamagerType { Flames, Spikes }
public class DamagePlayer : MonoBehaviour
{
    public DamagerType damagerType;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Damager Collided with: " + col.gameObject.name);

        if (col.isTrigger) return;
        PlayerHealth ph = col.transform.parent.GetComponent<PlayerHealth>();
        Debug.Log(ph);
        if (ph == null) return;

        switch (damagerType)
        {
            case DamagerType.Flames:
                if (!ph.isFireproof) ph.takeDamage(col.gameObject, 10);
                break;
            case DamagerType.Spikes:
                if (ph.damageImmunity <= 0) ph.takeDamage(col.gameObject);
                break;
        }
    }
}
