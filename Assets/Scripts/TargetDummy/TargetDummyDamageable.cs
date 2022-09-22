using Interfaces;
using UnityEngine;

namespace TargetDummy
{
    public class TargetDummyDamageable : MonoBehaviour, IDamageable
    {
        private IAnimated _animated;
        
        #region interface implementation
        
        public void GetDamage(int damage)
        {
            // TODO: target dummy never dies, it doesn't need to take damage
            _animated.StartAnimation(AnimationsEnum.Damage);
        }

        public void Init(IAnimated animated)
        {
            _animated = animated;
        }
        
        #endregion
    }
}