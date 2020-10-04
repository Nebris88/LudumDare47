using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public Image blackScreen;

    public GameObject playerObject;

    bool spawned = false;
    float spawnTime = 1f;

    bool debug = false;

    void Update()
    {
        if (!spawned)
        {
            if (spawnTime > 0)
            {
                spawnTime -= Time.deltaTime;
                blackScreen.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 1f, spawnTime / 3f));
            }
            else
            {
                spawnPlayer();
            }
        }
    }

    private void spawnPlayer()
    {
        playerObject = Instantiate(playerPrefab);
        playerObject.transform.position = transform.position;
        MasterManager.Instance.spawnPlayer(playerObject);
        spawned = true;

        if (debug)
        {
            PlayerMovement pm = playerObject.GetComponent<PlayerMovement>();
            PlayerHealth ph = playerObject.GetComponent<PlayerHealth>();

            pm.canSprint = true;
            pm.canClimb = true;
            pm.canDoubleJump = true;
            ph.isFireproof = true;
        }
    }
}
