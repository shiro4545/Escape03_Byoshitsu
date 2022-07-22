using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Judge : MonoBehaviour
{
    //入力状態
    public string InputCode = "";
    //クリア判定
    public bool isClear = false;

    //パスワード画面
    public GameObject ScreenPSW;
    //入力画像配列
    public GameObject[] Mojis;
    //エラーメッセージ
    public GameObject Msg;

    //予定画面
    public GameObject ScreenTime;



    /// <summary>
    /// キー入力
    /// </summary>
    /// <param name="code"></param>
    public void Input(string code)
    {
        Msg.SetActive(false);

        //InputCodeの更新
        if (code == "BackSpace")
        {
            if (InputCode.Length != 0)
            {
                Mojis[InputCode.Length / 2 - 1].GetComponent<SpriteRenderer>().sprite = null;
                InputCode = InputCode.Substring(0, InputCode.Length - 2);
            }
        }
        else if (code == "Space")
        {
        }
        else if (code == "Enter")
            Judge();
        else
        {
            //既に7文字だったら何もしない
            if (InputCode.Length / 2 >= 7)
                return;

            Mojis[InputCode.Length / 2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/04_Moji/" + code);
            InputCode += code;
        }
    }

    /// <summary>
    /// 答え合わせ
    /// </summary>
    private void Judge()
    {
        foreach (var obj in Mojis)
            obj.GetComponent<SpriteRenderer>().sprite = null;

        if (InputCode != "taiiiinniiwaii")
        {
            //不正解の場合
            Msg.SetActive(true);
            InputCode = "";
            return;
        }

        //正解の場合
        AudioManager.Instance.SoundSE("Clear");
        BlockPanel.Instance.ShowBlock();

        //演出
        Invoke(nameof(AfterClear1), 1.1f);

        InputCode = "";
        isClear = true;

        SaveLoadSystem.Instance.gameData.isClearPC = true;
        SaveLoadSystem.Instance.Save();
    }

    //演出
    private void AfterClear1()
    {
        ScreenPSW.SetActive(false);
        ScreenTime.SetActive(true);
        BlockPanel.Instance.HideBlock();
    }
}
