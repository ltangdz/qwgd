using DG.Tweening;
using Honeti;

namespace DLC7.Titan
{
	public class VirusFinishedLoading : TitanVirusBaseDialog
	{
		public AlubaLoading1 loading;

		protected override void AfterShow()
		{
			loading.AddCallback(FinishedLoading);
			loading.BeginLoad();
		}

		private void FinishedLoading()
		{
			loading.StopAllCoroutines();
			loading._loadingText.text = "";
			loading._loadingText.DOText(I18N.instance.getValue("^110008_game_119"), 0.3f);
			Invoke("Finish", 1.3f);
		}

		private void Finish()
		{
			GetComponentInParent<TitanVirusDialog>().Finished();
		}

		protected override void AfterHidden()
		{
		}
	}
}
