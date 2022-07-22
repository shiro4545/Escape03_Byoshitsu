using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartResetManager : MonoBehaviour
{
    private GameData gameData;


    //ゲーム内オブジェクト**********************
    public Box2_Judge Box2Class;
    public Box2_Tap[] Box2Tap;
    public Shelf1_Judge Shelf1Class;
    public Shelf1_Tap[] Shelf1Tap;
    public TVCardMacine_Tap TVCardMacineClass;
    public TVCardInsert_Tap TVCardInsertClass;
    public Box_Judge BoxClass;
    public Bear_Judge BearClass;
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
        Box2Class.InputNo = "0000";
        Box2Class.CloseCover.SetActive(true);
        Box2Class.OpenCover.SetActive(false);

        foreach(var Tap in Box2Tap)
        {
            Tap.Index = 0;
            foreach (var obj in Tap.Objects)
                obj.SetActive(false);
            Tap.Objects[0].SetActive(true);
        }

        Box2Class.MedicinePack.SetActive(false);

        //2.未開封→薬
        //なし

        //3.Shelf1 (千円)
        Shelf1Class.InputNo = "0000";
        Shelf1Class.CloseDoor.SetActive(true);
        Shelf1Class.OpenDoor.SetActive(false);

        foreach (var Tap in Shelf1Tap)
        {
            Tap.Index = 0;
            foreach (var obj in Tap.Objects)
                obj.SetActive(false);
            Tap.Objects[0].SetActive(true);
        }

        Shelf1Class.Money.SetActive(false);

        //4.TVカード販売機 (TVカード)
        TVCardMacineClass.TVCard.SetActive(false);

        //5.TVカード挿入
        TVCardInsertClass.TVScreen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/90_Other/TVBlack");

        //6.Chair1の箱 (薬3つ)
        BoxClass.Status = "000000";
        BoxClass.CloseDoor.SetActive(true);
        BoxClass.OpenDoor.SetActive(false);
        for (int i = 0; i < 5; i++)
        {
            BoxClass.Left1[i].SetActive(false);
            BoxClass.Left2[i].SetActive(false);
            BoxClass.Center1[i].SetActive(false);
            BoxClass.Center2[i].SetActive(false);
            BoxClass.Right1[i].SetActive(false);
            BoxClass.Right2[i].SetActive(false);
        }
        BoxClass.Left1[0].SetActive(true);
        BoxClass.Left2[0].SetActive(true);
        BoxClass.Center1[0].SetActive(true);
        BoxClass.Center2[0].SetActive(true);
        BoxClass.Right1[0].SetActive(true);
        BoxClass.Right2[0].SetActive(true);

        BoxClass.Medicine1.SetActive(false);
        BoxClass.Medicine2.SetActive(false);
        BoxClass.Medicine3.SetActive(false);

        //7.クロスワード
        //なし

        //8.クマ (天秤の皿)
        BearClass.isClear = false;
        BearClass.Status1 = "0000000";
        BearClass.Bear1[0].SetActive(true);
        BearClass.Bear2[0].SetActive(true);
        BearClass.Bear3[0].SetActive(true);
        BearClass.Bear1[1].SetActive(false);
        BearClass.Bear2[1].SetActive(false);
        BearClass.Bear3[1].SetActive(false);

        BearClass.Tenbin.SetActive(false);

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
        Puzzle31.Red.SetActive(false);
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
        BearClass.Status2 = "000000000";
        BearClass.Plate2.SetActive(true);
        BearClass.Puzzle8.SetActive(false);

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
        if (gameData.isClearBox2)
        {
            Box2Class.CloseCover.SetActive(false);
            Box2Class.OpenCover.SetActive(true);
            Box2Class.MedicinePack.SetActive(true);
        }

        if(gameData.isGetMedicinePack)
            Box2Class.MedicinePack.SetActive(false);

        //2.未開封→薬
        //なし

        //3.Shelf1と千円
        if (gameData.isClearShelf1)
        {
            Shelf1Class.CloseDoor.SetActive(false);
            Shelf1Class.OpenDoor.SetActive(true);
            Shelf1Class.Money.SetActive(true);
        }

        if(gameData.isGetMoney)
            Shelf1Class.Money.SetActive(false);


        //4.TVカード販売機
        if(gameData.isClearMoney && !gameData.isGetTVCard)
            TVCardMacineClass.TVCard.SetActive(true);

        //5.TVカード挿入
        if(gameData.isClearTVCard)
            TVCardInsertClass.TVScreen.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/90_Other/TV");

        //6.Chair1の箱 (薬3つ)
        if (gameData.isClearBox)
        {
            BoxClass.CloseDoor.SetActive(false);
            BoxClass.OpenDoor.SetActive(true);
            BoxClass.Medicine1.SetActive(true);
            BoxClass.Medicine2.SetActive(true);
            BoxClass.Medicine3.SetActive(true);
        }

        if(gameData.isGetMedicine1)
            BoxClass.Medicine1.SetActive(false);
        if (gameData.isGetMedicine2)
            BoxClass.Medicine2.SetActive(false);
        if (gameData.isGetMedicine3)
            BoxClass.Medicine3.SetActive(false);

        //7.クロスワード
        //なし

        //8.クマ (天秤の皿)
        BearClass.isClear = SaveLoadSystem.Instance.gameData.isClearBear;
        if (BearClass.isClear)
        {
            BearClass.Bear3[0].SetActive(false);
            BearClass.Bear3[1].SetActive(true);
            BearClass.Tenbin.SetActive(true);
        }

        if(gameData.isGetBalance)
            BearClass.Tenbin.SetActive(false);

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
        {
            CurtainRopeClass.CurtainBefore.SetActive(false);
            CurtainRopeClass.CurtainAfter.SetActive(true);
        }

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
        if (Puzzle31.isClear)
            Puzzle31.Red.SetActive(true);

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
        if (SaveLoadSystem.Instance.gameData.isClearBear2)
        {
            BearClass.Bear1[0].SetActive(false);
            BearClass.Bear1[1].SetActive(true);
            BearClass.Bear2[0].SetActive(false);
            BearClass.Bear2[1].SetActive(true);
            BearClass.Bear3[0].SetActive(false);
            BearClass.Bear3[1].SetActive(true);

            BearClass.Plate2.SetActive(false);
            BearClass.Puzzle8.SetActive(true);
        }

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
        if (!gameData.isClearBox2)
            //流しの箱 備品数
            progress = 1;
        else if (!gameData.isClearShelf1)
            //棚のボタン　柵の形
            progress = 3;
        else if (!gameData.isClearMoney)
            //千円でカード買う
            progress = 4;
        else if (!gameData.isClearTVCard)
            //TVカードを入れる
            progress = 5;
        else if (!gameData.isClearBox)
            //薬の色の箱
            progress = 6;
        else if (!gameData.isClearBear)
            //クロスワード　→　白熊７回
            progress = 8;
        else if (!gameData.isClearBalance)
            //天秤の皿をセット
            progress = 9;
        else if (!gameData.isClearDoll)
            //薬計量　→　人形に薬セット
            progress = 11;
        else if (!gameData.isClearSciccers)
            //ハサミでカーテン紐きる
            progress = 12;
        else if (!gameData.isClearPC)
            //PCのロック解除
            progress = 13;
        else if (!gameData.isClearDial)
            //朝食の時間ダイアル
            progress = 14;
        else if (!gameData.isClearNurseCurt)
            //ナースかーとの引き出し開ける
            progress = 15;
        else if (!gameData.isClearWaterTunk)
            //タンクをセット
            progress = 17;
        else if (!gameData.isClearTaionkei)
            //水入れ　→ 体温計で36ど
            progress = 19;
        else if (!gameData.isClearStar)
            //星型のボタン
            progress = 20;
        else if (!gameData.isClearName)
            //フルカワ
            progress = 21;
        else if (!gameData.isClearPuzzle31)
            //パズルで31に
            progress = 22;
        else if (!gameData.isClearCurtain31)
            //カーテンで31に
            progress = 23;
        else if (!gameData.isDown1 || !gameData.isDown2 && !gameData.isDown3 && !gameData.isDown4)
            //人形倒す
            progress = 25;
        else if (!gameData.isClearBear2)
            //くろ熊2 茶熊3
            progress = 26;
        else if (!gameData.isClearPuzzle8)
            //パズルで-8に
            progress = 27;
        else if (!gameData.isClearTenkey)
            //テンキーで60%
            progress = 28;
        else
            //あとは脱出するだけ
            progress = 29;

        return progress;
    }

}
