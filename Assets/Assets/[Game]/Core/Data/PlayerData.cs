using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    [SerializeField]
    private int coinAmount;
    public int CoinAmount { get { return coinAmount; } set { coinAmount = value; EventManager.OnPlayerDataUpdated.Invoke(this); } }

    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; EventManager.OnPlayerDataUpdated.Invoke(this); } }
}

public class TurretData
{

}