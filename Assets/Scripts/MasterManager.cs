using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManager : MonoBehaviour
{
    public static MasterManager Instance;

    void Awake()
    {
        if (MasterManager.Instance == null)
        {
            MasterManager.Instance = this;
        }
    }
}
