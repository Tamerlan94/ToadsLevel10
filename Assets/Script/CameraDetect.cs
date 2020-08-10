using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraDetect : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private GameObject playerFollow;
    private void Start()
    {
        GameEvent.current.OnSelect += SelectedPlayer;
        vcam = GetComponent<CinemachineVirtualCamera>();
        playerFollow = GameManager.current.currentPlayer;
        vcam.Follow = playerFollow.transform;
        
    }
    private void SelectedPlayer()
    {
        playerFollow = GameManager.current.currentPlayer;
        vcam.Follow = playerFollow.transform;
    }
}
