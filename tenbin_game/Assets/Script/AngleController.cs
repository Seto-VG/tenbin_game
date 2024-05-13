using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AngleController : MonoBehaviour
{
	[SerializeField]
	Transform _TrLeft;
	[SerializeField]
	Transform _TrRight;

	[SerializeField]
	float _angle; // 角度初期値：-25度
	Vector3 _angle3;
	float MAX_ANGLE = 25f; // 左最大傾き角度：-25度	右最大傾き角度：25度

	[SerializeField]
	float _itemWeight; // 課題物の重さ
	public float sumWeight; // 重りの合計の重さ
	[SerializeField]
	Collider _weightDetectionArea; // 重量検知エリア
	[SerializeField]
	private List<GameObject> _weightObjList;
	public Dictionary<GameObject, float> _weightInfoMap = new Dictionary<GameObject, float>(); // 重りとその重量のマップ
	[Header("ONにするとsumWeightがリセットされる(デバッグ用)")]
	[SerializeField]
	bool resetFlag = false;

	private void Start()
	{
		_angle3 = Vector3.zero;
		_angle = -25;
	}

	private void Update()
	{
		// 天秤の角度の調整
		_angle3.z = _angle;
		transform.localEulerAngles = _angle3;
		_angle3.z = -_angle3.z;
		_TrLeft.localEulerAngles = _angle3;
		_TrRight.localEulerAngles = _angle3;

		// 角度の限界値を設定
		if (_angle > MAX_ANGLE)
		{
			_angle = MAX_ANGLE;
		}
		if (_angle < -MAX_ANGLE)
		{
			_angle = -MAX_ANGLE;
		}

		if (resetFlag)
		{
			resetFlag = false;
			sumWeight = -25;
		}
	}

	// 角度の変更
	public void ChangeAngle()
	{
		_angle3.z = _angle;
		transform.localEulerAngles = _angle3;
		_angle3.z = -_angle3.z;
		_TrLeft.localEulerAngles = _angle3;
		_TrRight.localEulerAngles = _angle3;

		if (sumWeight == 0) return;

		float _beforeAngle = _angle;
		// 割合の小数第三位は偶数丸めを使用
		float roundedNumber = Mathf.Round(sumWeight / _itemWeight * 100f) / 100f; // 割合 小数第三位まで四捨五入
		if (Mathf.Abs(roundedNumber * 100f - Mathf.Floor(roundedNumber * 100f) * 100f) == 0.5f) // 小数第三位が0.5の場合
		{
			if (Mathf.Floor(roundedNumber * 100f) % 2 != 0) // 偶数でない場合
			{
				roundedNumber -= 0.01f; // 小数第三位を奇数から偶数に変更
			}
		}

		// (sum > item) Angle = 割合 x 25
		if (sumWeight >= _itemWeight)
		{
			roundedNumber -= 1;
			_angle = roundedNumber * MAX_ANGLE;
			DOTween.To(() => _beforeAngle, (x) => _angle = x, _angle, 2);
		}
		// (sum < item) Angle = 割合 x 25 - 25
		else
		{
			_angle = roundedNumber * MAX_ANGLE - MAX_ANGLE;
			DOTween.To(() => _beforeAngle, (x) => _angle = x, _angle, 2);
		}
	}

	// TODO DOTweenによる値の変更
	public void ChangeValueWithDOTween(float value)
	{
		
	}

	// 重りの登録
	public void RegisterWeights(GameObject other, float weight)
	{
		// 重りと重さをマップに追加する
		if (!_weightInfoMap.ContainsKey(other))
		{
			_weightInfoMap.Add(other, weight);
			Debug.Log(_weightInfoMap[other]);
		}
	}

	// 合計重量に指定された重りの重量を追加
	public void TryAddWeight(GameObject other)
	{
		if (!_weightObjList.Contains(other))
		{
			sumWeight += _weightInfoMap[other];
			ChangeAngle();
		}
	}

	// 重りの登録解除と重量削除
	public void RequestUnregisterWeights(GameObject other)
	{
		// オブジェクトBとその情報をマップから削除する
		_weightInfoMap.Remove(other);
		// TODO ChangeAngle 角度を変える
	}
}
