using UnityEngine;
using System;
using Generic;

public class UIService : MonoSingletonGeneric<UIService>
{
    [Range(1,10)]
    public int TimeToShowUIOnScreen;

    protected override void Awake()
    {
        base.Awake();
    }

    public async void ShowAchivementUI(GameObject achievement)
    {
        achievement.SetActive(true);
        await new WaitForSeconds(TimeToShowUIOnScreen);
        achievement.SetActive(false);

    }
}