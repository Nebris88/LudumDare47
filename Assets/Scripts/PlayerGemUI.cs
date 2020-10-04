using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGemUI : MonoBehaviour
{
    public GameObject gemPrefab;
    public Sprite sapphireFilled;
    public Sprite sapphireEmpty;
    public Sprite emeraldFilled;
    public Sprite emeraldEmpty;
    public Sprite rubyFilled;
    public Sprite rubyEmpty;
    public Sprite diamondFilled;
    public Sprite diamondEmpty;

    Dictionary<Gems, GameObject> gemObjects = new Dictionary<Gems, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        MasterManager.Instance.registerOnPlayerSpawned(onPlayerSpawned);
        MasterManager.Instance.registerGemCollected(onGemCollected);
    }

    private void onPlayerSpawned(GameObject playerObject)
    {
        foreach (Gems gem in Enum.GetValues(typeof(Gems)))
        {
            GameObject newSocket = Instantiate(gemPrefab);
            newSocket.transform.SetParent(transform);
            newSocket.GetComponent<Image>().sprite = getSprite(gem, true);
            gemObjects.Add(gem, newSocket);
        }
    }

    private void onGemCollected(Gems gem, bool firstTime)
    {
        gemObjects[gem].GetComponent<Image>().sprite = getSprite(gem);
    }

    private Sprite getSprite(Gems gem, bool empty = false)
    {
        switch (gem)
        {
            case Gems.Sapphire:
                return empty ? sapphireEmpty : sapphireFilled;
            case Gems.Emerald:
                return empty ? emeraldEmpty : emeraldFilled;
            case Gems.Ruby:
                return empty ? rubyEmpty : rubyFilled;
            case Gems.Diamond:
                return empty ? diamondEmpty : diamondFilled;
            default:
                return null;
        }
    }
}
