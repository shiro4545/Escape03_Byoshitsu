using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartResetManager : MonoBehaviour
{
    private GameData gameData;


    //アイテムオブジェクト**********************
    public GameObject Hanger;


    //ゲーム内オブジェクト**********************
    //public PutHanger_Tap PutHangerClass;


    //<summary>
    //タイトル画面の「はじめから」の時
    //<summary>
    public void GameStart()
    {
        SaveLoadSystem.Instance.GameStart();
    }

    //<summary>
    //タイトル画面の「続きから」の時
    //<summary>
    public void GameContinue()
    {
        SaveLoadSystem.Instance.Load();
        gameData = SaveLoadSystem.Instance.gameData;

        //ハンガー取得有無
        //if (gameData.isGetHanger)
        //    Hanger.SetActive(false);



        //保有アイテム
        if (gameData.getItems == "")
            return;
        string[] arr = gameData.getItems.Split(';');
        foreach (var item in arr)
        {
            if (item != "")
                ItemManager.Instance.loadItem(item);
        }
    }


    //<summary>
    //ゲーム進捗の算出
    //<summary>
    public int CheckProgress()
    {
        //進捗
        int progress = 1;
        //インスタンスを代入(ソース短縮化のため)
        gameData = SaveLoadSystem.Instance.gameData;

        //進捗算出
        //if (!gameData.isSetHanger)
        //    //step1 ハンガーセットしたか
        //    progress = 1;
        //else if (!gameData.isClearHanger)
        //    //step2 ハンガー4つの回転
        //    progress = 2;
        //else if (!gameData.isSetStraw)
        //    //step3 ストロー挿したか
        //    progress = 3;
        //else if (!gameData.isClearCop)
        //    //step4 コップ回転 
        //    progress = 4;
        //else if (!gameData.isClearTambarin)
        //    //step5 タンバリン並べ
        //    progress = 5;
        //else if (!gameData.isClearRimocon)
        //    //step7 エアコンのリモコン 
        //    progress = 7;
        //else if (!gameData.isClearDenmokuRock)
        //    //step8 デンモクロック解除 
        //    progress = 8;
        //else if (!gameData.isSendStarPower)
        //    //step9 星の力送信
        //    progress = 9;
        //else if (!gameData.isSendStepStep)
        //    //step10 1歩1歩送信
        //    progress = 10;
        //else if (!gameData.isSendLovers)
        //    //step11 九州Lovers送信
        //    progress = 11;
        //else if (!gameData.isClearMachine)
        //    //step12 カラオケ機のボタン
        //    progress = 12;
        //else if (!gameData.isClearOrder)
        //    //step13 1000円注文
        //    progress = 13;
        //else if (!gameData.isClearPhone)
        //    //step14 電話裏のボタン
        //    progress = 14;
        //else if (!gameData.isClearPowerOn)
        //    //step16 デンモクロック再起動
        //    progress = 16;
        //else if (!gameData.isClearDenmokuSlide)
        //    //step17 デンモクロック解除２回目
        //    progress = 17;
        //else if (!gameData.isGetKey2)
        //    //step18 ドライバーで鍵箱開ける
        //    progress = 18;
        //else if (!gameData.isSendKosho)
        //    //step20 こしょう送信 
        //    progress = 20;
        //else if (!gameData.isClearPentagon)
        //    //step21 五角形
        //    progress = 21;
        //else if (!gameData.isClearFinalBtn)
        //    //step22 最後の扉のボタン
        //    progress = 22;
        //else
        //    //あとは脱出するだけ
        //    progress = 23;

        return progress;
    }

}
