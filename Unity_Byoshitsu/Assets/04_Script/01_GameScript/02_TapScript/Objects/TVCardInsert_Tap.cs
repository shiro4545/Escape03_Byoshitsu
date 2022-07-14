using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVCardInsert_Tap : TapCollider
{
    //TVカード
    public GameObject TVCard;
    //テレビ画面
    public GameObject TVScreen;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();


        if (ItemManager.Instance.SelectItem != "TVCard")
            return;

        BlockPanel.Instance.ShowBlock();

        //TVカード消費
        ItemManager.Instance.UseItem();
        //お札表示
        TVCard.SetActive(true);

        AudioManager.Instance.SoundSE("CardInsert");


        //カードを入れる
        Invoke(nameof(HideCard), 1.3f);
    }

    //カード入れる
    private void HideCard()
    {
        TVCard.SetActive(false);
        AudioManager.Instance.SoundSE("Clear");

        //視点切替
        Invoke(nameof(AfterClear1), 1f);
    }

    //視点切替
    private void AfterClear1()
    {
        CameraManager.Instance.ChangeCameraPosition("TV1");
        //テレビ画面切替
        Invoke(nameof(AfterClear2), 1.5f);
    }

    //テレビ画面切替
    private void AfterClear2()
    {
        TVScreen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/90_Other/TV");

        BlockPanel.Instance.HideBlock();

        SaveLoadSystem.Instance.gameData.isClearTVCard = true;
        SaveLoadSystem.Instance.Save();
    }

}
