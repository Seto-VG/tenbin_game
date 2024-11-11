using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonBehavior<StageManager>
{
    [SerializeField] private List<GameObject> _stagePrefabs = default;
    private GameObject _stage = default;
    // ステージ数取得
    public int GetStageMaxCount()
    {
        return _stagePrefabs.Count;
    }

    // ステージIdを取得
    public int GetStageId()
    {
        return GameData.stageId;
    }

    // ステージIdを設定
    public void SetStageId(int stageId)
    {
        GameData.stageId = Mathf.Clamp(stageId, 0, GetStageMaxCount() - 1);
    }

    // ステージを次に進める
    public void SetNextStage()
    {
        SetStageId(GetStageId() + 1);
    }

    // ラストステージ判定
    public bool IsLastStage()
    {
        return GetStageId() == GetStageMaxCount() - 1;
    }

    // ステージ生成
    public void CreateStage(int stageId)
    {
        stageId = Mathf.Clamp(stageId, 0, GetStageMaxCount() - 1);

        // ステージの生成
        _stage = Instantiate(_stagePrefabs[stageId], Vector3.zero, Quaternion.identity);

    }

    // 現在のステージを生成
    public void CreateCurrentStage()
    {
        CreateStage(GetStageId());
    }
}
