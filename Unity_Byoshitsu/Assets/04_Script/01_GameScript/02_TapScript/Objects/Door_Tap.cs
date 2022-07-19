using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door_Tap : TapCollider
{
    //閉まりドア
    public GameObject CloseDoor;
    //開きドア
    public GameObject OpenDoor;

    //エンディングクラス
    public ClearManager Escape;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        //Escape.White.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.0f);

        if (ItemManager.Instance.SelectItem != "Key2")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        SaveLoadSystem.Instance.gameData.isClearAll = true;

        //鍵を使用
        ItemManager.Instance.UseItem();

        Invoke(nameof(AfterClear1), 1.5f);
    }

    //演出
    private void AfterClear1()
    {
        //扉開く
        AudioManager.Instance.SoundSE("OpenShelf");
        OpenDoor.SetActive(true);
        CloseDoor.SetActive(false);
        Invoke(nameof(AfterClear2), 1.2f);
    }
    //エンディング
    private void AfterClear2()
    {
        Escape.Escape();
    }
}
