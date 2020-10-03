using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerMovement pm;
    public Rigidbody2D rb;
    public int playerHealth = 3;

    public bool isFireproof = false;

    public float damageImmunity = 0f;

    Action onPlayerDamaged;
    Action onPlayerKilled;

    void Update()
    {
        if (!pm.inControl && damageImmunity < 1.5f) pm.inControl = true;
        if (damageImmunity > 0) damageImmunity -= Time.deltaTime;
    }

    public void takeDamage (GameObject damager, int amount = 1)
    {
        Debug.Log("Player Collided with: " + damager.name);
        damageImmunity = 2f;
        pm.inControl = false;
        rb.velocity = Vector2.zero;
        Vector2 bounceForce = (transform.position - damager.transform.position) * 5;
        rb.AddForce(bounceForce, ForceMode2D.Impulse);
        playerHealth -= amount;
        if (onPlayerDamaged != null) onPlayerDamaged();
        if (playerHealth <= 0 && onPlayerKilled != null) onPlayerKilled();
    }

    public void registerOnPlayerDamaged(Action callback, bool register = true)
    {
        if (register) onPlayerDamaged += callback;
        else onPlayerDamaged -= callback;
    }

    public void registerOnPlayerKilled(Action callback, bool register = true)
    {
        if (register) onPlayerKilled += callback;
        else onPlayerKilled -= callback;
    }
}
