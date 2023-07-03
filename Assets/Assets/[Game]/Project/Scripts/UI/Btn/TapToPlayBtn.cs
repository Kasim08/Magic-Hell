using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToPlayBtn : Button
{
    protected override void OnEnable()
    {
        base.OnEnable();
        onClick.AddListener(PlayGame);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onClick.RemoveListener(PlayGame);
    }

    private void PlayGame()
    {
        if (GameManager.Instance.IsGameStarted && !GameManager.Instance.IsLevelStarted)
        {
            GameManager.Instance.StartLevel();
        }
    }
}