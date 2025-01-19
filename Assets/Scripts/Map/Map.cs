using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Map : MonoBehaviour
{
    [SerializeField] private static BuildManager buildManager;
    public static BuildManager BuildManager { get { return buildManager; } }

    private void Awake()
    {
        buildManager = GetComponent<BuildManager>();
    }

}
