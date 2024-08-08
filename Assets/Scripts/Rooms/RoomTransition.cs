using Cinemachine;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    private CinemachineVirtualCamera roomCamera;
    private RoomManager roomManager;

    private void Awake()
    {
        roomCamera = GetComponentInChildren<CinemachineVirtualCamera>(true);

        roomManager = GetComponent<RoomManager>();
    }

    private void Start()
    {
        if (roomCamera == null)
        {
            Debug.LogError("You Forgot to add Virtual Camera");
        }
        else
        {
            DeactivateCamera();
        }
    }

    private void OnTriggerEnter2D(Collider2D body)
    {
        if (body.CompareTag("Player"))
        {
            if (roomManager.IsRoomUnlocked)
            {
                ActivateCamera();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D body)
    {
        if (body.CompareTag("Player"))
        {
            if (roomManager.IsRoomUnlocked)
            {
                roomManager.LockRoom();

                DeactivateCamera();
            }
        }
    }

    public void ActivateCamera()
    {
        roomCamera.gameObject.SetActive(true);
    }

    public void DeactivateCamera()
    {
        roomCamera.gameObject.SetActive(false);
    }

    public void SwitchRooms(RoomTransition roomToDeactivate, RoomTransition roomToActivate)
    {
        roomToActivate.ActivateCamera();

        roomToDeactivate.DeactivateCamera();
    }
}
