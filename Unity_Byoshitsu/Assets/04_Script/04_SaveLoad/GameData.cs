using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    //所有アイテム
    public string getItems = "";

    //アイテム取得有無
    public bool isGetHanger = false;
    public bool isGetStraw = false;
    public bool isGetTanbarine_Maru = false;
    public bool isGetTanbarine_Sankaku = false;
    public bool isGetTanbarine_Shikaku = false;
    public bool isGetKey1 = false;
    public bool isGetKeyBox = false;
    public bool isGetDriver = false;
    public bool isGetPiece1 = false;
    public bool isGetPiece2 = false;
    public bool isGetPiece3 = false;
    public bool isGetPiece4 = false;
    public bool isGetPiece5 = false;
    public bool isGetKey3 = false;

    //謎クリア有無
    public bool isSetHanger = false;
    public bool isClearHanger = false;
    public bool isSetStraw = false;
    public bool isClearCop = false;
    public bool isClearTambarin = false;
    public bool isClearRimocon = false;
    public bool isClearDenmokuRock = false;
    public bool isSendStarPower = false;
    public bool isSendStepStep = false;
    public bool isSendLovers = false;
    public bool isClearMachine = false;
    public bool isClearOrder = false;
    public bool isClearPhone = false;
    public bool isClearPowerOn = false;
    public bool isClearDenmokuSlide = false;
    public bool isGetKey2 = false;
    public bool isClearDoor = false; //鍵を使ったか
    public bool isFullOpen = false; //1度でも最初の扉を全開にしたか
    public bool isSendKosho = false;
    public bool isClearPentagon = false;
    public bool isClearFinalBtn = false;
    public bool isClearAll = false;


    //オブジェクト状態
    public string HangerStatus = "0000";
    public bool isChangePage = false;
    public string CopStatus = "03";
    public int DeskUpperStatus = 0;
    public int DeskUnderStatus = 0;
    public string TambarinStatus = "001";
    public bool isOpenShelf = false;
    public int DenmokuStatus = 1; //0:電源off,1:ロック状態,2:ロックなし 
    public int DoorStatus = 0; //0:全閉,1:全開,2:ちょい開け
    public string PentagonStatus = "00000"; //0:ピースなし,1~5:それぞれのピースが置かれている

    //各ヒントの数と動画視聴有無(0:未視聴,1:視聴済み)
    public string[] HintFlgArray = new string[] {
        "0",     //ダミー
        "0000",  //1
        "0000",
        "0000",
        "0000",
        "0000",  //5
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //10
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //15
        "0000",
        "0000", 
        "0000",
        "0000",
        "0000",  //20
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //25
        "0000",
        "0000",
        "0000",
        "0000",
        "0000",  //30
        "0000",
    };
}
