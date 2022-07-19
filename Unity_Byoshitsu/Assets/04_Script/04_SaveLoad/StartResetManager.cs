using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartResetManager : MonoBehaviour
{
    private GameData gameData;


    //アイテムオブジェクト**********************



    //ゲーム内オブジェクト**********************
    public TVCardMacine_Tap TVCardMacineClass;
    public TVCardInsert_Tap TVCardInsertClass;
    public BalanceBefore_Tap BalanceBeforeClass;
    public Balance_Judge BalanceClass;
    public Doll_Judge DollClass;
    public Doll_Tap[] DollTapArray;
    public CurtainRope_Tap CurtainRopeClass;
    public PC_Judge PCClass;
    public Shelf5Slide_Tap ShelfSlideClass;
    public Dial_Judge DialClass;
    public NurseCurt_Tap NurseCurtClass;
    public SetWaterTunk_Tap SetTunkClass;
    public WaterServer_Judge WaterSeverClass;
    public Star_Judge StarClass;
    public Name_Judge NameClass;
    public Puzzle_Judge Puzzle31;
    public Curtain_Judge CurtainClass;
    public Puzzle_Judge Puzzle8;
    public Tenkey_Judge TenkeyClass;
    public Door_Tap DoorClass;
    public ClearManager ClearClass;
    

    //<summary>
    //タイトル画面の「はじめから」の時
    //<summary>
    public void GameStart()
    {
        SaveLoadSystem.Instance.GameStart();
    }


    //<summary>
    //メニュー画面の「タイトルへ」の時
    //<summary>
    public void ResetObject()
    {
        //1.手洗いの箱 (未開封薬)

        //2.未開封→薬
        //なし

        //3.Shelf1 (千円)

        //4.TVカード販売機 (TVカード)
        TVCardMacineClass.TVCard.SetActive(false);

        //5.TVカード挿入
        TVCardInsertClass.TVScreen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/90_Other/TVBlack");

        //6.Chair1の箱 (薬3つ)

        //7.クロスワード
        //なし

        //8.クマ (天秤の皿)

        //9.天秤に皿セット
        BalanceBeforeClass.BalanceBefore.SetActive(true);
        BalanceBeforeClass.RightPlate.SetActive(false);
        BalanceBeforeClass.Balance.SetActive(false);

        //10.天秤で薬の計量
        BalanceClass.Status = "00";
        foreach (var obj in BalanceClass.Tenbins)
            obj.SetActive(false);

        foreach(var obj in BalanceClass.MedicinesLeft)
        {
            foreach (var Medicine in obj)
                Medicine.SetActive(false);
        }

        //11.人形に薬セット (グラスとハサミ)
        DollClass.Status = "0000";
        DollClass.isClear = false;
        DollClass.CloseDoor.SetActive(true);
        DollClass.OpenDoor.SetActive(false);
        DollClass.Sciccers.SetActive(true);
        DollClass.Glass.SetActive(false);

        foreach(var obj in DollTapArray)
        {
            foreach (var Medicine in obj.Medicines)
                Medicine.SetActive(false);
        }

        //12.ハサミでカーテン紐をきる
        CurtainRopeClass.CurtainBefore.SetActive(true);
        CurtainRopeClass.CurtainAfter.SetActive(false);

        //13.PCロック解除
        PCClass.InputCode = "";
        PCClass.isClear = false;
        PCClass.ScreenPSW.SetActive(true);
        PCClass.ScreenTime.SetActive(false);
        PCClass.Msg.SetActive(false);
        foreach(var obj in PCClass.Mojis)
            obj.GetComponent<SpriteRenderer>().sprite = null;

        //14.Shelf5のダイアル錠 (タンクと鍵1)
        ShelfSlideClass.CloseSlide.SetActive(true);
        ShelfSlideClass.OpenSlide.SetActive(false);

        DialClass.isClear = false;
        DialClass.Status = "000";
        foreach (var obj in DialClass.BtnLeft)
            obj.SetActive(false);
        foreach (var obj in DialClass.BtnCenter)
            obj.SetActive(false);
        foreach (var obj in DialClass.BtnRight)
            obj.SetActive(false);
        DialClass.BtnLeft[0].SetActive(true);
        DialClass.BtnCenter[0].SetActive(true);
        DialClass.BtnRight[0].SetActive(true);

        DialClass.CloseDoor.SetActive(true);
        DialClass.OpenDoor.SetActive(false);
        DialClass.WaterTunk.SetActive(false);
        DialClass.Key1.SetActive(false);

        //15.ナースカートの引出し (体温計)
        NurseCurtClass.CloseSlide.SetActive(true);
        NurseCurtClass.OpenSlide.SetActive(false);
        NurseCurtClass.Taionkei.SetActive(true);

        //16.コップの中を捨てる
        //なし

        //17.タンクをセット
        SetTunkClass.isClear = false;
        SetTunkClass.WaterTunk.SetActive(false);

        //18.コップに水・お湯を入れる
        WaterSeverClass.GlassStatus = 3;
        WaterSeverClass.HotStatus = 0;
        WaterSeverClass.ColdStatus = 3;

        //19.体温計で計測
        ItemManager.Instance.TaionkeiName = "T_0";

        //20.Shelf2の星型 (Block1~3)
        StarClass.Status = "000000";
        StarClass.CloseDoor.SetActive(true);
        StarClass.OpenDoor.SetActive(false);
        StarClass.Block1.SetActive(false);
        StarClass.Block2.SetActive(false);
        StarClass.Block3.SetActive(false);

        //21.Name2のフルカワ (Block4)
        NameClass.Status = "0000";
        for(int i = 0; i < NameClass.Btn1.Length; i++)
        {
            NameClass.Btn1[i].SetActive(false);
            NameClass.Btn2[i].SetActive(false);
            NameClass.Btn3[i].SetActive(false);
            NameClass.Btn4[i].SetActive(false);
        }
        NameClass.Btn1[0].SetActive(true);
        NameClass.Btn2[0].SetActive(true);
        NameClass.Btn3[0].SetActive(true);
        NameClass.Btn4[0].SetActive(true);

        NameClass.CloseDoor.SetActive(true);
        NameClass.OpenDoor.SetActive(false);
        NameClass.Block.SetActive(false);

        NameClass.TablePlate.SetActive(true);
        NameClass.Puzzle31.SetActive(false);

        //22.パズル31
        Puzzle31.isClear = false;
        Puzzle31.Status = "000000000000000000";
        foreach (var obj in Puzzle31.Blocks)
            obj.SetActive(false);
        foreach (var obj in Puzzle31.Colliders)
            obj.SetActive(true);

        //23.カーテンで31
        CurtainClass.isClear = false;
        CurtainClass.Status = "0000002000";

        CurtainClass.Curtains1[0].SetActive(true);
        CurtainClass.Curtains2[0].SetActive(true);
        CurtainClass.Curtains3[0].SetActive(true);
        CurtainClass.Curtains4[0].SetActive(true);
        CurtainClass.Curtains5[0].SetActive(true);
        CurtainClass.Curtains6[0].SetActive(true);
        CurtainClass.Curtains7[0].SetActive(false); //紐
        CurtainClass.Curtains8[0].SetActive(true);
        CurtainClass.Curtains9[0].SetActive(true);
        CurtainClass.Curtains10[0].SetActive(true);

        CurtainClass.Curtains1[1].SetActive(false);
        CurtainClass.Curtains2[1].SetActive(false);
        CurtainClass.Curtains3[1].SetActive(false);
        CurtainClass.Curtains4[1].SetActive(false);
        CurtainClass.Curtains5[1].SetActive(false);
        CurtainClass.Curtains6[1].SetActive(false);
        CurtainClass.Curtains7[1].SetActive(false);
        CurtainClass.Curtains8[1].SetActive(false);
        CurtainClass.Curtains9[1].SetActive(false);
        CurtainClass.Curtains10[1].SetActive(false);

        CurtainClass.TVScreen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/90_Other/TVBlack");

        //24.クロスワード
        //なし

        //25.人形を倒す
        foreach(var Doll in DollTapArray)
        {
            Doll.Stand.SetActive(true);
            Doll.Down.SetActive(false);
        }

        //26.くま

        //27.パズル-8
        Puzzle8.isClear = false;
        Puzzle8.Status = "000000000000000000";
        foreach (var obj in Puzzle8.Blocks)
            obj.SetActive(false);
        foreach (var obj in Puzzle8.Colliders)
            obj.SetActive(true);

        Puzzle8.Plate8.SetActive(true);
        Puzzle8.Tenkey.SetActive(false);

        //28.テンキーで60% (鍵2)
        TenkeyClass.Status = "";
        foreach(var obj in TenkeyClass.Images)
            obj.GetComponent<SpriteRenderer>().sprite = null;

        TenkeyClass.Before.SetActive(true);
        TenkeyClass.After.SetActive(false);
        TenkeyClass.Key2.SetActive(false);

        //29脱出
        DoorClass.CloseDoor.SetActive(true);
        DoorClass.OpenDoor.SetActive(false);

        //エンディング
        ClearClass.White1.SetActive(true);
        ClearClass.White1.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.0f);
        ClearClass.White2.SetActive(false);
        ClearClass.White2.GetComponent<Image>().color = new Color(255f, 255f, 255f, 1.0f); 

        ClearClass.ClearImage.SetActive(false);
        ClearClass.ClearImage.transform.localScale = new Vector3(0, 0, 1);

        ClearClass.ToOtherApp.SetActive(false);
        ClearClass.ToOtherApp.GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.0f);

        //アイテムリセット
        foreach (var obj in ItemManager.Instance.getItemsArray)
        {
            obj.GetComponent<Outline>().enabled = false;
            obj.GetComponent<Image>().sprite = null;
            obj.SetActive(false);
        }
        ItemManager.Instance.SelectItem = "";
        ItemManager.Instance.ItemPanel.SetActive(false);

    }


    //***************************************************************************
    //<summary>
    //タイトル画面の「続きから」の時
    //<summary>
    public void GameContinue()
    {
        gameData = SaveLoadSystem.Instance.gameData;


        //1.箱と未開封薬

        //2.未開封→薬
        //なし

        //3.Shelf1と千円

        //4.TVカード販売機
        if(gameData.isClearMoney && !gameData.isGetTVCard)
            TVCardMacineClass.TVCard.SetActive(true);

        //5.TVカード挿入
        if(gameData.isClearTVCard)
            TVCardInsertClass.TVScreen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/90_Other/TV");

        //6.Chair1の箱 (薬3つ)

        //7.クロスワード
        //なし

        //8.クマ (天秤の皿)

        //9.天秤に皿セット
        if (gameData.isClearBalance)
            BalanceBeforeClass.BalanceBefore.SetActive(false);

        //10.天秤で薬の計量
        BalanceClass.Status = gameData.MedicineStatus;
        if(gameData.TenbinStatus != -1)
        {
            //天秤自体
            BalanceClass.Tenbins[gameData.TenbinStatus].SetActive(true);
            //左皿
            int LeftStatus = int.Parse(BalanceClass.Status.Substring(0, 1));
            if (LeftStatus != 0)
                BalanceClass.MedicinesLeft[gameData.TenbinStatus][LeftStatus - 1].SetActive(true);
            //右皿
            int RightStatus = int.Parse(BalanceClass.Status.Substring(1));
            if (RightStatus != 0)
                BalanceClass.MedicinesRight[gameData.TenbinStatus][RightStatus - 1].SetActive(true);
        }

        //11.人形に薬セット (グラスとハサミ)
        DollClass.Status = gameData.DollStatus;
        DollClass.isClear = gameData.isClearDoll;

        for(int i = 0; i < 4; i++)
        {
            int status = int.Parse(DollClass.Status.Substring(i, 1));
            if (status != 0)
                DollTapArray[i].Medicines[status - 1].SetActive(true);
        }

        if (DollClass.isClear)
        {
            DollClass.CloseDoor.SetActive(false);
            DollClass.OpenDoor.SetActive(true);
            DollClass.Glass.SetActive(true);
        }

        if(gameData.isGetGlass)
            DollClass.Glass.SetActive(false);
        if (gameData.isGetSciccers)
            DollClass.Sciccers.SetActive(false);


        //12.ハサミでカーテン紐をきる
        if (gameData.isClearSciccers)
            CurtainRopeClass.CurtainBefore.SetActive(false);
            CurtainRopeClass.CurtainAfter.SetActive(true);

        //13.PCロック解除
        PCClass.isClear = gameData.isClearPC;
        if (PCClass.isClear)
        {
            PCClass.ScreenPSW.SetActive(false);
            PCClass.ScreenTime.SetActive(true);
        }

        //14.Shelf5のダイアル錠 (タンクと鍵1)
        DialClass.isClear = gameData.isClearDial;
        if (DialClass.isClear)
        {
            DialClass.CloseDoor.SetActive(false);
            DialClass.OpenDoor.SetActive(true);
            DialClass.WaterTunk.SetActive(true);
            DialClass.Key1.SetActive(true);
        }

        if(gameData.isGetWaterTunk)
            DialClass.WaterTunk.SetActive(false);
        if (gameData.isGetKey1)
            DialClass.Key1.SetActive(false);

        //15.ナースカートの引出し (体温計)
        if(gameData.isClearNurseCurt)
        {
            NurseCurtClass.CloseSlide.SetActive(false);
            NurseCurtClass.OpenSlide.SetActive(true);
        }

        if(gameData.isGetTaionkei)
            NurseCurtClass.Taionkei.SetActive(false);


        //16.コップの中を捨てる
        //なし

        //17.タンクをセット
        SetTunkClass.isClear = gameData.isClearWaterTunk;
        if(SetTunkClass.isClear)
            SetTunkClass.WaterTunk.SetActive(true);

        //18.コップに水・お湯を入れる
        WaterSeverClass.GlassStatus = gameData.GlassStatus;
        WaterSeverClass.HotStatus = gameData.HotStatus;
        WaterSeverClass.ColdStatus = gameData.ColdStatus;

        //19.体温計で計測
        ItemManager.Instance.TaionkeiName = gameData.TaionkeiStatus;

        //20.Shelf2の星型　(Block1~3)
        if (gameData.isClearStar)
        {
            StarClass.CloseDoor.SetActive(false);
            StarClass.OpenDoor.SetActive(true);
            StarClass.Block1.SetActive(true);
            StarClass.Block2.SetActive(true);
            StarClass.Block3.SetActive(true);
        }

        if(gameData.isGetBlock1)
            StarClass.Block1.SetActive(false);
        if (gameData.isGetBlock2)
            StarClass.Block2.SetActive(false);
        if (gameData.isGetBlock3)
            StarClass.Block3.SetActive(false);

        //21.Name2のフルカワ (Block4)
        if (gameData.isClearName)
        {
            NameClass.CloseDoor.SetActive(false);
            NameClass.OpenDoor.SetActive(true);
            NameClass.Block.SetActive(true);
            NameClass.TablePlate.SetActive(false);
            NameClass.Puzzle31.SetActive(true);
        }
        if(gameData.isGetBlock4)
            NameClass.Block.SetActive(false);

        //22.パズル31
        Puzzle31.isClear = gameData.isClearPuzzle31;
        Puzzle31.Status = gameData.Puzzle31Status;
        Puzzle31.Restrat();

        //23.カーテンで31
        CurtainClass.Status = gameData.CurtainStatus;
        CurtainClass.isClear = gameData.isClearCurtain31;

        if(CurtainClass.Status.Substring(0,1) == "1")
        {
            CurtainClass.Curtains1[0].SetActive(false);
            CurtainClass.Curtains1[1].SetActive(true);
        }
        if (CurtainClass.Status.Substring(1, 1) == "1")
        {
            CurtainClass.Curtains2[0].SetActive(false);
            CurtainClass.Curtains2[1].SetActive(true);
        }
        if (CurtainClass.Status.Substring(2, 1) == "1")
        {
            CurtainClass.Curtains3[0].SetActive(false);
            CurtainClass.Curtains3[1].SetActive(true);
        }
        if (CurtainClass.Status.Substring(3, 1) == "1")
        {
            CurtainClass.Curtains4[0].SetActive(false);
            CurtainClass.Curtains4[1].SetActive(true);
        }
        if (CurtainClass.Status.Substring(4, 1) == "1")
        {
            CurtainClass.Curtains5[0].SetActive(false);
            CurtainClass.Curtains5[1].SetActive(true);
        }
        if (CurtainClass.Status.Substring(5, 1) == "1")
        {
            CurtainClass.Curtains6[0].SetActive(false);
            CurtainClass.Curtains6[1].SetActive(true);
        }
        if (CurtainClass.Status.Substring(6, 1) == "1")
        {
            CurtainClass.Curtains7[0].SetActive(false);
            CurtainClass.Curtains7[1].SetActive(true);
        }
        if (CurtainClass.Status.Substring(7, 1) == "1")
        {
            CurtainClass.Curtains8[0].SetActive(false);
            CurtainClass.Curtains8[1].SetActive(true);
        }
        if (CurtainClass.Status.Substring(8, 1) == "1")
        {
            CurtainClass.Curtains9[0].SetActive(false);
            CurtainClass.Curtains9[1].SetActive(true);
        }
        if (CurtainClass.Status.Substring(9, 1) == "1")
        {
            CurtainClass.Curtains10[0].SetActive(false);
            CurtainClass.Curtains10[1].SetActive(true);
        }

        if (CurtainClass.isClear)
            CurtainClass.TVScreen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/90_Other/TV2");


        //24.クロスワード
        //なし

        //25.人形を倒す
        if (gameData.isDown1)
        {
            DollTapArray[0].Stand.SetActive(false);
            DollTapArray[0].Down.SetActive(true);
        }
        if (gameData.isDown2)
        {
            DollTapArray[1].Stand.SetActive(false);
            DollTapArray[1].Down.SetActive(true);
        }
        if (gameData.isDown3)
        {
            DollTapArray[2].Stand.SetActive(false);
            DollTapArray[2].Down.SetActive(true);
        }
        if (gameData.isDown4)
        {
            DollTapArray[3].Stand.SetActive(false);
            DollTapArray[3].Down.SetActive(true);
        }

        //26.くま

        //27.パズル-8
        Puzzle8.isClear = gameData.isClearPuzzle8;
        Puzzle8.Status = gameData.Puzzle8Status;
        Puzzle8.Restrat();

        if (Puzzle8.isClear)
        {
            Puzzle8.Plate8.SetActive(false);
            Puzzle8.Tenkey.SetActive(true);
        }

        //28.テンキーで60% (鍵2)
        if (gameData.isClearTenkey)
        {
            TenkeyClass.Before.SetActive(false);
            TenkeyClass.After.SetActive(true);
            TenkeyClass.Key2.SetActive(true);
        }

        if(gameData.isGetKey2)
            TenkeyClass.Key2.SetActive(false);

        //29脱出
        if (gameData.isClearAll)
            gameData.getItems += "Key2;";

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


    //***************************************************************************
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
