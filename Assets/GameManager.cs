using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameEnd = false;
    private void Update()
    {
        if(gameEnd)
            return;

        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnd = true;
        //게임 오버시 나올 팝업 추가.
    }
}
