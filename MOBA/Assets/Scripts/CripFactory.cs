using UnityEngine;
using System.Collections.Generic;

public class CripFactory : MonoBehaviour
{
    [SerializeField] private Crip _cripPrefab;
    [SerializeField] private TeamTag _teamTag;
    [SerializeField] private int _amount;
    [SerializeField] private float _timeDelay;
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

}