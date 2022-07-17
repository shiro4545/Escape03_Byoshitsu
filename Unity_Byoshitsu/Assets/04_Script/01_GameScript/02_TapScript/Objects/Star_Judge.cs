using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        //セーブデータ
        SaveLoadSystem.Instance.gameData.isClearStar = true;
        SaveLoadSystem.Instance.Save();
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

        BlockPanel.Instance.HideBlock();
    }

}
