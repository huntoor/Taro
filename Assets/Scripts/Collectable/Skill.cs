using UnityEngine;

public class Skill : MonoBehaviour
{    
    private enum SkillType
    {
        RapidFire,
        Blast,
        Laser
    }

    [SerializeField] private SkillType skillType;

    public delegate void RapidFireSkill();
    public static RapidFireSkill rapidFireSkill;

    public delegate void BlastSkill();
    public static BlastSkill blastSkill;

    public delegate void LaserSkill();
    public static LaserSkill laserSkill;

    private void OnTriggerEnter2D(Collider2D body)
    {
        if (body.CompareTag("Player"))
        {
            switch (skillType)
            {
                case SkillType.RapidFire:
                    rapidFireSkill?.Invoke();
                    break;

                case SkillType.Blast:
                    blastSkill?.Invoke();
                    break;
                    
                case SkillType.Laser:
                    laserSkill?.Invoke();
                    break;

            }

            Destroy(gameObject);
        }
    }
}
