using System;
using UnityEngine;
using UnityEngine.U2D;

namespace _DLC8.Game.Voice
{
	public class VoicePrintEvent
	{
		private static VoicePrintEvent _instance;

		private SpriteAtlas _spriteAtlas;

		public static VoicePrintEvent Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new VoicePrintEvent();
				}
				return _instance;
			}
		}

		public event Action<VoicePrintRoleDLC8> onNoticeClickRole;

		public event Action onNoticeStartGame;

		public event Action<string, string, bool> onNoticeUsed;

		public void NoticeClickRole(VoicePrintRoleDLC8 roleDlc8)
		{
			if (this.onNoticeClickRole != null)
			{
				this.onNoticeClickRole(roleDlc8);
			}
		}

		public void NoticeUsed(string source, string path, bool isUsed)
		{
			if (this.onNoticeUsed != null)
			{
				this.onNoticeUsed(source, path, isUsed);
			}
		}

		public void SetSpriteAtlas(SpriteAtlas atlas)
		{
			_spriteAtlas = atlas;
		}

		public Sprite GetSprite(string name)
		{
			return _spriteAtlas.GetSprite(name);
		}
	}
}
