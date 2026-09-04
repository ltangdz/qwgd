using UnityEngine;

namespace Michsky.UI.FieldCompleteMainMenu
{
	public class SplashScreenManager : MonoBehaviour
	{
		[Header("RESOURCES")]
		public GameObject splashScreen;

		public GameObject splashScreenLogin;

		public GameObject splashScreenRegister;

		public GameObject mainPanels;

		private Animator mainPanelsAnimator;

		[Header("SETTINGS")]
		public bool isLoggedIn;

		public bool alwaysShowLoginScreen = true;

		public bool disableSplashScreen;

		private void Start()
		{
			if (disableSplashScreen)
			{
				splashScreen.SetActive(value: false);
				splashScreenLogin.SetActive(value: false);
				splashScreenRegister.SetActive(value: false);
				mainPanels.SetActive(value: true);
				mainPanelsAnimator = mainPanels.GetComponent<Animator>();
				mainPanelsAnimator.Play("Main Panel Opening");
			}
			else if (!isLoggedIn && alwaysShowLoginScreen)
			{
				splashScreen.SetActive(value: false);
				splashScreenLogin.SetActive(value: true);
				splashScreenRegister.SetActive(value: true);
			}
			else if (!isLoggedIn && !alwaysShowLoginScreen)
			{
				splashScreen.SetActive(value: false);
				splashScreenLogin.SetActive(value: true);
				splashScreenRegister.SetActive(value: true);
			}
			else if (!isLoggedIn && !alwaysShowLoginScreen)
			{
				splashScreen.SetActive(value: false);
				splashScreenLogin.SetActive(value: true);
				splashScreenRegister.SetActive(value: true);
			}
			else if (isLoggedIn && alwaysShowLoginScreen)
			{
				splashScreen.SetActive(value: false);
				splashScreenLogin.SetActive(value: true);
				splashScreenRegister.SetActive(value: true);
			}
			else if (isLoggedIn && !alwaysShowLoginScreen)
			{
				splashScreen.SetActive(value: true);
				splashScreenLogin.SetActive(value: false);
				splashScreenRegister.SetActive(value: false);
			}
		}
	}
}
