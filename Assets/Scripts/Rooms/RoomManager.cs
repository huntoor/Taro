using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private RoomManager nextRoom;

    private RoomTransition nextRoomTransitionCamera;
    private RoomTransition myTransitionCamera;

    [SerializeField] private bool isFirstRoom;
    [SerializeField] private bool isLastRoom;
    
    public bool IsRoomUnlocked { get; private set; }

    private int numberOfEnemiesInRoom;

    public delegate void OnLastRoomFinished();
    public static OnLastRoomFinished onLastRoomFinished;

    private void Start()
    {
        if (isFirstRoom)
        {
            IsRoomUnlocked = true;
        }
        else
        {
            IsRoomUnlocked = false;
        }

        if (nextRoom != null)
        {
            nextRoomTransitionCamera = nextRoom.GetComponent<RoomTransition>();
        }
        myTransitionCamera = GetComponent<RoomTransition>();

        numberOfEnemiesInRoom = GetComponentsInChildren<Enemy>().Length;
        
    }

    private void OnEnable()
    {
        Enemy.onEnemyDeath += EnemyDied;
    }

    private void OnDestroy()
    {
        Enemy.onEnemyDeath -= EnemyDied;
    }

    private void UnlockNextRoom()
    {
        if (isLastRoom)
        {
            onLastRoomFinished?.Invoke();
            return;
        }

        nextRoom.UnlockRoom();

        nextRoomTransitionCamera.ActivateCamera();
        myTransitionCamera.DeactivateCamera();

    }

    public void UnlockRoom()
    {
        IsRoomUnlocked = true;
    }

    public void LockRoom()
    {
        IsRoomUnlocked = false;
    }

    private void EnemyDied()
    {
        if (IsRoomUnlocked && numberOfEnemiesInRoom > 0)
        {
            numberOfEnemiesInRoom = GetComponentsInChildren<Enemy>().Length - 1;
            Debug.Log(gameObject.name + ": " + numberOfEnemiesInRoom);
            if (numberOfEnemiesInRoom <= 0)
            {
                UnlockNextRoom();

                Debug.Log("All Enemies dead");
            }
        }
    }
}
