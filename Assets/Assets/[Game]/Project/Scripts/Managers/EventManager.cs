using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent OnBangBangGame = new UnityEvent();
    public static UnityEvent OnGameStart = new UnityEvent();
    public static UnityEvent OnLevelStart = new UnityEvent();
    public static UnityEvent OnLevelSuccess = new UnityEvent();
    public static UnityEvent OnLevelFailed = new UnityEvent();
    public static UnityEvent OnLevelEnd = new UnityEvent();
    public static UnityEvent OnGameEnd = new UnityEvent();


    public static UnityEvent OnLevelContine = new UnityEvent();
    public static UnityEvent OnLevelLoaded = new UnityEvent();
    public static PlayerDataEvent OnPlayerDataUpdated = new PlayerDataEvent();

    public class PlayerDataEvent : UnityEvent<PlayerData> { }
}