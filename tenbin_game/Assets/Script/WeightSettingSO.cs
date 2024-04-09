using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeightData
{
    public string Id;
    public float Weight;
}

[CreateAssetMenu(fileName = "WeightData", menuName = "ScriptableObject/WeightDataSO")]
public class WeightSetting : ScriptableObject
{
    public List<WeightData> DataList;
}