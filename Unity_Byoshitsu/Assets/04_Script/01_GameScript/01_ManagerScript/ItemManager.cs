using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    //選択中のアイテム名
    public string SelectItem;
    //ヘッダーに並ぶアイテム画像
    public GameObject[] getItemsArray;
    //アイテムパネル全体
    public GameObject ItemPanel;
    //アイテム拡大画像
    public GameObject ItemImage;

    //特定アイテムでの透明ボタン
    public GameObject BtnMedicinePack;
    public GameObject BtnTaionkei;
    public GameObject BtnGlass;

    //ウォータサーバークラス
    public WaterServer_Judge WaterServerClass;

    //体温計の画像名
    public string TaionkeiName = "T_0";

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        foreach (var obj in getItemsArray)
        {
            obj.GetComponent<Button>().onClick.AddListener(() =>
            {
                AudioManager.Instance.SoundSE("TapUIBtn");
                onTapItemImage(obj);
            });
        }

        //アイテム拡大画面でタップする場合

        //鍵入り箱をひっくり返す/ドライバーで開ける
        BtnMedicinePack.GetComponent<Button>().onClick.AddListener(() =>
        {
            GetMedicine4();
        });

        //体温計の画面を見る
        BtnTaionkei.GetComponent<Button>().onClick.AddListener(() =>
        {
            WatchScreen();
        });

        //グラスの水の温度を測る
        BtnGlass.GetComponent<Button>().onClick.AddListener(() =>
        {
            UseTaionkei();
        });
    }

    //<summary>
    //アイテム取得
    //</summary>
    //<param>アイテム名</param>
    public void GetItem(string itemName)
    {
        AudioManager.Instance.SoundSE("ItemGet");

        for (int i = 0; i < getItemsArray.Length; i++)
        {
            if (getItemsArray[i].GetComponent<Image>().sprite == null)
            {
                getItemsArray[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/" + itemName);
                getItemsArray[i].SetActive(true);
                break;
            }
        }

        //セーブデータ
        if (itemName == "Medicine1")
            SaveLoadSystem.Instance.gameData.isGetMedicine1 = true;
        if (itemName == "Medicine2")
            SaveLoadSystem.Instance.gameData.isGetMedicine2 = true;
        if (itemName == "Medicine3")
            SaveLoadSystem.Instance.gameData.isGetMedicine3 = true;
        if (itemName == "Medicine_Pack")
            SaveLoadSystem.Instance.gameData.isGetMedicinePack = true;
        if (itemName == "Money")
            SaveLoadSystem.Instance.gameData.isGetMoney = true;
        if (itemName == "TVCard")
            SaveLoadSystem.Instance.gameData.isGetTVCard = true;
        if (itemName == "Balance")
            SaveLoadSystem.Instance.gameData.isGetBalance = true;
        if (itemName == "Sciccers")
            SaveLoadSystem.Instance.gameData.isGetSciccers = true;
        if (itemName == "Glass3")
            SaveLoadSystem.Instance.gameData.isGetGlass = true;
        if (itemName == "WaterTunk")
            SaveLoadSystem.Instance.gameData.isGetWaterTunk = true;
        if (itemName == "Key1")
            SaveLoadSystem.Instance.gameData.isGetKey1 = true;
        if (itemName == "Taionkei")
            SaveLoadSystem.Instance.gameData.isGetTaionkei = true;
        if (itemName == "Block1")
            SaveLoadSystem.Instance.gameData.isGetBlock1 = true;
        if (itemName == "Block2")
            SaveLoadSystem.Instance.gameData.isGetBlock2 = true;
        if (itemName == "Block3")
            SaveLoadSystem.Instance.gameData.isGetBlock3 = true;
        if (itemName == "Block4")
            SaveLoadSystem.Instance.gameData.isGetBlock4 = true;
        if (itemName == "Key2")
            SaveLoadSystem.Instance.gameData.isGetKey2 = true;

        SaveLoadSystem.Instance.gameData.getItems += itemName + ";";
        SaveLoadSystem.Instance.Save();
    }

    //<summary>
    //取得アイテムのロード
    //</summary>
    //<param>アイテム名</param>
    public void loadItem(string itemName)
    {
        for (int i = 0; i < getItemsArray.Length; i++)
        {
            if (getItemsArray[i].GetComponent<Image>().sprite == null)
            {
                getItemsArray[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/" + itemName);
                getItemsArray[i].SetActive(true);
                break;
            }
        }
    }

    //<summary>
    //アイテム選択
    //</summary>
    //<param>アイテムオブジェクト</param>
    private void onTapItemImage(GameObject item)
    {
        //選択済みの場合
        if (item.GetComponent<Outline>().enabled)
        {
            ShowItem(item);
            return;
        }

        //未選択の場合
        foreach (var obj in getItemsArray)
        {
            if (item == obj)
            {
                obj.GetComponent<Outline>().enabled = true;
                SelectItem = obj.GetComponent<Image>().sprite.name;
            }
            else
            {
                obj.GetComponent<Outline>().enabled = false;
            }
        }
    }

    //<summary>
    //アイテム拡大画面の表示
    //</summary>
    //<param>アイテムオブジェクト</param>
    private void ShowItem(GameObject item)
    {
        ItemPanel.SetActive(true);
        ItemImage.GetComponent<Image>().sprite = item.GetComponent<Image>().sprite;
        CameraManager.Instance.ButtonLeft.SetActive(false);
        CameraManager.Instance.ButtonRight.SetActive(false);
        CameraManager.Instance.ButtonBack.SetActive(true);

        //透明ボタンを非表示
        BtnMedicinePack.SetActive(false);
        BtnTaionkei.SetActive(false);
        BtnGlass.SetActive(false);


        //未開封薬の場合に透明ボタン表示
        if (SelectItem == "Medicine_Pack")
            BtnMedicinePack.SetActive(true);

        //体温計の画面を見る
        if (SelectItem == "Taionkei")
            BtnTaionkei.SetActive(true);

        //グラスの水量1~3の時
        if (SelectItem == "Glass1" || SelectItem == "Glass2" || SelectItem == "Glass3")
            BtnGlass.SetActive(true);
    }

    //<summary>
    //アイテム使用時
    //</summary>
    //<param>アイテム名</param>
    public void UseItem()
    {
      for(int i = 0; i < getItemsArray.Length; i++)
      {
        if(getItemsArray[i].GetComponent<Image>().sprite.name == SelectItem)
        {
          //枠線を非表示に
          getItemsArray[i].GetComponent<Outline>().enabled = false;

          //持ち物数がMaxの時 最後のアイテムを非表示に
          if(i == getItemsArray.Length - 1)
          {
            getItemsArray[i].GetComponent<Image>().sprite = null;
            getItemsArray[i].SetActive(false);
            break;
          }

          //それ以降のアイテム画像を左に詰める
          for(int j = i + 1; j < getItemsArray.Length; j++)
          {
            if(getItemsArray[j].GetComponent<Image>().sprite == null)
            {
              getItemsArray[j - 1].GetComponent<Image>().sprite = null;
              getItemsArray[j - 1].SetActive(false);
              break;
            }
            else if(j == getItemsArray.Length - 1)
            {
              getItemsArray[j - 1].GetComponent<Image>().sprite = getItemsArray[j].GetComponent<Image>().sprite;
              getItemsArray[j].GetComponent<Image>().sprite = null;
              getItemsArray[j].SetActive(false);
              break;
            }
            else
            {
              getItemsArray[j - 1].GetComponent<Image>().sprite = getItemsArray[j].GetComponent<Image>().sprite;
            }
          }
          break;
        }
      }
      //セーブデータ
      SaveLoadSystem.Instance.gameData.getItems = SaveLoadSystem.Instance.gameData.getItems.Replace(SelectItem + ";","");
      
      SelectItem = "";
      SaveLoadSystem.Instance.Save();
    }

    //<summary>
    //未開封薬を薬4に変更
    //</summary>
    //<param></param>
    private void GetMedicine4()
    {
        BtnMedicinePack.SetActive(false);
        AudioManager.Instance.SoundSE("Clear");
        //拡大画面をKey2に変える
        ItemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/Medicine4");
        BtnMedicinePack.SetActive(false);

        //ヘッダーのアイテム画像を変える
        foreach (var obj in getItemsArray)
        {
            if (obj.GetComponent<Image>().sprite.name == "Medicine_Pack")
            {
                //アイテム画像を変える
                obj.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/Medicine4");
                break;
            }
        }

        SaveLoadSystem.Instance.gameData.getItems = SaveLoadSystem.Instance.gameData.getItems.Replace("Medicine_Pack", "Medicine4");
        SaveLoadSystem.Instance.Save();
    }

    /// <summary>
    /// 体温計の画面を見る
    /// </summary>
    private void WatchScreen()
    {
        BtnTaionkei.SetActive(false);
        AudioManager.Instance.SoundSE("TapUIBtn");
        ItemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/" + TaionkeiName);
    }

    //<summary>
    //グラスの水の温度を測る
    //</summary>
    //<param></param>
    private void UseTaionkei()
    {
        //体温計以外は無反応
        if (SelectItem != "Taionkei")
            return;

        BtnGlass.SetActive(false);
        AudioManager.Instance.SoundSE("TapUIBtn");
        BlockPanel.Instance.ShowBlock();

        //測定写真に切替
        ItemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/GT" + WaterServerClass.GlassStatus);

        Invoke(nameof(After1), 1.5f);
    }

    //温度測定の演出
    private void After1()
    {
        AudioManager.Instance.SoundSE("Pon");
        Invoke(nameof(After2), 1f);
    }
    //
    private void After2()
    {
        int h = WaterServerClass.HotStatus;
        int c = WaterServerClass.ColdStatus;

        if (h == 0)
            TaionkeiName = "T_C";
        else if (c == 0)
            TaionkeiName = "T_H";
        else if (h == 1 && c == 1)
            TaionkeiName = "T_C1H1";
        else if (h == 2 && c == 1)
            TaionkeiName = "T_C1H2";
        else if (h == 1 && c == 2)
            TaionkeiName = "T_C2H1";

        ItemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/" + TaionkeiName);

        //答え合わせ
        if (TaionkeiName == "T_C2H1")
        {
            TaionkeiName = "T_Goal";
            AudioManager.Instance.SoundSE("Clear");
            Invoke(nameof(After3), 2f);
            SaveLoadSystem.Instance.gameData.isClearTaionkei = true;
        }
        else
            BlockPanel.Instance.HideBlock();

        SaveLoadSystem.Instance.gameData.TaionkeiStatus = TaionkeiName;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void After3()
    {
        //星形画面に切替
        ItemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/01_Items/" + TaionkeiName);

        //アイテム欄からGlass3削除
        for (int i = 0; i < getItemsArray.Length; i++)
        {
            if (getItemsArray[i].GetComponent<Image>().sprite.name == "Glass3")
            {
                //持ち物数がMaxの時 最後のアイテムを非表示に
                if (i == getItemsArray.Length - 1)
                {
                    getItemsArray[i].GetComponent<Image>().sprite = null;
                    getItemsArray[i].SetActive(false);
                    break;
                }

                //それ以降のアイテム画像を左に詰める
                for (int j = i + 1; j < getItemsArray.Length; j++)
                {
                    if (getItemsArray[j].GetComponent<Image>().sprite == null)
                    {
                        getItemsArray[j - 1].GetComponent<Image>().sprite = null;
                        getItemsArray[j - 1].SetActive(false);
                        break;
                    }
                    else if (j == getItemsArray.Length - 1)
                    {
                        getItemsArray[j - 1].GetComponent<Image>().sprite = getItemsArray[j].GetComponent<Image>().sprite;
                        getItemsArray[j].GetComponent<Image>().sprite = null;
                        getItemsArray[j].SetActive(false);
                        break;
                    }
                    else
                    {
                        getItemsArray[j - 1].GetComponent<Image>().sprite = getItemsArray[j].GetComponent<Image>().sprite;
                    }
                }
                break;
            }
        }

        //アイテム蘭の枠線をリセット
        foreach(var item in getItemsArray)
        {
            item.GetComponent<Outline>().enabled = false;

            if (item.GetComponent<Image>().sprite == null)
                break;
            if(item.GetComponent<Image>().sprite.name == "Taionkei")
                item.GetComponent<Outline>().enabled = true;
        }

        //セーブデータ
        SaveLoadSystem.Instance.gameData.getItems = SaveLoadSystem.Instance.gameData.getItems.Replace("Glass3;", "");
        SaveLoadSystem.Instance.Save();

        BlockPanel.Instance.HideBlock();
    }
}
