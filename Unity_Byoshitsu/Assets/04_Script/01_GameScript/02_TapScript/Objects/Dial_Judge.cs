using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial_Judge : MonoBehaviour
{
    //クリア有無
    public bool isClear = false;

    //ボタンステータス
    public string Status = "000";

    //ボタン配列
    public GameObject[] BtnLeft;
    public GameObject[] BtnCenter;
    public GameObject[] BtnRight;

    //閉まり扉
    public GameObject CloseDoor;
    //開き扉
    public GameObject OpenDoor;

    //水タンク
    public GameObject WaterTunk;
    //鍵1
    public GameObject Key1;

    //PCクラス
    public PC_Judge PCClass;

    /// <summary>
    /// 答え合わせ
    /// </summary>
    /// <param name="PositionNo"></param>
    public void Judge(int PositionNo)
    {
        //現在の数字
        int Index = int.Parse(Status.Substring(PositionNo,1));

        GameObject[] BtnArray = BtnLeft;
        if(PositionNo == 1)
            BtnArray = BtnCenter;
        else if (PositionNo == 2)
            BtnArray = BtnRight;

        //数字の切替
        BtnArray[Index].SetActive(false);

        Index++;
        if (Index >= BtnArray.Length)
            Index = 0;

        BtnArray[Index].SetActive(true);

        //Status更新
        if (PositionNo == 0)
            Status = Index + Status.Substring(1);
        else if (PositionNo == 1)
            Status = Status.Substring(0, 1) + Index + Status.Substring(2);
        else
            Status = Status.Substring(0,2) + Index;

        //答え合わせ
        if (Status != "755" || !PCClass.isClear)
            return;

        AudioManager.Instance.SoundSE("Clear");
        BlockPanel.Instance.ShowBlock();

        //演出
        Invoke(nameof(AfterClear1), 1.5f);

        //セーブデータ
        isClear = true;
        SaveLoadSystem.Instance.gameData.isClearDial = true;
        SaveLoadSystem.Instance.Save();
    }

    //演出
    private void AfterClear1()
    {
        CameraManager.Instance.ChangeCameraPosition("Shelf5_btm");
        Invoke(nameof(AfterClear2), 1.5f);
    }
    //
    private void AfterClear2()
    {
        WaterTunk.SetActive(true);
        Key1.SetActive(true);

        CloseDoor.SetActive(false);
        OpenDoor.SetActive(true);
        AudioManager.Instance.SoundSE("OpenShelf");

        BlockPanel.Instance.HideBlock();
    }

    /// <summary>
    /// 視点を変えた時にダイアルをリセットする
    /// </summary>
    public void ResetDial()
    {
        Status = "000";

        foreach (var obj in BtnLeft)
            obj.SetActive(false);
        foreach (var obj in BtnCenter)
            obj.SetActive(false);
        foreach (var obj in BtnRight)
            obj.SetActive(false);

        BtnLeft[0].SetActive(true);
        BtnCenter[0].SetActive(true);
        BtnRight[0].SetActive(true);
    }
}
