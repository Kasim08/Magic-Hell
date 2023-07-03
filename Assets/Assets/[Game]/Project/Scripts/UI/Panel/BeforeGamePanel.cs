using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeforeGamePanel : Panel
{
    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnGameStart.AddListener(ShowPanel);
        EventManager.OnLevelStart.AddListener(HidePanel);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnGameStart.RemoveListener(ShowPanel);
        EventManager.OnLevelStart.RemoveListener(HidePanel);
    }
}
