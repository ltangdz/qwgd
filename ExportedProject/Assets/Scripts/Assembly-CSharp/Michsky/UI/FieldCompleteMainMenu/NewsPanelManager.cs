using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class NewsPanelManager : MonoBehaviour
	{
		[Header("NEWS LIST")]
		public List<GameObject> panels = new List<GameObject>();

		[Header("RESOURCES")]
		public Slider slider;

		private string panelFadeIn = "NPI In";

		private string panelFadeOut = "NPI Out";

		private string buttonFadeIn = "NPIS In";

		private string buttonFadeOut = "NPIS Out";

		private GameObject currentPanel;

		private GameObject nextPanel;

		private GameObject currentButton;

		private GameObject nextButton;

		[Header("SETTINGS")]
		public int currentPanelIndex;

		[Range(1f, 25f)]
		public float speed = 3f;

		private int currentButtonlIndex;

		private Animator currentPanelAnimator;

		private Animator nextPanelAnimator;

		private Animator currentButtonAnimator;

		private Animator nextButtonAnimator;

		private int newPanel;

		private int sizeOfList;

		private float sliderValue;

		private void Start()
		{
			sizeOfList = panels.Count;
			sizeOfList--;
			InvokeRepeating("ChangeNew", speed, speed);
			slider.maxValue = sizeOfList;
			slider.value = currentPanelIndex;
		}

		private void ChangeNew()
		{
			if (newPanel == sizeOfList)
			{
				nextPanelAnimator = nextPanel.GetComponent<Animator>();
				nextPanelAnimator.Play(panelFadeOut);
				newPanel = 0;
				currentPanelIndex = 0;
				nextPanel = panels[currentPanelIndex];
				nextPanelAnimator = nextPanel.GetComponent<Animator>();
				nextPanelAnimator.Play(panelFadeIn);
			}
			else
			{
				currentPanel = panels[currentPanelIndex];
				currentPanelIndex = newPanel;
				currentPanelAnimator = currentPanel.GetComponent<Animator>();
				currentPanelIndex++;
				nextPanel = panels[currentPanelIndex];
				nextPanelAnimator = nextPanel.GetComponent<Animator>();
				currentPanelAnimator.Play(panelFadeOut);
				nextPanelAnimator.Play(panelFadeIn);
				newPanel++;
			}
			slider.value = currentPanelIndex;
		}

		public void SwitchClick(int newPanel)
		{
			if (newPanel == sizeOfList)
			{
				nextPanelAnimator = nextPanel.GetComponent<Animator>();
				nextPanelAnimator.Play(panelFadeOut);
				newPanel = 0;
				currentPanelIndex = 0;
				nextPanel = panels[currentPanelIndex];
				nextPanelAnimator = nextPanel.GetComponent<Animator>();
				nextPanelAnimator.Play(panelFadeIn);
			}
			else
			{
				currentPanel = panels[currentPanelIndex];
				currentPanelIndex = newPanel;
				currentPanelAnimator = currentPanel.GetComponent<Animator>();
				currentPanelIndex++;
				nextPanel = panels[currentPanelIndex];
				nextPanelAnimator = nextPanel.GetComponent<Animator>();
				currentPanelAnimator.Play(panelFadeOut);
				nextPanelAnimator.Play(panelFadeIn);
				newPanel++;
			}
		}
	}
}
