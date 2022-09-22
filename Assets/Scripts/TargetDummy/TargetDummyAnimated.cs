using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace TargetDummy
{
    public class TargetDummyAnimated : MonoBehaviour, IAnimated
    {
        private Dictionary<AnimationsEnum, string> _animationsDictionary;
        private Animator _animator;
        private AnimationsEnum? _currentAnimation;


        #region interface implementation

        public AnimationsEnum? GetCurrentAnimation()
        {
            return _currentAnimation;
        }
        

        public void StartAnimation(AnimationsEnum animationType)
        {
            _animator.SetTrigger(_animationsDictionary[animationType]);
            _currentAnimation = animationType;
        }

        public void Init(Animator animator, Dictionary<AnimationsEnum, string> animationsDictionary)
        {
            _animator = animator;
            _animationsDictionary = animationsDictionary;
        }

        #endregion
    }
}