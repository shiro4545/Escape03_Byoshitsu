using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleGet : TapCollider
{
    public int BlockNo;

    public int PositionNo;

    //Judgeクラス
    public Puzzle_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        ItemManager.Instance.GetItem("Block" + BlockNo);
        this.gameObject.SetActive(false);

        JudgeClass.GetBlock(PositionNo);
    }

}
