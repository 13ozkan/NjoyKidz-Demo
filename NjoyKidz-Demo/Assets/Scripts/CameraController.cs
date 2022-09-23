using Instance;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoSingleton<CameraController>
{
    [HideInInspector] public Camera mainCamera;
    private const float _refRatio = (float)1080 / 1920;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void SetOrthographicSize(float size)
    {
        var currentRatio = (float)Screen.width / Screen.height;
        var multiplier = currentRatio / _refRatio;
        mainCamera.orthographicSize = (size) / multiplier;
    }
}
