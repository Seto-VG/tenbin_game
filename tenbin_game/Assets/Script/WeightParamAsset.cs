using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeightData
{
    public string weightName;
    public float weight;
}

[CreateAssetMenu(fileName = "WeightData", menuName = "ScriptableObjects/WeightParam")]
public class WeightParamAsset : ScriptableObject
{
    public List<WeightData> DataList;
}