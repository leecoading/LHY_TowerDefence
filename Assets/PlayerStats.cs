using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    [SerializeField] private int StartMoney = 400;

    public static int Lives;
    public int startLives = 20;

    private void Awake()
    {
        Money = StartMoney;
        Lives = startLives;
    }
}
