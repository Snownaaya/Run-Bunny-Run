using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CoinSpawnUnstaller : MonoInstaller
{
    [SerializeField] private List<CoinSpawner> _spawner;
    public override void InstallBindings()
    {
        Container.Bind<CoinSpawner>().FromComponentInHierarchy().AsTransient();
    }
}
