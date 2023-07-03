using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameBtn : Button
{
    protected override void OnEnable()
    {
        base.OnEnable();
        onClick.AddListener(StartGame);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        GameManager.Instance.StartGame();
    }
}
