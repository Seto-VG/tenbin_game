
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleController : MonoBehaviour
{
	[SerializeField]
	Transform _TrLeft;
	[SerializeField]
	Transform _TrRight;

	[SerializeField]
	float _angle;
	Vector3	_angle3;

	[SerializeField]
	float _itemWeight; // 課題物の重さ
	[SerializeField]
	Collider _weightDetectionArea; // 重量検知エリア
	[SerializeField]
	List<GameObject> _weightList = new List<GameObject>(); // 検知している重りのリスト

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
		if (_angle > 25f)
		{
			_angle = 25f;
		}
		if (_angle < -25f)
		{
			_angle = -25f;
		}
	}

	public void ChangeAngle( float newAngle )
	{
		_angle = newAngle;
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
