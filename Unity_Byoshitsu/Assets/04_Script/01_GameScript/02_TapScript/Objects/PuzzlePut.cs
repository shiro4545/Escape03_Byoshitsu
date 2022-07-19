using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePut : PuzzleTapCollider
{
    //場所
    public int PositionNo;

    //Judgeクラス
    public Puzzle_Judge JudgeClass;

    //ボタンタップ時
    protected override void OnTap()
    {
        base.OnTap();

        JudgeClass.PutBlock(PositionNo);
    }
}
