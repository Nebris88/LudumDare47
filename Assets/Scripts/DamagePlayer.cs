using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamagerType { Spikes, Flames, Void }
public class DamagePlayer : MonoBehaviour
{
    public DamagerType damagerType;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.isTrigger) return;
        PlayerHealth ph = col.transform.parent.GetComponent<PlayerHealth>();
        if (ph == null) return;

        switch (damagerType)
        {
            case DamagerType.Spikes:
                if (ph.damageImmunity <= 0) ph.takeDamage(gameObject);
                break;
            case DamagerType.Flames:
                if (!ph.isFireproof) ph.takeDamage(gameObject, 10);
                break;
            case DamagerType.Void:
                ph.takeDamage(gameObject, 10);
                break;
        }
    }
}
