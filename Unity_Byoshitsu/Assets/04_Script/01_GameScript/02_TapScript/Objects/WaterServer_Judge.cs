using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterServer_Judge : MonoBehaviour
{
    //グラスの水量(0:空,1~3:水)
    public int GlassStatus = 3;
    public int HotStatus = 0;
    public int ColdStatus = 3;

    //HotとCoolの取っ手(0:close,1open)
    public GameObject[] Hots;
    public GameObject[] Colds;
    private GameObject[] Totte;

    //HotとCoolのグラス(0:空,1~2:水)
    public GameObject[] HotGlass;
    public GameObject[] ColdGlass;
    private GameObject[] Glass;

    //水タンクセットクラス
    public SetWaterTunk_Tap SetTunkClass;

    //

    /// <summary>
    /// 水をグラスに入れる
    /// </summary>
    /// <param name="CH"></param> (0:Coll,1:Hot)
    public void PourWater(int CH)
    {
        //コップに水がいっぱいだったら無反応
        //水タンクがない場合は無反応
        if (GlassStatus == 3 || !SetTunkClass.isClear)
            return;

        //アイテムがGlass0~2以外だと無反応
        if (ItemManager.Instance.SelectItem != "Glass0" &&
            ItemManager.Instance.SelectItem != "Glass1" &&
            ItemManager.Instance.SelectItem != "Glass2")
            return;

        //Hot,Coldの識別
        Totte = CH == 0 ? Colds : Hots;
        Glass = CH == 0 ? ColdGlass : HotGlass;
        if (CH == 0)
            ColdStatus++;
        else
            HotStatus++;

        //演出
        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("WaterServer");

        Totte[0].SetActive(false);
        Totte[1].SetActive(true);
        Glass[GlassStatus].SetActive(true);

        Invoke(nameof(StopWater), 1.5f);

    }

    //演出
    private void StopWater()
    {
        Totte[1].SetActive(false);
        Totte[0].SetActive(true);
        Glass[GlassStatus].SetActive(false);

        //次の水量
        GlassStatus++;

        //アイテム画像切替
        foreach(var obj in ItemManager.Instance.getItemsArray)
        {
            if(obj.GetComponent<Image>().sprite.name == ItemManager.Instance.SelectItem)
            {
                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/Glass" + GlassStatus);
                break;
            }
        }

        ItemManager.Instance.SelectItem = "Glass" + GlassStatus;

        BlockPanel.Instance.HideBlock();

        //セーブデータ
        SaveLoadSystem.Instance.gameData.GlassStatus = GlassStatus;
        SaveLoadSystem.Instance.gameData.HotStatus = HotStatus;
        SaveLoadSystem.Instance.gameData.ColdStatus = ColdStatus;
        SaveLoadSystem.Instance.gameData.getItems = SaveLoadSystem.Instance.gameData.getItems.Replace("Glass" + (GlassStatus -1 ), "Glass" + GlassStatus);
        SaveLoadSystem.Instance.Save();
    }
}
