using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityUI : MonoBehaviour
{
    public GameObject abilityPrefab;
    public Sprite sprint;
    public Sprite climb;
    public Sprite fireproof;
    public Sprite doublejump;

    // Start is called before the first frame update
    void Start()
    {
        MasterManager.Instance.registerOnPlayerSpawned(onPlayerSpawned);
    }

    private void onPlayerSpawned(GameObject playerObject)
    {
        PlayerMovement pm = playerObject.GetComponent<PlayerMovement>();
        PlayerHealth ph = playerObject.GetComponent<PlayerHealth>();

        if (pm.canSprint) spawnAbility(sprint);
        if (pm.canClimb) spawnAbility(climb);
        if (ph.isFireproof) spawnAbility(fireproof);
        if (pm.canDoubleJump) spawnAbility(doublejump);
    }

    private void spawnAbility(Sprite sprite)
    {
        GameObject newAbility = Instantiate(abilityPrefab);
        newAbility.transform.SetParent(transform);
        newAbility.GetComponent<Image>().sprite = sprite;
    }
}
