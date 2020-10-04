using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Gems { Sapphire, Emerald, Ruby, Amythest }

public class MasterManager : MonoBehaviour
{
    public static MasterManager Instance;

    public Dictionary<Gems, bool> gems;
    public Dictionary<Gems, bool> firstTimeGems;

    Action<GameObject> onPlayerSpawned;
    Action<Gems, bool> onGemCollected;

    void Awake()
    {
        if (MasterManager.Instance == null)
        {
            MasterManager.Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //Gem Stuff
        gems = new Dictionary<Gems, bool>();
        firstTimeGems = new Dictionary<Gems, bool>();
        foreach (Gems gem in Enum.GetValues(typeof(Gems)))
        {
            gems.Add(gem, false);
            firstTimeGems.Add(gem, true);
        }
    }

    public void spawnPlayer(GameObject playerObject)
    {
        PlayerMovement pm = playerObject.GetComponent<PlayerMovement>();
        PlayerHealth ph = playerObject.GetComponent<PlayerHealth>();

        pm.canSprint = gems[Gems.Sapphire];
        pm.canClimb = gems[Gems.Emerald];
        pm.canDoubleJump = gems[Gems.Amythest];
        ph.isFireproof = gems[Gems.Ruby];
        resetGems();

        if (onPlayerSpawned != null) onPlayerSpawned(playerObject);
    }

    public void collectGem(Gems gem)
    {
        gems[gem] = true;
        if (onGemCollected != null)
        {
            onGemCollected(gem, firstTimeGems[gem]);
        }
        if (firstTimeGems[gem])
        {
            Debug.Log("This is your first time finding: " + gem);
            firstTimeGems[gem] = false;
        }
    }

    public void resetWorld(bool complete = false)
    {
        onPlayerSpawned = null;
        onGemCollected = null;
        if (complete) resetGems(true);
        SceneManager.LoadScene("Game"); 
    }

    public void resetGems(bool complete = false)
    {
        foreach (Gems gem in Enum.GetValues(typeof(Gems)))
        {
            gems[gem]= false;
            if (complete) firstTimeGems[gem]= true;
        }
    }

    public void registerOnPlayerSpawned(Action<GameObject> callback, bool register = true)
    {
        if (register) onPlayerSpawned += callback;
        else onPlayerSpawned -= callback;
    }

    public void registerGemCollected(Action<Gems, bool> callback, bool register = true)
    {
        if (register) onGemCollected += callback;
        else onGemCollected -= callback;
    }
}
