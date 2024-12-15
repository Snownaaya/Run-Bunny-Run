using UnityEngine;
using System;
using Zenject;

public class PlayerWallet
{
    public int Coin { get; private set; }

    public event Action CoinChanges;

    public void AddCoin()
    {
        Coin += 1;
        CoinChanges?.Invoke();
    }
}