using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePanel : Panel
{
    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnLevelStart.AddListener(ShowPanel);
        EventManager.OnLevelEnd.AddListener(HidePanel);
        EventManager.OnGameEnd.AddListener(HidePanel);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnLevelStart.RemoveListener(ShowPanel);
        EventManager.OnLevelEnd.RemoveListener(HidePanel);
        EventManager.OnLevelEnd.RemoveListener(HidePanel);
    }
}
