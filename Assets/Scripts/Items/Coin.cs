using UnityEngine;

public class Coin : MonoBehaviour
{
    public int MinCoin { get; private set; } = 10;
    public int MaxCoin { get; private set; } = 20;

    private int _coinBeyond = 10;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.TryGetComponent(out CoinSpawner coin))
            if (transform.transform.position.z < _coinBeyond)
                coin.ReturnCoin(this);
    }
}