using System;
using UnityEngine;

namespace Honeti
{
	[Serializable]
	public class I18NFonts
	{
		public LanguageCode lang;

		public Font font;

		public bool customLineSpacing;

		public float lineSpacing = 1f;

		public bool customFontSizeOffset;

		public int fontSizeOffsetPercent;

		public bool customAlignment;

		public TextAlignment alignment;
	}
}
