using System;
using UnityEngine;

public class CatchUtils
{
	public Vector2 FinalPosistion(Vector2 _curTargetPoint, Vector2 curPoint, float speed)
	{
		if (_curTargetPoint == curPoint)
		{
			return _curTargetPoint;
		}
		Vector2 vector = _curTargetPoint - curPoint;
		float num = Mathf.Abs(Vector2.Distance(_curTargetPoint, curPoint));
		float num2 = 0f;
		float num3 = 0f;
		if (num <= speed * Time.deltaTime)
		{
			num2 = _curTargetPoint.x;
			num3 = _curTargetPoint.y;
		}
		else
		{
			int num4 = 0;
			int num5 = 0;
			num4 = ((vector.x != 0f) ? ((vector.x > 0f) ? 1 : (-1)) : 0);
			num5 = ((vector.y != 0f) ? ((vector.y > 0f) ? 1 : (-1)) : 0);
			num2 = ((!(Mathf.Abs(curPoint.x - _curTargetPoint.x) < speed * Time.deltaTime)) ? (curPoint.x + speed * Time.deltaTime * (float)num4) : _curTargetPoint.x);
			num3 = ((!(Mathf.Abs(curPoint.y - _curTargetPoint.y) < speed * Time.deltaTime)) ? (curPoint.y + speed * Time.deltaTime * (float)num5) : _curTargetPoint.y);
		}
		return new Vector2((float)Math.Round(num2, 2), (float)Math.Round(num3, 2));
	}
}
