using System;
using UnityEngine;

public class PlayerWallet
{
    private const string PlayerCoinSave = nameof(PlayerCoinSave);

    public PlayerWallet() =>
        Coin = PlayerPrefs.GetInt(PlayerCoinSave, 0);

    public int Coin { get; private set; }

    public event Action CoinChanges;

    public void AddCoin()
    {
        int newValue = Coin += 1;
        PlayerPrefs.SetInt(PlayerCoinSave, Coin);
        CoinChanges?.Invoke();
    }

    public void Reset() =>
        Coin = 0;

    public bool SpendCoins(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount));

        if (Coin >= amount)
        {
            Coin -= amount;
            PlayerPrefs.SetInt(PlayerCoinSave, Coin);
            PlayerPrefs.Save();
            CoinChanges?.Invoke();
            return true;
        }

        return false;
    }
}