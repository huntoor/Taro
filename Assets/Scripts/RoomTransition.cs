using System;
using Cinemachine;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    CinemachineVirtualCamera roomCamera;
    private void Awake()
    {
        roomCamera = GetComponentInChildren<CinemachineVirtualCamera>(true);
    }
    private void Start()
    {
        if (roomCamera == null)
        {
            Debug.LogError("You Forgot to add Virtual Camera");
        }
        else
        {
            roomCamera.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D body)
    {
        if (body.CompareTag("Player"))
        {
            roomCamera.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D body)
    {
        if (body.CompareTag("Player"))
        {
            GetComponentInChildren<CinemachineVirtualCamera>(true).gameObject.SetActive(false);
        }
    }
}
