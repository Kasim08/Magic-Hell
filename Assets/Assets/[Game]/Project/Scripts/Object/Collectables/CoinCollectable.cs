using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : CollectableBase
{
    public override void Collect()
    {
        Use();
    }
    public override void Use()
    {
        var playerData = SaveLoadManager.LoadPDP<PlayerData>(SavedFileNameHolder.PlayerData, new PlayerData());
        playerData.CoinAmount += 1;
        SaveLoadManager.SavePDP(playerData, SavedFileNameHolder.PlayerData);

        Dispose();
    }
}
