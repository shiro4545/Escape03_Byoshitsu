using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceBefore_Tap : TapCollider
{
    //天秤(皿セット前)
    public GameObject BalanceBefore;
    //天秤(皿セット後)
    public GameObject Balance;

    //右側の皿
    public GameObject RightPlate;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (ItemManager.Instance.SelectItem != "Balance")
            return;

        BlockPanel.Instance.ShowBlock();

        //右側の皿をセットする
        ItemManager.Instance.UseItem();
        RightPlate.SetActive(true);

        AudioManager.Instance.SoundSE("PutTambarin");


        //並行にする
        Invoke(nameof(AfterClear1), 1.2f);
    }

    //並行にする
    private void AfterClear1()
    {
        AudioManager.Instance.SoundSE("BalanceMove");
        //天秤を並行にする
        BalanceBefore.SetActive(false);
        Balance.SetActive(true);

        BlockPanel.Instance.HideBlock();

        SaveLoadSystem.Instance.gameData.isClearBalance = true;
        SaveLoadSystem.Instance.gameData.TenbinStatus = 0;
        SaveLoadSystem.Instance.Save();
    }
}
