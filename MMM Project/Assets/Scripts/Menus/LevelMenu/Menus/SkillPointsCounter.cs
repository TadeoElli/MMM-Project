using UnityEngine;

public class SkillPointsCounter : MonoBehaviour
{
    [SerializeField] private NexusStats stats;
    [SerializeField] private ChangeStats change;

    private void Update()
    {
        change.SetAmount(stats.currentSkillPoints.Value);
    }
}
