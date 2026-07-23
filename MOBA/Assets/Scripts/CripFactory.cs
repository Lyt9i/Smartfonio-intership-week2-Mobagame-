using UnityEngine;
using System.Collections.Generic;

public class CripFactory : MonoBehaviour
{
    [SerializeField] private Crip _cripPrefab;
    [SerializeField] private TeamTag _teamTag;
    [SerializeField] private int _amount;
    [SerializeField] private float _timeDelay;
    [SerializeField] private int _manualSpawnCost = 10;
    private float _currentDelay = 0;

    private void Update()
    {
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

    public void TrySpawnByPlayer()
    {
        if (GameSession.PlayerTeamId != _teamTag.GetTeamId()) return;
        if (!ScoreManager.Instance.TrySpend(_teamTag.GetTeamId(), _manualSpawnCost)) return;
        SpawnOne();
    }

    private void SpawnOne()
    {
        var crip = Instantiate(_cripPrefab, transform);
        crip.gameObject.SetActive(true);
        crip.GetTeamTag().SetTeamId(_teamTag.GetTeamId());
        crip.Initialize();
    }
    public TeamTag GetTeamTag() => _teamTag;

}