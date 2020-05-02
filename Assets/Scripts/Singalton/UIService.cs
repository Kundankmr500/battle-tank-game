using UnityEngine;
using TMPro;
using System;
using Generic;

public class UIService : MonoSingletonGeneric<UIService>
{
    [Range(1,10)]
    public int TimeToShowUIOnScreen;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI EnemyKillText;

    protected override void Awake()
    {
        base.Awake();
        Init();
    }

    void Init()
    {
        ShowScore(0);
        ShowEnemyKill(0);
    }

    public async void ShowAchivementUI(GameObject achievement)
    {
        achievement.SetActive(true);
        await new WaitForSeconds(TimeToShowUIOnScreen);
        achievement.SetActive(false);

    }


    public void ShowScore(int scoreValue)
    {
        ScoreText.text = Constants.Score + scoreValue;
    }

    public void ShowEnemyKill(int enemyKillCount)
    {
        EnemyKillText.text = Constants.EnemiesKilled + enemyKillCount;
    }
}