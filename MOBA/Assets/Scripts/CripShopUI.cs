using UnityEngine;
using UnityEngine.UI;

public class CripShopUI : MonoBehaviour
{
    [SerializeField] private Button _meleeButton;
    [SerializeField] private Button _tankButton;
    [SerializeField] private Button _rangeButton;

    [SerializeField] private CripFactory[] _allFactories; // перетащи сюда ОБЕ фабрики (красную и синюю)

    private CripFactory _playerFactory;

    private void Start()
    {
        gameObject.SetActive(false);
        GameSession.onTeamSelected += HandleTeamSelected;

        _meleeButton.onClick.AddListener(() => _playerFactory?.TrySpawnByPlayer(0));
        _tankButton.onClick.AddListener(() => _playerFactory?.TrySpawnByPlayer(1));
        _rangeButton.onClick.AddListener(() => _playerFactory?.TrySpawnByPlayer(2));

        if (GameSession.IsSelected)
            HandleTeamSelected(GameSession.PlayerTeamId);
    }

    private void HandleTeamSelected(int teamId)
    {
        foreach (var factory in _allFactories)
        {
            if (factory.GetTeamTag().GetTeamId() == teamId)
            {
                _playerFactory = factory;
                break;
            }
        }

        gameObject.SetActive(_playerFactory != null);
    }

    private void OnDestroy() => GameSession.onTeamSelected -= HandleTeamSelected;
}