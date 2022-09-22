using System;
using Interfaces;
using UnityEngine;

namespace PlayerClasses
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovable : MonoBehaviour, IMovable
    {
        private const string HORIZONTAL_AXIS = "Horizontal";

        private static readonly Quaternion quaternionUpOneHundredEighty = new(0, 1, 0, 0);
        private static readonly Quaternion quaternionZero = new(0, 0, 0, 1);

        [SerializeField] private float speed;
        [SerializeField] private float stopSpeed;
        [SerializeField] private float maxHorizontalSpeed;
        [SerializeField] private Rigidbody2D playerRigidbody;
        [SerializeField] private Transform _playerToRotate;

        private IAnimated _animated;
        private IFightable _fightable;
        private int _horizontalFactor;
        private float _horizontalStopSpeed;
        private IJumpable _jumpable;
        private float _t;


        private static void KeepHorizontalSpeedLimit(Rigidbody2D rbParameter, float horizontalLimit)
        {
            Vector2 KeepHorizontalSpeedLimitInternal()
            {
                var velocity = rbParameter.velocity;
                if (velocity.x > 0)
                    return velocity.x < horizontalLimit ? velocity : new Vector2(horizontalLimit, velocity.y);
                return velocity.x > -horizontalLimit ? velocity : new Vector2(-horizontalLimit, velocity.y);
            }

            rbParameter.velocity = KeepHorizontalSpeedLimitInternal();
        }

        private static void SetPlayerFlip(Transform rotateTransform, float speed)
        {
            // 0.04f is necessary so that when meeting with an obstacle the player does not turn back and forth
            rotateTransform.rotation = speed > 0.04f
                ? quaternionUpOneHundredEighty // Y = 180
                : quaternionZero; // X,Y,Z = 0
        }

        private void StopPlayer(float xVelocity)
        {
            xVelocity = Mathf.Lerp(xVelocity, 0, _t);
            playerRigidbody.velocity = new Vector2(xVelocity, playerRigidbody.velocity.y);

            _t += 0.5f * Time.deltaTime * stopSpeed;

            if (Math.Abs(playerRigidbody.velocity.magnitude) > 0.5f) return;

            if (_animated.GetCurrentAnimation() == AnimationsEnum.Run)
                StartIdleAnimation();
        }

        private void StartIdleAnimation()
        {
            _animated.StartAnimation(AnimationsEnum.Idle);
        }

        private void StartRunAnimation()
        {
            _animated.StartAnimation(AnimationsEnum.Run);
        }

        private void MoveInternal()
        {
            var horizontal = _horizontalFactor * speed * Time.deltaTime;

            playerRigidbody.AddForce(new Vector2(horizontal, 0));

            KeepHorizontalSpeedLimit(playerRigidbody, maxHorizontalSpeed);
            SetPlayerFlip(_playerToRotate, playerRigidbody.velocity.x);
            StartRunAnimation();
        }

        private void Move()
        {
            // 1 if axis > 0, 0 if axis == 0, -1 if axis < 0
            _horizontalFactor = Math.Sign(Input.GetAxis(HORIZONTAL_AXIS));

            if (_horizontalFactor == 0 || _fightable.IsFighting())
            {
                if (_horizontalStopSpeed == 0)
                {
                    _t = 0;
                    _horizontalStopSpeed = playerRigidbody.velocity.x;
                }

                StopPlayer(_horizontalStopSpeed);
                return;
            }

            _horizontalStopSpeed = 0;
            MoveInternal();
        }


        #region interface implementation

        public void Updating()
        {
            if (!_jumpable.IsGrounded()) return;

            Move();
        }

        public float HorizontalMovement()
        {
            return _horizontalFactor;
        }

        public void Init(IAnimated animated, IJumpable jumpable, IFightable fightable)
        {
            _animated = animated;
            _jumpable = jumpable;
            _fightable = fightable;
        }

        #endregion
    }
}