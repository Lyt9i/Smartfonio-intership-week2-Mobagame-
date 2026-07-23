using System;

public static class GameSession
{
    public static int PlayerTeamId { get; private set; } = 0; // 0 = ещё не выбрано
    public static event Action<int> onTeamSelected;

    public static void SetPlayerTeam(int teamId)
    {
        PlayerTeamId = teamId;
        onTeamSelected?.Invoke(teamId);
    }

    public static bool IsSelected => PlayerTeamId != 0;
}