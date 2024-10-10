using System;
using Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    [SerializeField] private Transform white;
    [SerializeField] private Transform whiteToBlack;
    [SerializeField] private Transform black;
    [SerializeField] private Transform blackToWhite;

    private void Awake()
    {
        virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ChangeFollow()
    {
        switch (Player.playerType)
        {
            case Enums.PlayerType.White:
                virtualCamera.Follow = white;
                break;
            case Enums.PlayerType.Black:
                virtualCamera.Follow = black;
                break;
            case Enums.PlayerType.WhiteToBlack:
                virtualCamera.Follow = whiteToBlack;
                break;
            case Enums.PlayerType.BlackToWhite:
                virtualCamera.Follow = blackToWhite;
                break;
        }
    }
    
    public void Shake()
    {
        virtualCameraNoise.m_AmplitudeGain = 1;
        
        Invoke(nameof(StopShake), 0.3f);
    }
    
    private void StopShake()
    {
        virtualCameraNoise.m_AmplitudeGain = 0;
    }
}
