using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nagashi_Tap : TapCollider
{
    //グラスオブジェクト
    public GameObject Glass;

    //WaterServerクラス
    public WaterServer_Judge WaterServerClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        //アイテムがGlass1~3以外だと無反応
        if (ItemManager.Instance.SelectItem != "Glass1" &&
            ItemManager.Instance.SelectItem != "Glass2" &&
            ItemManager.Instance.SelectItem != "Glass3")
            return;

        //演出
        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("OutWater");

        Glass.SetActive(true);
        Invoke(nameof(HideGlass), 1.5f);

        //ステータス更新
        SaveLoadSystem.Instance.gameData.getItems = SaveLoadSystem.Instance.gameData.getItems.Replace("Glass" + (WaterServerClass.GlassStatus), "Glass0");
        WaterServerClass.GlassStatus = 0;
        WaterServerClass.HotStatus = 0;
        WaterServerClass.ColdStatus = 0;

        //セーブデータ
        SaveLoadSystem.Instance.gameData.GlassStatus = 0;
        SaveLoadSystem.Instance.gameData.HotStatus = 0;
        SaveLoadSystem.Instance.gameData.ColdStatus = 0;
        SaveLoadSystem.Instance.Save();

    }

    //グラス消す
    private void HideGlass()
    {
        Glass.SetActive(false);

        //アイテム画像切替
        foreach (var obj in ItemManager.Instance.getItemsArray)
        {
            if (obj.GetComponent<Image>().sprite.name == ItemManager.Instance.SelectItem)
            {
                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/Glass0");
                break;
            }
        }

        ItemManager.Instance.SelectItem = "Glass0";

        BlockPanel.Instance.HideBlock();
    }
}
