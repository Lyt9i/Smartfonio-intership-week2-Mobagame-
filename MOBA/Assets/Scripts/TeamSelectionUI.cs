using UnityEngine;
using UnityEngine.UI;

public class TeamSelectionUI : MonoBehaviour
{
    [SerializeField] private Button _redTeamButton;
    [SerializeField] private Button _blueTeamButton;
    [SerializeField] private GameObject _selectionPanel;
    [SerializeField] private GameObject[] _objectsToEnableAfterSelection; // Level, бой и т.п.

    private void Awake()
    {
        _redTeamButton.onClick.AddListener(() => SelectTeam(1));
        _blueTeamButton.onClick.AddListener(() => SelectTeam(2));

        foreach (var obj in _objectsToEnableAfterSelection)
            obj.SetActive(false);
    }

    private void SelectTeam(int teamId)
    {
        GameSession.SetPlayerTeam(teamId);

        foreach (var obj in _objectsToEnableAfterSelection)
            obj.SetActive(true);

        _selectionPanel.SetActive(false);
    }
}