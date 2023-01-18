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
    private TMP_Text _aiCount;
    [SerializeField]
    private TMP_Text _timeRemaining;

    public void UpdateScore(int score)
    {
        _score.SetText(score.ToString());
    }

    public void UpdateAICount(int ai)
    {
        _aiCount.SetText(ai.ToString());
    }

    public void UpdateTimeRemaining(float time)
    {
        _timeRemaining.SetText(time.ToString());
    }

}
