using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Managers : MonoBehaviour
{
    private static Managers instance;

    public static UIManager UI { get { return instance.uiManager; } }
    public static GameManager Game { get { return instance.gameManager; } }

    private UIManager uiManager;
    private GameManager gameManager;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        Screen.SetResolution(1920, 1080, false);

        GameObject gameObject = new GameObject("Managers");
        instance = gameObject.AddComponent<Managers>();
        //Managers라는 빈 게임옵젝 만들고 Managers 컴포넌트를 붙임.
        DontDestroyOnLoad(gameObject);

        instance.uiManager = CreateManager<UIManager>(gameObject.transform);
    }

    /// <summary>
    /// Hierarchy창에 Manager만들어주는 함수
    /// </summary>
    private static T CreateManager<T>(Transform parent) where T : Component, IManager
    {
        GameObject gameObject = new GameObject(typeof(T).Name);
        T generic = gameObject.AddComponent<T>();
        gameObject.transform.parent = parent;

        generic.Init();

        return generic;
    }
}
