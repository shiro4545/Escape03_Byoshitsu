using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWaterTunk_Tap : TapCollider
{
    //クリア有無
    public bool isClear = false;
    //水タンク
    public GameObject WaterTunk;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        if (ItemManager.Instance.SelectItem != "WaterTunk")
            return;

        AudioManager.Instance.SoundSE("PutTunk");

        ItemManager.Instance.UseItem();
        WaterTunk.SetActive(true);

        isClear = true;
        SaveLoadSystem.Instance.gameData.isClearWaterTunk = true;
        SaveLoadSystem.Instance.Save();
    }
}
