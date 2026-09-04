using UnityEngine;

public class SizeClipController : MonoBehaviour
{
	public enum NormalStatus
	{
		none = 0,
		up = 1,
		down = 2,
		left = 3,
		right = 4
	}

	public NormalStatus normalStatus;

	public Transform clipObjTrans;

	public MeshRenderer sizeRenderer;

	public MeshRenderer shortSizeRender_1;

	public MeshRenderer shortSizeRender_2;

	private Material clipMaterial;

	private Material clipMaterial2;

	private Material clipMaterial3;

	public Vector3 clipPos;

	private Vector3 clipNormal;

	[SerializeField]
	private bool isCalculate;

	private void Start()
	{
		if ((bool)sizeRenderer)
		{
			clipMaterial = sizeRenderer.sharedMaterial;
		}
		if ((bool)shortSizeRender_1)
		{
			clipMaterial2 = shortSizeRender_1.sharedMaterial;
		}
		if ((bool)shortSizeRender_2)
		{
			clipMaterial3 = shortSizeRender_2.sharedMaterial;
		}
	}

	private void SetMaterialValue(Vector3 pos, Vector3 normal)
	{
		if ((bool)clipMaterial)
		{
			clipMaterial.SetVector("_ClipObjPos", pos);
			clipMaterial.SetVector("_ClipObjNormal", normal);
		}
		if ((bool)clipMaterial2)
		{
			clipMaterial2.SetVector("_ClipObjPos", pos);
			clipMaterial2.SetVector("_ClipObjNormal", normal);
		}
		if ((bool)clipMaterial3)
		{
			clipMaterial3.SetVector("_ClipObjPos", pos);
			clipMaterial3.SetVector("_ClipObjNormal", normal);
		}
	}

	public void SetCalculate(bool value)
	{
		isCalculate = value;
	}

	private void Update()
	{
		if (isCalculate)
		{
			clipPos = clipObjTrans.position;
			if (normalStatus == NormalStatus.down)
			{
				clipNormal = clipObjTrans.rotation * Vector3.down;
			}
			else if (normalStatus == NormalStatus.up)
			{
				clipNormal = clipObjTrans.rotation * Vector3.up;
			}
			else if (normalStatus == NormalStatus.left)
			{
				clipNormal = clipObjTrans.rotation * Vector3.left;
			}
			else if (normalStatus == NormalStatus.right)
			{
				clipNormal = clipObjTrans.rotation * Vector3.right;
			}
			SetMaterialValue(clipPos, clipNormal);
		}
	}
}
