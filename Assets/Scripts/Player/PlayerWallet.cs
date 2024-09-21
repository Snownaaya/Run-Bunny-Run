 using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] private CoinSpawner _coinSpawner;

    public int Money { get; private set; }

    private void Collected() => 
        Money++;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            Collected();
            _coinSpawner.ReturnObject(coin);
            print("is collected");
        }
    }
}
