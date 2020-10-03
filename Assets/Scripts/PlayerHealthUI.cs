using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public GameObject heartPrefab;
    public Sprite heartFull;
    public Sprite heartEmpty;
    public PlayerHealth ph;

    List<Image> hearts = new List<Image>();

    void Start()
    {
        MasterManager.Instance.registerOnPlayerSpawned(onPlayerSpawned);
    }

    private void onPlayerSpawned(GameObject playerObject)
    {
        ph = playerObject.GetComponent<PlayerHealth>();

        for (int i = 0; i < ph.playerHealth; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab);
            newHeart.transform.SetParent(transform);
            hearts.Add(newHeart.GetComponent<Image>());
        }

        ph.registerOnPlayerDamaged(onPlayerDamaged);
    }

    public void onPlayerDamaged()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].sprite = ph.playerHealth > i ? heartFull : heartEmpty;
        }
    }
}
