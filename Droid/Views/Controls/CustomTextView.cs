﻿using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace ComPact.Droid.Helpers
{
	public class CustomTextView : TextView
	{
		private const string Tag = "TextView";

		public CustomTextView(IntPtr javaRef, JniHandleOwnership transfer)
			: base(javaRef, transfer)
		{
		}
		public CustomTextView(Context context)
			: base(context)
		{
		}
		public CustomTextView(Context context, IAttributeSet attrs)
			: base(context, attrs)
		{
		}
		public CustomTextView(Context context, IAttributeSet attrs, int defStyle)
			: base(context, attrs, defStyle)
		{
			var a = context.ObtainStyledAttributes(attrs,
					Resource.Styleable.CustomFonts);
			var customFont = a.GetString(Resource.Styleable.CustomFonts_customFont);
			SetCustomFont(customFont);
			a.Recycle();
		}
		public CustomTextView(Context context, IAttributeSet attrs, int defStyleAttrs, int defStyleRes)
			: base(context, attrs, defStyleAttrs, defStyleRes)
		{
		}
		public void SetCustomFont(string asset)
		{
			Typeface tf;
			try
			{
				tf = Typeface.CreateFromAsset(Context.Assets, asset);
			}
			catch (Exception e)
			{
				Log.Error(Tag, string.Format("Could not get Typeface: {0} Error: {1}", asset, e));
				return;
			}

			if (null == tf) return;

			var tfStyle = TypefaceStyle.Normal;
			if (null != Typeface) //Takes care of android:textStyle=""
				tfStyle = Typeface.Style;
			SetTypeface(tf, tfStyle);
		}
	}
}
