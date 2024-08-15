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
    private int numberOfBossesInRoom;

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

        numberOfEnemiesInRoom = GetComponentsInChildren<BaseEnemy>().Length;
        numberOfBossesInRoom = GetComponentsInChildren<BaseBoss>().Length;
    }

    private void OnEnable()
    {
        BaseEnemy.onEnemyDeath += EnemyDied;
        BaseBoss.onEnemyDeath += BossDied;
    }

    private void OnDestroy()
    {
        BaseEnemy.onEnemyDeath -= EnemyDied;
        BaseBoss.onEnemyDeath -= BossDied;
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
            numberOfEnemiesInRoom = GetComponentsInChildren<BaseEnemy>().Length - 1;
            Debug.Log(gameObject.name + ": " + numberOfEnemiesInRoom);
            if (numberOfEnemiesInRoom <= 0 && numberOfBossesInRoom <= 0)
            {
                UnlockNextRoom();

                Debug.Log("All Enemies dead");
            }
        }
    }

    private void BossDied()
    {
        if (IsRoomUnlocked && numberOfBossesInRoom > 0)
        {
            numberOfBossesInRoom = GetComponentsInChildren<BaseBoss>().Length - 1;

            if (numberOfBossesInRoom <= 0 && numberOfEnemiesInRoom <= 0)
            {
                UnlockNextRoom();
            }

        }
    }
}
