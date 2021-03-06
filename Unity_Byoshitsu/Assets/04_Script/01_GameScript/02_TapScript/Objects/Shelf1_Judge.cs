using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf1_Judge : MonoBehaviour
{
    //正解したかどうか
    public bool isClear = false;

    //ボタンの4桁
    public string InputNo = "0000";

    //答えの4桁
    public string AnswerNo = "1032";

    //OpenDoor&CloseDoorオブジェクト
    public GameObject OpenDoor;
    public GameObject CloseDoor;
    public GameObject Money;




    //答え合わせ
    public void JudgeAnswer(string buttonName, int Index)
    {
        //入力値を更新
        if (buttonName == "Btn1") //ボタン1の時
        {
            //1桁目をチェンジ
            InputNo = Index + InputNo.Substring(1);
        }
        else if (buttonName == "Btn2") //ボタン2の時
        {
            //2桁目をチェンジ
            InputNo = InputNo.Substring(0, 1) + Index + InputNo.Substring(2);
        }
        else if (buttonName == "Btn3") //ボタン3の時
        {
            //3桁目をチェンジ
            InputNo = InputNo.Substring(0, 2) + Index + InputNo.Substring(3);
        }
        else //ボタン4の時
        {
            //4桁目をチェンジ
            InputNo = InputNo.Substring(0, 3) + Index;
        }


        //答え判定
        if (InputNo == AnswerNo)
        {
            //クリアの効果音
            AudioManager.Instance.SoundSE("Clear");
            //クリア判定をtrueに
            isClear = true;

            //画面ブロック
            BlockPanel.Instance.ShowBlock();

            //1秒後にカメラ移動
            Invoke(nameof(AfterClear1), 1.5f);

            //最後にセーブ
            SaveLoadSystem.Instance.gameData.isClearShelf1 = true;
            SaveLoadSystem.Instance.Save();
        }

    }



    //正解後のスライド開く
    private void AfterClear1()
    {
        //効果音
        AudioManager.Instance.SoundSE("OpenShelf");
        //Close→Open
        CloseDoor.SetActive(false);
        OpenDoor.SetActive(true);
        //MoneyCollider解除
        Money.SetActive(true);
        //画面ブロックを解除
        BlockPanel.Instance.HideBlock();
    }
}