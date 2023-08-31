using System;
using Cinemachine;
using Player;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _playerCamera;
        private PlayerMarker _playerMarker;

        [Inject]
        private void Construct(PlayerMarker playerMarker)
        {
            _playerMarker = playerMarker;
        }

        private void Awake()
        {
            Transform playerTransform = _playerMarker.transform;
            _playerCamera.Follow = playerTransform;
            _playerCamera.LookAt = playerTransform;
        }
    }
}
