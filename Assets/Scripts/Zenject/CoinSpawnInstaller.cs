using UnityEngine;
using Zenject;

namespace Zenject
{
    public class CoinSpawnInstaller : MonoInstaller
    {
        [SerializeField] private CoinSpawner[] _spawner;

        public override void InstallBindings()
        {
            Container.Bind<CoinSpawner[]>().FromInstance(_spawner).AsSingle();
        }
    }
}