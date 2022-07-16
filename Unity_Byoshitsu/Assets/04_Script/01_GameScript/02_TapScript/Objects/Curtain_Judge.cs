using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain_Judge : MonoBehaviour
{

    //カーテン状態(0:close,1:open,2:紐縛り)
    public string Status = "0000002000";

    //各カーテンオブジェクト
    public GameObject[] Curtains1;
    public GameObject[] Curtains2;
    public GameObject[] Curtains3;
    public GameObject[] Curtains4;
    public GameObject[] Curtains5;
    public GameObject[] Curtains6;
    public GameObject[] Curtains7;
    public GameObject[] Curtains8;
    public GameObject[] Curtains9;
    public GameObject[] Curtains10;

    //クリア有無
    public bool isClear = false;

    //パズル31クラス
    public Puzzle31_Judge Puzzle31Class;

    //TV画面
    public GameObject TVScreen;

    //カメラ視点名
    private string CameraPosition;

    //カーテン操作
    public void OpenClose(int No, int _status)
    {
        //カーテンの特定
        GameObject[] Curtains = Curtains1;
        if (No == 2)
            Curtains = Curtains2;
        else if (No == 3)
            Curtains = Curtains3;
        else if (No == 4)
            Curtains = Curtains4;
        else if (No == 5)
            Curtains = Curtains5;
        else if (No == 6)
            Curtains = Curtains6;
        else if (No == 7)
            Curtains = Curtains7;
        else if (No == 8)
            Curtains = Curtains8;
        else if (No == 9)
            Curtains = Curtains9;
        else if (No == 10)
            Curtains = Curtains10;

        //カーテン状態の把握
        int _NewStatus = 0;
        if (_status == 0)
            _NewStatus = 1;

        //カテーンオブジェクト切替
        if (_status == 0)
            AudioManager.Instance.SoundSE("Curtain1");
        else
            AudioManager.Instance.SoundSE("Curtain2");

        Curtains[_status].SetActive(false);
        Curtains[_NewStatus].SetActive(true);

        //ステータス更新
        if (No == 1)
            Status = _NewStatus + Status.Substring(1);
        else if (No == 10)
            Status = Status.Substring(0, 9) + _NewStatus;
        else
            Status = Status.Substring(0, No - 1) + _NewStatus + Status.Substring(No);

        //セーブデータ
        SaveLoadSystem.Instance.gameData.CurtainStatus = Status;
        SaveLoadSystem.Instance.Save();

        //パズルで31を作っている場合
        if (Puzzle31Class.isClear && !isClear)
            Judge();
    }

    /// <summary>
    /// カーテン開閉の答え合わせ
    /// </summary>
    private void Judge()
    {
        if (Status != "1111101010")
            return;

        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");
        //演出
        Invoke(nameof(AfterClear1), 1.5f);

        //セーブデータ
        isClear = true;
        SaveLoadSystem.Instance.gameData.isClearCurtain31 = true;
        SaveLoadSystem.Instance.Save();
    }

    //演出
    private void AfterClear1()
    {
        CameraPosition = CameraManager.Instance.CurrentPositionName;
        CameraManager.Instance.ChangeCameraPosition("TV2");
        Invoke(nameof(AfterClear2), 1.5f);
    }
    //
    private void AfterClear2()
    {
        //テレビ画面切替
        TVScreen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/90_Other/TV2");
        Invoke(nameof(AfterClear3), 1.5f);
    }
    //
    private void AfterClear3()
    {
        //元の視点へ
        CameraManager.Instance.ChangeCameraPosition(CameraPosition);
        BlockPanel.Instance.HideBlock();
    }
}
