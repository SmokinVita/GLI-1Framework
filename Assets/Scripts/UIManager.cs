using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UIManager is NULL!");

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
    }

    [SerializeField]
    private TMP_Text _score;
    [SerializeField]
    private TMP_Text _ammoCount;
    [SerializeField]
    private TMP_Text _aiCount;
    [SerializeField]
    private TMP_Text _timeRemaining;
    [SerializeField]
    private TMP_Text _warningText;

    public void UpdateScore(int score)
    {
        _score.SetText(score.ToString());
    }

    public void UpdateAmmoCount(int ammo)
    {
        _ammoCount.SetText(ammo.ToString());
    }

    public void UpdateAICount(int ai)
    {
        _aiCount.SetText(ai.ToString());
    }

    public void UpdateTimeRemaining(float timeRemaining, bool spawnStarted)
    {
        if (spawnStarted == false)
        {
            _warningText.enabled = false;
            float minutes = Mathf.FloorToInt(timeRemaining / 60);
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            _timeRemaining.SetText("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            _timeRemaining.enabled = false;
            _warningText.enabled = true;
        }
    }

}
