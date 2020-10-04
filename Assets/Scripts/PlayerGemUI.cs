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
    public Sprite amythestFilled;
    public Sprite amythestEmpty;

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
        gemObjects[gem].GetComponent<Image>().color = getSpriteColor(gem);
    }

    private Sprite getSprite(Gems gem, bool empty = false)
    {

        return empty ? sapphireEmpty : sapphireFilled;

        /*
        switch (gem)
        {
            case Gems.Sapphire:
                return empty ? sapphireEmpty : sapphireFilled;
            case Gems.Emerald:
                return empty ? emeraldEmpty : emeraldFilled;
            case Gems.Ruby:
                return empty ? rubyEmpty : rubyFilled;
            case Gems.Amythest:
                return empty ? amythestEmpty : amythestFilled;
            default:
                return null;
        }
        */
    }

    private Color getSpriteColor(Gems gem)
    {
        switch (gem)
        {
            case Gems.Sapphire:
                return Color.blue;
            case Gems.Emerald:
                return Color.green;
            case Gems.Ruby:
                return Color.red;
            case Gems.Amythest:
                return Color.magenta;
            default:
                return Color.white;
        }
    }
}
