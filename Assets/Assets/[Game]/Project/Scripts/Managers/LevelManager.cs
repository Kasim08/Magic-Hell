using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    private void OnEnable()
    {
        EventManager.OnGameStart.AddListener(StartLevel);
    }

    private void OnDisable()
    {
        EventManager.OnGameStart.RemoveListener(StartLevel);
    }

    private string activeLevel;
    public string ActiveLevel { get { return activeLevel; } private set { activeLevel = value; } }
    public int LevelIndex
    {
        get { return PlayerPrefs.GetInt("CurrentLevel", 1); }
        set { PlayerPrefs.SetInt("CurrentLevel", value); }
    }

    public void StartLevel()
    {
        string lstlvl = PlayerPrefs.GetString("SelectedLevel", "Level01");
        ActiveLevel = PlayerPrefs.GetString("LastLevel", lstlvl);
        StartCoroutine(LoadSceneCo(ActiveLevel));
    }
    public void Restart()
    {
        AsyncOperation op = SceneManager.UnloadSceneAsync(ActiveLevel);
        op.completed += (AsyncOperation result) =>
        { StartCoroutine(LoadSceneCo(ActiveLevel)); };
    }
    public void NextLevel() { }
    public void ExitLevel()
    {
        if (ActiveLevel.Contains("Level"))
        {
            StartCoroutine(UnloadSceneCo(ActiveLevel));         
        }
    }
    private IEnumerator LoadSceneCo(string value)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(value, LoadSceneMode.Additive);
        // wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
            yield return null;
        
        EventManager.OnLevelLoaded.Invoke();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(value));
    }
    private IEnumerator UnloadSceneCo(string value)
    {
        AsyncOperation asyncunLoad = SceneManager.UnloadSceneAsync(value);
        // wait until the asynchronous scene fully unloads
        while (!asyncunLoad.isDone)
            yield return null;

        GameManager.Instance.EndGame();
    }
}
