using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace TargetDummy
{
    public class TargetDummy : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private IAnimated _targetDummyAnimated;
        private IDamageable _targetDummyDamageable;

        private void Start()
        {
            var animationsNames = new Dictionary<AnimationsEnum, string>
            {
                { AnimationsEnum.Damage, "Damage" },
                { AnimationsEnum.Hub, "Hub" }
            };

            InitVariables();

            _targetDummyAnimated.Init(_animator, animationsNames);
            _targetDummyDamageable.Init(_targetDummyAnimated);
        }

        private void InitVariables()
        {
            _targetDummyAnimated = GetComponent<IAnimated>();
            _targetDummyDamageable = GetComponent<IDamageable>();

            _targetDummyAnimated ??= GetComponentInChildren<IAnimated>();
            _targetDummyDamageable ??= GetComponentInChildren<IDamageable>();
        }
    }
}