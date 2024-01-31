using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleController : MonoBehaviour
{
	[SerializeField]
	Transform	TrLeft;
	[SerializeField]
	Transform	TrRight;

	[SerializeField]
	float	angle;
	Vector3	angle3;

	private void Start()
	{
		angle3 = Vector3.zero;
	}

	private void Update()
	{
		angle3.z = angle;
		transform.localEulerAngles = angle3;
		angle3.z = -angle3.z;
		TrLeft.localEulerAngles = angle3;
		TrRight.localEulerAngles = angle3;
	}


	public	void	ChangeAngle( float newAngle )
	{
		angle = newAngle;
	}
}
