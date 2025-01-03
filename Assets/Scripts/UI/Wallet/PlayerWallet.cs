using System;

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