using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private TMP_Text keysText, timerText;

    private const string KeyString = "Keys : ";

    private float _timer;

    private void Update()
    {
        if (!LevelManager.LevelStarted) return;
        
        KeysUI();
        Timer();
    }

    private void KeysUI() => keysText.text = KeyString + TriggerInteractions.NbKeys;

    private void Timer()
    {
        _timer += Time.deltaTime;
        
        float minutes = Mathf.FloorToInt(_timer / 60);  
        float seconds = Mathf.FloorToInt(_timer % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}