using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    #region Panels
    [Header("Panels")]
    //Ust
    public CanvasGroup MainMenuPanel;
    public CanvasGroup BeforeGamePanel;
    public CanvasGroup InGamePanel;
    public CanvasGroup EndGamePanel;
    public CanvasGroup MenuPanel;
    //Alt
    public CanvasGroup MainMenu_MenuPanel;
    public CanvasGroup MainMenuLevelPanel;
    public CanvasGroup GameBarPanel;
    public CanvasGroup WinPanel;
    public CanvasGroup LosePanel;
    public CanvasGroup LoadScenePanel;
    public CanvasGroup LoadedScenePanel;
    #endregion

    #region Buttons
    [Header("UI Buttons")]
    //MainMenuPanel
        //MenuPanel
    public Button StartGameButton;
    public Button LevelMenuButton;
    public Button OptionsButton;
    public Button ExitGameButton;
        //LevelMenuPanel
    public Button MenuBackButton;
    //BeforeGamePanel
    public Button TaptoPlayButton;
    //InGamePanel
        //GameBarPanel
    public Button PauseButton;
    //EndGamePanel
        //WinPanel
    public Button WinNextButton;
    public Button WinRestartButton;
    public Button WinExitButton;
        //LosePanel
    public Button LoseRestartButton;
    public Button LoseExitButton;
    //MenuPanel
    public Button MenuResumeButton;
    public Button MenuRestartButton;
    public Button MenuExitButton;
    #endregion

    #region Unity
    public void Awake()
    {
        Instance = this;
        DefaultLayout();
        AddButonListeners();
    }
    private void OnEnable()
    {
        EventManager.OnGameStart.AddListener(GameStart);
        EventManager.OnLevelStart.AddListener(LevelStart);
        EventManager.OnLevelFailed.AddListener(LevelFailed);
        EventManager.OnLevelSuccess.AddListener(LevelSucces);
        EventManager.OnLevelLoaded.AddListener(LevelLoaded);
        //EventManager.OnLevelEnd.AddListener();
        EventManager.OnGameEnd.AddListener(DefaultLayout);
    }
    private void OnDisable()
    {
        EventManager.OnGameStart.RemoveListener(GameStart);
        EventManager.OnLevelStart.RemoveListener(LevelStart);
        EventManager.OnLevelFailed.RemoveListener(LevelFailed);
        EventManager.OnLevelSuccess.RemoveListener(LevelSucces);
        EventManager.OnLevelLoaded.RemoveListener(LevelLoaded);
        //EventManager.OnLevelEnd.RemoveListener();
        EventManager.OnGameEnd.RemoveListener(DefaultLayout);
    }
    private void AddButonListeners()
    {
        LevelMenuButton.onClick.AddListener(LevelMenu);
        //OptionsButton.onClick.AddListener();
        ExitGameButton.onClick.AddListener(ExitGame);
        MenuBackButton.onClick.AddListener(MenuBack);
        PauseButton.onClick.AddListener(PauseTheGame);
        WinNextButton.onClick.AddListener(NextLevel);
        WinRestartButton.onClick.AddListener(RestartLevel);
        WinExitButton.onClick.AddListener(ExitMenu);
        LoseRestartButton.onClick.AddListener(RestartLevel);
        LoseExitButton.onClick.AddListener(ExitMenu);
        MenuResumeButton.onClick.AddListener(ResumeGame);
        MenuRestartButton.onClick.AddListener(RestartLevel);
        MenuExitButton.onClick.AddListener(ExitMenu);
    }
    #endregion

    #region UI Control
    private void DefaultLayout()
    {
        CanvasProp.Show(MainMenuPanel);
        CanvasProp.Hide(MainMenuLevelPanel);
        CanvasProp.Hide(BeforeGamePanel);
        CanvasProp.Hide(InGamePanel);
        CanvasProp.Hide(EndGamePanel);
        CanvasProp.Hide(MenuPanel);
        Time.timeScale = 0;
        ResetData();
    }
    private void DefaultGameInLayout()
    {
        CanvasProp.Hide(MainMenuPanel);
        CanvasProp.Show(BeforeGamePanel);
        CanvasProp.Hide(LoadedScenePanel);
        CanvasProp.Hide(InGamePanel);
        CanvasProp.Hide(EndGamePanel);
        CanvasProp.Hide(MenuPanel);
        Time.timeScale = 0;
        ResetData();
    }
    #endregion

    #region MainMenu
    private void LevelMenu() 
    { 
        CanvasProp.Hide(MainMenu_MenuPanel); 
        CanvasProp.Show(MainMenuLevelPanel); 
    }
    private void MenuBack() 
    { 
        CanvasProp.Hide(MainMenuLevelPanel); 
        CanvasProp.Show(MainMenu_MenuPanel); 
    }
    private void ExitGame() { }
    #endregion

    #region Game
    private void GameStart()
    {
    }
    private void LevelStart()
    {
        Time.timeScale = 1;
    }
    private void LevelFailed()
    {
        Time.timeScale = 0;
        CanvasProp.Hide(InGamePanel);//Bug olmasýn diye. Arka planda týklama engellemek için.
        CanvasProp.Show(LosePanel);
    }
    private void LevelSucces()
    {
        Time.timeScale = 0;
        CanvasProp.Hide(InGamePanel);//Bug olmasýn diye. Arka planda týklama engellemek için.
        CanvasProp.Show(WinPanel);
    }
    #endregion

    #region Pause-Resume
    private void PauseTheGame()
    {
        Time.timeScale = 0;
        CanvasProp.Hide(InGamePanel);//Bug olmasýn diye. Arka planda týklama engellemek için.
        CanvasProp.Show(MenuPanel);
    }
    private void ResumeGame()
    {
        CanvasProp.Hide(MenuPanel);
        CanvasProp.Show(InGamePanel);//Bug olmasýn diye. Arka planda týklama engellemek için.
        Time.timeScale = 1;
    }
    #endregion

    #region Next-Restart-ExitMenu
    private void NextLevel()
    {
        GameManager.Instance.EndLevel();
        DefaultGameInLayout();
        NextScene();
    }
    private void RestartLevel()
    {
        GameManager.Instance.EndLevel();
        DefaultGameInLayout();
        ReloadScene();
    }
    private void ExitMenu()
    {
        GameManager.Instance.EndLevel();
        DefaultGameInLayout();
        ExitScene();
        //DefaultLayout();
    }
    #endregion

    #region Methods
    private void ResetData()
    {   //Reset data iþlemi çok fazla veri olursa Farklý bir manager ile sýfýrlanabilinir. Þimdilik birden fazla veriyi burada kontrollü bir þekilde sýfýrtlayacaðýz.
        var playerData = SaveLoadManager.LoadPDP<PlayerData>(SavedFileNameHolder.PlayerData, new PlayerData());
        ResetValues(playerData);
    }
    private void ResetValues(PlayerData playerData)
    {
        playerData.CoinAmount = 0;
    }
    private void LevelLoaded() 
    {
        CanvasProp.Hide(LoadScenePanel); 
        CanvasProp.Show(LoadedScenePanel); 
    }
    private void NextScene()
    {
        LevelManager.Instance.NextLevel();
    }
    private void ReloadScene()
    { 
        LevelManager.Instance.Restart();
    }
    private void ExitScene()
    { 
        LevelManager.Instance.ExitLevel(); 
    }
    #endregion
}