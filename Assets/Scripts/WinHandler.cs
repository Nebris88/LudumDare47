using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinHandler : MonoBehaviour
{
    public GameObject cameraObject;
    public GameObject playerObject;
    public GameObject deathRattle;
    public Image blackScreen;
    public Text winText;
    public Button replayButton;
    public Text replayText;

    bool won = false;
    bool isFree = false;
    float graceTime = 1.5f;
    float driftTime = 3f;
    float replayTime = 3f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!won && !col.isTrigger && col.GetComponentInParent<PlayerMovement>() != null) trigger();
    }

    void Start()
    {
        MasterManager.Instance.registerOnPlayerSpawned(onPlayerSpawned);
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        replayButton.onClick.AddListener(delegate { MasterManager.Instance.resetWorld(true); });
    }

    void Update()
    {
        if (won)
        {
            if (graceTime > 0)
            {
                graceTime -= Time.deltaTime;
            }
            else if (!isFree)
            {
                isFree = true;
                deathRattle = new GameObject();
                deathRattle.transform.position = playerObject.transform.position;
                cameraObject.GetComponent<CameraMovement>().target = deathRattle;
            }
            else if (driftTime > 0)
            {
                driftTime -= Time.deltaTime;
                Vector3 dirftDir = transform.position - deathRattle.transform.position;
                dirftDir.Normalize();
                deathRattle.transform.Translate(dirftDir * Time.deltaTime);
                blackScreen.color = new Color(0.5f, 0.8f, 0.9f, Mathf.Lerp(1f, 0f, driftTime / 2.5f));
                winText.color = new Color(0f, 1f, 0f, Mathf.Lerp(1f, 0f, driftTime / 2.5f));
                replayButton.image.color = new Color(0.2f, 0.2f, 0.3f, Mathf.Lerp(1f, 0f, driftTime / 2.5f));
                replayText.color = new Color(0f, 1f, 0f, Mathf.Lerp(1f, 0f, driftTime / 2.5f));
            }
            else if (replayTime > 0)
            {
                replayTime -= Time.deltaTime;
            }
        }
    }

    private void onPlayerSpawned(GameObject playerObject)
    {
        this.playerObject = playerObject;
    }

    private void trigger()
    {
        Debug.Log("You win!");
        won = true;

        PlayerMovement pm = playerObject.GetComponent<PlayerMovement>();
        pm.inControl = false;
        pm.victory = true;

        Rigidbody2D rb = playerObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }
}
