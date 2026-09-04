using System;
using UnityEngine;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class RotateObject : MonoBehaviour
	{
		public float rotSpeed = 2f;

		private void OnMouseDrag()
		{
			float num = Input.GetAxis("Mouse X") * rotSpeed * ((float)Math.PI / 180f);
			base.transform.Rotate(Vector3.up, 0f - num);
		}
	}
}
