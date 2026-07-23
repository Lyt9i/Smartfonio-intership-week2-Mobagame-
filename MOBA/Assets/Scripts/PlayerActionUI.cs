using UnityEngine;
using UnityEngine.UI;

public class PlayerActionUI : MonoBehaviour
{
    [SerializeField] private Button _spawnCripButton;
    [SerializeField] private CripFactory[] _allCripFactories; // перетащи сюда ВСЕ фабрики (красная + синяя)

    private CripFactory _playerCripFactory;

    private void Start()
    {
        gameObject.SetActive(false);
        _spawnCripButton.onClick.AddListener(() => _playerCripFactory?.TrySpawnByPlayer());
        GameSession.onTeamSelected += HandleTeamSelected;

        if (GameSession.IsSelected)
        {
            HandleTeamSelected(GameSession.PlayerTeamId);
        }
    }

    private void HandleTeamSelected(int teamId)
    {
        _playerCripFactory = FindFactoryForTeam(teamId);
        gameObject.SetActive(_playerCripFactory != null);
    }

    private CripFactory FindFactoryForTeam(int teamId)
    {
        foreach (var factory in _allCripFactories)
        {
            if (factory.GetTeamTag().GetTeamId() == teamId)
                return factory;
        }
        return null;
    }

    private void OnDestroy() => GameSession.onTeamSelected -= HandleTeamSelected;
}