using System;

namespace DLC7
{
	public class FrameAnimationEvent
	{
		private static FrameAnimationEvent _instance;

		public static FrameAnimationEvent Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new FrameAnimationEvent();
				}
				return _instance;
			}
		}

		public event Action<string, int, int> frameFinished;

		public void FrameFinished(string frameName, int curFrame, int maxFrameNumber)
		{
			if (this.frameFinished != null)
			{
				this.frameFinished(frameName, curFrame, maxFrameNumber);
			}
		}
	}
}
