using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateEnemyParamAsset")]
public class WeightParamAsset : ScriptableObject
{
    public string weightName;

    [SerializeField]
    int _weight;
}