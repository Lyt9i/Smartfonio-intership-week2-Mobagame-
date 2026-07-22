using UnityEngine;

public class KillRewardListener : MonoBehaviour
{
    private void Start()
    {
        GamePlayManager.Instance.onRemoved += HandleUnitRemoved;
    }

    private void HandleUnitRemoved(Unit unit)
    {
        if (unit.GetKillReward() <= 0) return;
        var rewardTeam = ScoreManager.GetOtherTeam(unit.GetTeamTag().GetTeamId());
        ScoreManager.Instance.AddPoints(rewardTeam, unit.GetKillReward());
    }
}