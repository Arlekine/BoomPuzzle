using BoomPuzzle.Application;
using BoomPuzzle.Application.LevelSystem;
using Model;
using Model.ClickSystem;
using Model.NonMomentalExplosion;
using UnityEngine;
using Zenject;

namespace Application.Installers
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private ExplosionConfig _explosionConfig;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Level _testLevel;
        [SerializeField] private ExplosionFX _explosionFX;
        [SerializeField] private Transform _fxParent;

        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(_mainCamera);

            Container.BindInterfacesTo<MouseClickInput>().AsSingle();
            Container.BindInterfacesAndSelfTo<ClickingSystem>().AsSingle();

            Container.Bind<ExplosionConfig>().FromInstance(_explosionConfig).AsSingle();
            
            Container.Bind<ExplosionCreator>()
                .AsSingle()
                .OnInstantiated<ExplosionCreator>((context, cr) => cr.SetCasters(_testLevel.ExplosionEventCasters))
                .NonLazy();

            Container.Bind<ExplosionFXCreator>()
                .AsSingle()
                .WithArguments(_explosionFX, _fxParent)
                .OnInstantiated<ExplosionFXCreator>((context, cr) => cr.SetCasters(_testLevel.ExplosionEventCasters))
                .NonLazy();
        }
    }
}