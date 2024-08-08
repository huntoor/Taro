using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    }
    
    private void OnEnable()
    {
        
        Skill.blastSkill += BlastSkill;
        Skill.rapidFireSkill += RapidFireSkill;
        Skill.laserSkill += LaserSkill;
    }

    private void RapidFireSkill()
    {
        Debug.Log("Rapid Fire");
    }

    private void BlastSkill()
    {
        Debug.Log("Blast Skill");

    }

    private void LaserSkill()
    {
        Debug.Log("Laser Skill");
    }
}
