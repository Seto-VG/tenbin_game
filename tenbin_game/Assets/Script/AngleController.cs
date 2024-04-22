
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class AngleController : MonoBehaviour
{
	[SerializeField]
	Transform _TrLeft;
	[SerializeField]
	Transform _TrRight;

	[SerializeField]
	float _angle = -25f;
	Vector3	_angle3;
	float MAX_ANGLE = 25f;

	[SerializeField]
	float _itemWeight; // 課題物の重さ
	public float sumWeight; // 重りの合計の重さ
	[SerializeField]
	Collider _weightDetectionArea; // 重量検知エリア
	[SerializeField]
	List<GameObject> _weightList = new List<GameObject>(); // 検知している重りのリスト
	[Header("ONにするとsumWeightがリセットされる(デバッグ用)")]
	[SerializeField]
	bool resetFlag = false;

	private void Start()
	{
		_angle3 = Vector3.zero;
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

		ChangeAngle();
		if (resetFlag)
		{
			resetFlag = false;
			sumWeight = -25;
		}
	}

	// public void ChangeAngle( float newAngle )
	// {
	// 	_angle = newAngle;
	// }
	public void ChangeAngle()
	{
		if (sumWeight == 0) return;
		float ratio = sumWeight / _itemWeight;
		if (sumWeight >= _itemWeight)
		{
			ratio -= 1;
			_angle = ratio * MAX_ANGLE;
		}
		else
		{
			_angle = ratio * MAX_ANGLE - MAX_ANGLE;
			Debug.Log(_angle);
		}
	}

	public void EnterAreaObj(GameObject gameObject)
	{
		_weightList.Add(gameObject);
	}

	public void ExitAreaObj(GameObject gameObject)
	{
		_weightList.Remove(gameObject);
	}
}
