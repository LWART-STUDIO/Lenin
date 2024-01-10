using System.Collections;
using System.Collections.Generic;
using Camera;
using UnityEngine;
using Zenject;

public class CameraInstaller : MonoInstaller
{
    [SerializeField] private GameObject _cameraControl;
    public override void InstallBindings()
    {
        CameraControl cameraControl = Container
            .InstantiatePrefabForComponent<CameraControl>(_cameraControl, Vector3.zero, Quaternion.identity,
                null);

        Container
            .Bind<CameraControl>()
            .FromInstance(cameraControl)
            .AsSingle();
    }
}
