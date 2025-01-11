using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Title : MonoBehaviour
{
    public void OnClickPlayBtn()
    {
        SceneLoadManager.LoadScene(SceneType.LevelChooseScene);
    }

    public void OnClickCreateLevelBtn()
    {
        SceneLoadManager.LoadScene(SceneType.MapEditScene);
    }

    public void OnClickQuitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
