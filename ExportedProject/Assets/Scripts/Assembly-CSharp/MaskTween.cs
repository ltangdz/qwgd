using DG.Tweening;
using DLC7;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MaskTween : MonoBehaviour
{
	public Image maskImage;

	public void ResetTweenParam()
	{
		maskImage.material.SetVector("_UVOffset", Vector4.zero);
		maskImage.material.SetFloat("_MaskScale", 0f);
		maskImage.material.SetFloat("_MainScale", 0f);
		maskImage.material.SetFloat("_Alpha", 1f);
	}

	public void ChangeAlpha(float alpha, float time)
	{
		maskImage.material.DOFloat(alpha, "_Alpha", time);
	}

	public void PlayMaskScaleTween(float time, UnityAction action)
	{
		GetComponent<FrameAnimation2D>().Play();
		maskImage.material.DOVector(new Vector4(-0.065f, 0.18f, 0f, 0f), "_UVOffset", time);
		maskImage.material.DOFloat(0.32f, "_MainScale", time);
		((Tween)maskImage.material.DOFloat(0.2f, "_MaskScale", time)).OnComplete((TweenCallback)delegate
		{
			action?.Invoke();
		});
	}
}
