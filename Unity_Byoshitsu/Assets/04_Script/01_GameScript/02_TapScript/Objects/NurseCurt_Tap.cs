using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NurseCurt_Tap : TapCollider
{
    //閉まり
    public GameObject CloseSlide;
    //開き
    public GameObject OpenSlide;
    //体温計
    public GameObject Taionkei;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (ItemManager.Instance.SelectItem != "Key1")
            return;

        AudioManager.Instance.SoundSE("Clear");
        BlockPanel.Instance.ShowBlock();

        ItemManager.Instance.UseItem();

        //演出
        Invoke(nameof(AfterClear), 1.5f);

        //セーブデータ
        SaveLoadSystem.Instance.gameData.isClearNurseCurt = true;
        SaveLoadSystem.Instance.Save();
    }

    //演出
    private void AfterClear()
    {
        AudioManager.Instance.SoundSE("Slide");

        CloseSlide.SetActive(false);
        OpenSlide.SetActive(true);

        BlockPanel.Instance.HideBlock();
    }
}
