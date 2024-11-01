using UnityEngine;
using System;

public class PlayerWallet
{
    [SerializeField] private CoinSpawner _coinSpawner;

    public int Coin { get; private set; }

    public event Action CoinChanges;

    public void AddCoin()
    {
        Coin += 1;
        CoinChanges?.Invoke();
    }
}