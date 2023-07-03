using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPanel : MonoBehaviour
{
    public Text CoinText;

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnPlayerDataUpdated.AddListener(UpdateCoinText);
        EventManager.OnLevelStart.AddListener(InitilizePanel);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.OnPlayerDataUpdated.RemoveListener(UpdateCoinText);
        EventManager.OnLevelStart.RemoveListener(InitilizePanel);
    }
    //Saveloadfilenameholder ile ilk deðiþken ismini string cinsinden yolluyoruz. Ve bu ismi PlayerData sýnýfýna veriyoruz. Baþka class ismi ile baþka veriler tutulur.
    private void InitilizePanel()
    {
        var playerData = SaveLoadManager.LoadPDP<PlayerData>(SavedFileNameHolder.PlayerData, new PlayerData());
        UpdateCoinText(playerData);
    }
    private void UpdateCoinText(PlayerData playerData)
    {
        CoinText.text=playerData.CoinAmount.ToString();
    }

}
