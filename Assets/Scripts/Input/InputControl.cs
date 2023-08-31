
using System;
using UnityEngine;

namespace CustomInput
{
    public class InputControl : MonoBehaviour
    {
        public Action<Vector2> MoveDirection;
        public Action OpenMap;
        [SerializeField] private Joystick _joystick; 
        private Vector2 _direction;
        private bool _allInputBlocked;
        private bool _moveInputBlocked;

        private void Awake()
        {
            _joystick.gameObject.SetActive(Application.isMobilePlatform);
        }

        public void BlockAllInput()
        {
            _allInputBlocked = true;
            if (Application.isMobilePlatform)
                _joystick.gameObject.SetActive(false);
        }

        public void UnlockAllInput()
        {
            _allInputBlocked = false;
            if (Application.isMobilePlatform)
                _joystick.gameObject.SetActive(true);
        }
        public void BlockMoveInput()
        {
            _moveInputBlocked = true;
            if (Application.isMobilePlatform)
                _joystick.gameObject.SetActive(false);
        }

        public void UnlockMoveInput()
        {
            _moveInputBlocked = false;
            if (Application.isMobilePlatform)
                _joystick.gameObject.SetActive(true);
        }

        private void Update()
        {
            if (_allInputBlocked)
            {
                ResetAllInput();
                return;
            }
            CheckMap();
            if (_moveInputBlocked)
            {
                ResetAllInput();
                return;
            }
            CheckMove();
            
        }

        private void ResetAllInput()
        {
            MoveDirection?.Invoke(Vector2.zero);
        }
        private void CheckMap()
        {
            if(Input.GetKeyDown(KeyCode.M)||Input.GetKeyDown(KeyCode.Tab))
                OpenMap?.Invoke();
        }

        private void CheckMove()
        {
            if (Application.isMobilePlatform)
            {
                float horizontalInput = _joystick.Horizontal;
                float verticalInput = _joystick.Vertical;
                _direction = new Vector2(horizontalInput, verticalInput).normalized;
            }
            else
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                _direction = new Vector2(horizontalInput, verticalInput).normalized;
               
            }

            MoveDirection?.Invoke(_direction); 
            
        }
    }
}
