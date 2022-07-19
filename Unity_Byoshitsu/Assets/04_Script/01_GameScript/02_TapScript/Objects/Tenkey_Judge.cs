using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tenkey_Judge : MonoBehaviour
{
    //入力状態
    public string Status = "";

    //スクリーンの数字オブジェクト
    public GameObject[] Images;
    //「x」画像
    public GameObject Msg;

    //クリア前のテンキー
    public GameObject Before;
    public GameObject After;
    //鍵2
    public GameObject Key2;


    /// <summary>
    /// ボタンタップ時
    /// </summary>
    /// <param name="No"></param>
    public void Input(int No)
    {
        if(No == 99)
        {
            //Enterの場合
            Judge();
        }
        else
        {
            //既に4文字の場合、何もしない
            if (Status.Length >= 4)
                return;

            //ステータス更新
            Status = No + Status;
            //スクリーン更新
            for(int i = 0; i < Status.Length; i++)
                Images[i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/03_Number/" + Status.Substring(i,1));
        }
    }

    /// <summary>
    /// Enterタップ時
    /// </summary>
    private void Judge()
    {
        //答え合わせ
        if (Status == "06")
            //正解時
            AfterClear1();
        else
        {
            //不正解の場合
            BlockPanel.Instance.ShowBlock();
            //入力をクリア
            Status = "";
            foreach (var obj in Images)
                obj.GetComponent<SpriteRenderer>().sprite = null;
            //×を表示
            Msg.SetActive(true);
            Invoke(nameof(HideBatsu),0.7f);
        }
    }

    //正解時
    private void AfterClear1()
    {
        BlockPanel.Instance.ShowBlock();
        AudioManager.Instance.SoundSE("Clear");

        Invoke(nameof(AfterClear2), 1.5f);

        SaveLoadSystem.Instance.gameData.isClearTenkey = true;
        SaveLoadSystem.Instance.Save();
    }
    //
    private void AfterClear2()
    {
        AudioManager.Instance.SoundSE("Slide");
        Before.SetActive(false);
        After.SetActive(true);
        Key2.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }


    //×を消す
    private void HideBatsu()
    {
        Msg.SetActive(false);
        BlockPanel.Instance.HideBlock();
    }
}
