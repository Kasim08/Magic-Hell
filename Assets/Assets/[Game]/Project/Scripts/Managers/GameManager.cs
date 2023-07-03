using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameData
{
    // Gibi oyun içi bazý veriler tutulunabilir. Ve gamedata olarak eriþilebilir.
    public int PointMultiplier;
    public float MultiplyTimer;
    public float MagnetTimer;
    public bool IsMagnetActive;
}


public class GameManager : Singleton<GameManager>
{
    public GameData GameData = new GameData();

    private bool isGameStarted;
    public bool IsGameStarted { get { return isGameStarted; } private set { isGameStarted = value; } }

    private bool isLevelStarted;
    public bool IsLevelStarted { get { return isLevelStarted; } private set { isLevelStarted = value; } }

    private void Awake()
    {
        IsGameStarted = false;
        IsLevelStarted = false;
    }

    public void StartGame()
    {
        if (IsGameStarted)
            return;

        IsGameStarted = true;
        EventManager.OnGameStart.Invoke();
    }

    public void StartLevel()
    {
        if (IsLevelStarted)
            return;

        IsLevelStarted = true;
        EventManager.OnLevelStart.Invoke();
    }
    public void EndLevel()
    {
        if (!isLevelStarted)
            return;

        IsLevelStarted = false;
        EventManager.OnLevelEnd.Invoke();
    }

    public void EndGame()
    {
        if (!IsGameStarted)
            return;

        isLevelStarted = false;
        isGameStarted = false;
        EventManager.OnGameEnd.Invoke();
    }
}
