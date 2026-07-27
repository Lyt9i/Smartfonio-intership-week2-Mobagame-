using UnityEngine;
using System;
using System.Collections.Generic;

public class CripFactory : MonoBehaviour
{
    [Serializable]
    public struct CripOption
    {
        public string name;      // просто для удобства в инспекторе, например "Melee"
        public Crip prefab;
        public int cost;
    }

    [SerializeField] private Crip _cripPrefab;      // для автоспавна, как раньше
    [SerializeField] private TeamTag _teamTag;
    [SerializeField] private int _amount;
    [SerializeField] private float _timeDelay;

    [Header("Крипы для ручного спавна игроком")]
    [SerializeField] private List<CripOption> _playerCripOptions;

    private float _currentDelay = 0;

    private void Update()
    {
        if (GameSession.IsSelected && GameSession.PlayerTeamId == _teamTag.GetTeamId())
            return;

        if (_currentDelay > 0)
        {
            _currentDelay -= Time.deltaTime;
            return;
        }

        _currentDelay = _timeDelay;

        for (int i = 0; i < _amount; i++)
        {
            var crip = Instantiate(_cripPrefab, transform);
            crip.gameObject.SetActive(true);
            crip.GetTeamTag().SetTeamId(_teamTag.GetTeamId());
            crip.Initialize();
        }
    }

    public bool TrySpawnByPlayer(int optionIndex)
    {
        if (GameSession.PlayerTeamId != _teamTag.GetTeamId()) return false;
        if (optionIndex < 0 || optionIndex >= _playerCripOptions.Count) return false;

        var option = _playerCripOptions[optionIndex];
        if (!ScoreManager.Instance.TrySpend(_teamTag.GetTeamId(), option.cost)) return false;

        var crip = Instantiate(option.prefab, transform.position, Quaternion.identity);
        crip.gameObject.SetActive(true);
        crip.GetTeamTag().SetTeamId(_teamTag.GetTeamId());
        crip.Initialize();
        return true;
    }

    public List<CripOption> GetPlayerCripOptions() => _playerCripOptions;
    public TeamTag GetTeamTag() => _teamTag;
}