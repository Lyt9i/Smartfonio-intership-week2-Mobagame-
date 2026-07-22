using UnityEngine;
using TMPro; 
public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _redText;
    [SerializeField] private TextMeshProUGUI _blueText;

    private void Start()
    {
        ScoreManager.Instance.onPointsChanged += HandlePointsChanged;

        
        UpdateText(1, ScoreManager.Instance.GetPoints(1));
        UpdateText(2, ScoreManager.Instance.GetPoints(2));
    }

    private void HandlePointsChanged(int teamId, int newValue)
    {
        UpdateText(teamId, newValue);
    }

    private void UpdateText(int teamId, int value)
    {
        if (teamId == 1)
        {
            _redText.text = $"Очков у красных: {value}";
        }
        else if (teamId == 2)
        {
            _blueText.text = $"Очков у синих: {value}";
        }
    }

    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.onPointsChanged -= HandlePointsChanged;
        }
    }
}