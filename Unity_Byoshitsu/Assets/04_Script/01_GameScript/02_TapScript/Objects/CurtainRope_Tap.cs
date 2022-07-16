using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainRope_Tap : TapCollider
{
    //カーテンカット前
    public GameObject CurtainBefore;
    //カーテンカット後
    public GameObject CurtainAfter;

    //ハサミ
    public GameObject Sciccers;

    //カーテンクラス
    public Curtain_Judge CurtainClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (ItemManager.Instance.SelectItem != "Sciccers")
            return;


        BlockPanel.Instance.ShowBlock();

        //右側の皿をセットする
        ItemManager.Instance.UseItem();
        Sciccers.SetActive(true);

        //ハサミできる
        Invoke(nameof(AfterClear1), 1.5f);


        CurtainClass.Status = CurtainClass.Status.Substring(0, 6) + 0 + CurtainClass.Status.Substring(7);

        SaveLoadSystem.Instance.gameData.CurtainStatus = CurtainClass.Status;
        SaveLoadSystem.Instance.gameData.isClearSciccers = true;
        SaveLoadSystem.Instance.Save();
    }


    //ハサミできる
    private void AfterClear1()
    {
        AudioManager.Instance.SoundSE("Clear");

        Sciccers.SetActive(false);
        CurtainBefore.SetActive(false);
        CurtainAfter.SetActive(true);

        BlockPanel.Instance.HideBlock();

    }
}