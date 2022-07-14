using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVCardMacine_Tap : TapCollider
{
    //千円札
    public GameObject Sennenn;
    //TVカード
    public GameObject TVCard;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (ItemManager.Instance.SelectItem != "Money")
            return;

        BlockPanel.Instance.ShowBlock();

        //お札消費
        ItemManager.Instance.UseItem();
        //お札表示
        Sennenn.SetActive(true);

        //お札を入れる音
        Invoke(nameof(Sound), 0.8f);
    }

    //お札を入れる音
    private void Sound()
    {
        AudioManager.Instance.SoundSE("MoneyInsert");
        Invoke(nameof(HideMoney), 1.2f);
    }

    //お札非表示
    private void HideMoney()
    {
        Sennenn.SetActive(false);
        Invoke(nameof(ShowTVCard), 1.5f);
    }

    //テレビカード表示
    private void ShowTVCard()
    {
        TVCard.SetActive(true);
        AudioManager.Instance.SoundSE("Clear");

        BlockPanel.Instance.HideBlock();

        SaveLoadSystem.Instance.gameData.isClearMoney = true;
        SaveLoadSystem.Instance.Save();
    }
}
