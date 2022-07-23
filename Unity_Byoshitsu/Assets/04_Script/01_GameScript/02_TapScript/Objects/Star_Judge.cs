using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Star_Judge : MonoBehaviour
{
    //ステータス
    public string Status = "000000";

    //閉まり扉
    public GameObject CloseDoor;
    //開き扉
    public GameObject OpenDoor;
    //Block1~3
    public GameObject Block1;
    public GameObject Block2;
    public GameObject Block3;

    //答え合わせ
    public void Judge(int Index)
    {
        if (!SaveLoadSystem.Instance.gameData.isClearTaionkei)
            return;

        //ステータス更新
        Status = Status.Substring(1) + Index;

        //答え合わせ
        if (Status != "453612")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        //演出
        Invoke(nameof(AfterClear1), 1.5f);

    }

    //演出
    private void AfterClear1()
    {
        AudioManager.Instance.SoundSE("OpenShelf");

        CloseDoor.SetActive(false);
        OpenDoor.SetActive(true);
        Block1.SetActive(true);
        Block2.SetActive(true);
        Block3.SetActive(true);

        //体温計をアイテム欄から消す
        for (int i = 0; i < ItemManager.Instance.getItemsArray.Length; i++)
        {
            if (ItemManager.Instance.getItemsArray[i].GetComponent<Image>().sprite.name == "Taionkei")
            {
                //枠線を非表示に
                ItemManager.Instance.getItemsArray[i].GetComponent<Outline>().enabled = false;

                //持ち物数がMaxの時 最後のアイテムを非表示に
                if (i == ItemManager.Instance.getItemsArray.Length - 1)
                {
                    ItemManager.Instance.getItemsArray[i].GetComponent<Image>().sprite = null;
                    ItemManager.Instance.getItemsArray[i].SetActive(false);
                    break;
                }

                //それ以降のアイテム画像を左に詰める
                for (int j = i + 1; j < ItemManager.Instance.getItemsArray.Length; j++)
                {
                    if (ItemManager.Instance.getItemsArray[j].GetComponent<Image>().sprite == null)
                    {
                        ItemManager.Instance.getItemsArray[j - 1].GetComponent<Image>().sprite = null;
                        ItemManager.Instance.getItemsArray[j - 1].SetActive(false);
                        break;
                    }
                    else if (j == ItemManager.Instance.getItemsArray.Length - 1)
                    {
                        ItemManager.Instance.getItemsArray[j - 1].GetComponent<Image>().sprite = ItemManager.Instance.getItemsArray[j].GetComponent<Image>().sprite;
                        ItemManager.Instance.getItemsArray[j].GetComponent<Image>().sprite = null;
                        ItemManager.Instance.getItemsArray[j].SetActive(false);
                        break;
                    }
                    else
                    {
                        ItemManager.Instance.getItemsArray[j - 1].GetComponent<Image>().sprite = ItemManager.Instance.getItemsArray[j].GetComponent<Image>().sprite;
                    }
                }
                break;
            }
        }

        foreach(var obj in ItemManager.Instance.getItemsArray)
            obj.GetComponent<Outline>().enabled = false;

        ItemManager.Instance.SelectItem = "";

        //セーブデータ
        SaveLoadSystem.Instance.gameData.getItems = SaveLoadSystem.Instance.gameData.getItems.Replace("Taionkei;", "");
        SaveLoadSystem.Instance.gameData.isClearStar = true;
        SaveLoadSystem.Instance.Save();

        BlockPanel.Instance.HideBlock();
    }

}
