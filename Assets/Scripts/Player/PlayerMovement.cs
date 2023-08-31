using CustomInput;
using Teleport.Map;
using UnityEngine;
using Zenject;

namespace Player 
{
    [RequireComponent(typeof(Rigidbody2D),typeof(MapPlayerAnimation))]
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 5f; // move speed
        private Rigidbody2D _rigidbody2D;

        private Vector2 _direction;
        private Vector2 _movement;

        private InputControl _inputControl;
        private MapPlayerAnimation _mapPlayerAnimation;
        
        [Inject]
        private void Construct(InputControl inputControl)
        {
            _inputControl = inputControl;
            _inputControl.MoveDirection += SetDirection;
        }

        private void Awake()
        {
            _mapPlayerAnimation = GetComponent<MapPlayerAnimation>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void FixedUpdate()
        {
            _movement = _direction * speed * Time.fixedDeltaTime;

            _rigidbody2D.MovePosition(_rigidbody2D.position + _movement);

            if (_direction != Vector2.zero)
                _mapPlayerAnimation.Animate(_direction);
            else
                _mapPlayerAnimation.Animate("front");
        }
    }
}
