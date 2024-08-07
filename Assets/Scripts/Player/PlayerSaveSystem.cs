using System.IO;
using UnityEngine;

public class PlayerSaveSystem : MonoBehaviour
{
    private const string SAVEFILEPATH = "playerData.json";

    private int _bulletDamage;
    public int BulletDamage
    {
        get { return _bulletDamage; }
        set { _bulletDamage = value; }
    }

    private int _maxHealth;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    private float _attackSpeed;
    public float AttackSpeed
    {
        get { return _attackSpeed; }
        set { _attackSpeed = value; }
    }

    private float _shieldCooldown;
    public float ShieldCooldown
    {
        get { return _shieldCooldown; }
        set { _shieldCooldown = value; }
    }

    private readonly PlayerStatus playerStatus = new()
        {
            playerDamage = 1,
            playerMaxHealth = 10,
            playerAttackSpeed = 0.3f,
            playerShieldCooldown = 3f

        };

    public PlayerStatus CurrentPlayerStatus { get; set; }

    private void Start()
    {
        LoadData();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(playerStatus);
        File.WriteAllText(SAVEFILEPATH, json);
    }

    private void LoadData()
    {
        FileInfo savedFile = new(SAVEFILEPATH);
        if (!savedFile.Exists || savedFile.Length == 0)
        {
            SaveData();
        }

        string json = File.ReadAllText(SAVEFILEPATH);

        PlayerStatus loadedData = JsonUtility.FromJson<PlayerStatus>(json);
        CurrentPlayerStatus = loadedData;

        BulletDamage = loadedData.playerDamage;
        AttackSpeed = loadedData.playerAttackSpeed;
        MaxHealth = loadedData.playerMaxHealth;
        ShieldCooldown = loadedData.playerShieldCooldown;

        Debug.Log("D: " + BulletDamage + " A: " + AttackSpeed + "HP: " + MaxHealth);

    }

}
