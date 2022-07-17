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
    public GameObject BtnGlass1;
    public GameObject BtnGlass2;
    public GameObject BtnGlass3;

    //ウォータサーバークラス
    public WaterServer_Judge WaterServerClass;

    // Start is called before the first frame update
    void Start()
    {
      Instance = this;

        foreach(var obj in getItemsArray)
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
            if(getItemsArray[i].GetComponent<Image>().sprite == null)
            {
                getItemsArray[i].GetComponent<Image>().sprite  = Resources.Load<Sprite>("Images/01_Items/" + itemName);
                getItemsArray[i].SetActive(true);
                break;
            }
        }

        //セーブデータ
        if(itemName == "Medicine1")
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
      for(int i = 0; i < getItemsArray.Length; i++)
      {
        if(getItemsArray[i].GetComponent<Image>().sprite == null)
        {
          getItemsArray[i].GetComponent<Image>().sprite  = Resources.Load<Sprite>("Images/01_Items/" + itemName);
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
      if(item.GetComponent<Outline>().enabled)
      {
        ShowItem(item);
        return;
      }

      //未選択の場合
      foreach(var obj in getItemsArray)
      {
        if(item == obj)
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


        //未開封薬の場合に透明ボタン表示
        if (ItemImage.GetComponent<Image>().sprite.name == "Medicine_Pack" )
            BtnMedicinePack.SetActive(true);

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

          //持ち物数がMaの時
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



}
