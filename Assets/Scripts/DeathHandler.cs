using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathHandler : MonoBehaviour
{
    public GameObject cameraObject;
    public GameObject playerObject;
    public GameObject deathRattle;
    public Image blackScreen;
    public Text deathText;

    bool isDead = false;
    bool isFallen = false;
    float doomTime = 1f;
    float graceTime = 1.5f;
    float driftTime = 3f;

    void Start()
    {
        MasterManager.Instance.registerOnPlayerSpawned(onPlayerSpawned);
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (doomTime > 0)
            {
                doomTime -= Time.deltaTime;
            }
            else if (!isFallen)
            {
                isFallen = true;
                deathRattle = new GameObject();
                deathRattle.transform.position = playerObject.transform.position;
                cameraObject.GetComponent<CameraMovement>().target = deathRattle;
            }
            else
            {
                if (graceTime > 0)
                {
                    graceTime -= Time.deltaTime;
                }
                else if (driftTime > 0)
                {
                    driftTime -= Time.deltaTime;
                    Vector3 dirftDir = transform.position - deathRattle.transform.position;
                    dirftDir.Normalize();
                    deathRattle.transform.Translate(dirftDir * Time.deltaTime);
                    blackScreen.color = new Color(0f,0f,0f, Mathf.Lerp(1f, 0f, driftTime / 2.5f));
                    deathText.color = new Color(1f, 0f, 0f, Mathf.Lerp(1f, 0f, driftTime / 2.5f));
                }
                else
                {
                    MasterManager.Instance.resetWorld();
                }
            }
        }
    }

    private void onPlayerSpawned(GameObject playerObject)
    {
        this.playerObject = playerObject;
        playerObject.GetComponent<PlayerHealth>().registerOnPlayerKilled(onPlayerKilled);
    }

    private void onPlayerKilled()
    {
        Debug.Log("RIP");

        PlayerMovement pm = playerObject.GetComponent<PlayerMovement>();
        pm.pc.enabled = false;
        pm.enabled = false;

        Rigidbody2D rb = playerObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);

        isDead = true;
    }
}
