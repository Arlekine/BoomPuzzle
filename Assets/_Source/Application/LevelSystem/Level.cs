using System;
using System.Collections.Generic;
using Model.NonMomentalExplosion;
using Model.WinningLoseConditions;
using UnityEngine;

namespace BoomPuzzle.Application.LevelSystem
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private List<ExplodableBody> _explodables = new List<ExplodableBody>();
        [SerializeField] private FinalBlock _finalBlock;
        [SerializeField] private float _checkingTime;

        public event Action Lost;
        public event Action Completed;

        public IEnumerable<IExplosionEventCaster> ExplosionEventCasters => _explodables;

        private void Start()
        {
            _finalBlock.Construct(_checkingTime);
            _finalBlock.StayedOnFinalPlatform += OnFinalBlockStayed;
        }

        private void OnFinalBlockStayed() => print("PASS!");
    }
}