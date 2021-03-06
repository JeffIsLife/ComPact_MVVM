﻿using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using ComPact.Models;
using ComPact.Payments;
using GalaSoft.MvvmLight.Helpers;
using Java.IO;


namespace ComPact.Droid
{
	[Activity]
	public class AddPaymentActivity : BaseActivity
	{
		//Local variables
		File _imageDirectory;
		string _currentImageFilePath;
		Image _image = new Image();
		public Image Image
		{
			get
			{
				return _image;
			}
			set
			{
				Image = value;
			}
		}
		//Keep track of bindings to avoid premature garbage collection
		readonly List<Binding> bindings = new List<Binding>();
		//Elements
		//__________________________Header______________________________
		ImageView _backImageView;
		TextView _titleTextView;
		ImageView _optionsImageView;
		//__________________________Others______________________________
		ImageView _addPictureImageView;
		EditText _whatEditText;
		EditText _priceEditText;
		EditText _detailsEditText;
		FloatingActionButton _createFloatingActionButton;
		//Bind Viewmodel to activity
		AddPaymentViewModel ViewModel
		{
			get
			{
				return App.Locator.AddPaymentViewModel;
			}
		}
		#region OnCreate
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			//Set Lay out
			SetContentView(Resource.Layout.ActivityAddPayment);

			//Init elements
			FindViews();
			//init
			Init();
			//bindings
			SetBindings();
			//Use Commands
			SetCommands();
			//Set Hint visible priceEditText
			_priceEditText.Text = null;


		}
		/**
		 * Init Views
		 */
		void FindViews()
		{
			//__________________________Header______________________________
			_backImageView = FindViewById<ImageView>(Resource.Id.customToolbarBackImageView);
			_titleTextView = FindViewById<TextView>(Resource.Id.customToolbarTitleTextView);
			_optionsImageView = FindViewById<ImageView>(Resource.Id.customToolbarOptionsImageView);
			//__________________________Others______________________________
			_addPictureImageView = FindViewById<ImageView>(Resource.Id.activityAddPaymentAddPicture);
			_whatEditText = FindViewById<EditText>(Resource.Id.activityAddPaymentWhatEditText);
			_priceEditText = FindViewById<EditText>(Resource.Id.activityAddPaymentPriceEditText);
			_detailsEditText = FindViewById<EditText>(Resource.Id.activityAddPaymentDetailsEditText);
			_createFloatingActionButton = FindViewById<FloatingActionButton>(Resource.Id.activityAddPaymentFloatingActionButton);
		}

		/**
		 * Set the bindings of this activity
		 */
		void SetBindings()
		{
			bindings.Add(this.SetBinding(() => ViewModel.Payment.Name, () => _whatEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Payment.Price, () => _priceEditText.Text, BindingMode.TwoWay));
			bindings.Add(this.SetBinding(() => ViewModel.Payment.Description, () => _detailsEditText.Text, BindingMode.TwoWay));
			//bindings.Add(this.SetBinding(() => ViewModel.Payment.Image.ImageValue, () => Image.ImageValue, BindingMode.TwoWay));
			//bindings.Add(this.SetBinding(() => ViewModel.Payment.Image.Path, () => Image.Path, BindingMode.TwoWay));

		}

		/**
		 * Register the commands from the ViewModel to the View
		 */
		void SetCommands()
		{
			_backImageView.SetCommand("Click", ViewModel.BackRedirectCommand);
			_createFloatingActionButton.SetCommand("Click", ViewModel.CreatePaymentCommand);
		}
		void Init()
		{
			_optionsImageView.Visibility = ViewStates.Gone;
			_titleTextView.Text = "Add Payment";

			_addPictureImageView.Click += BtnCamera_click;
		}



		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			Bitmap bitmap;
			int height = _addPictureImageView.Height;
			int width = _addPictureImageView.Width;
			bitmap = GetImageBitmapFromFilePath(_currentImageFilePath, height, width);

			if (bitmap != null)
			{
				ViewModel
					.SetImageCommand
					.Execute(new Image
					{
						ImageValue = Convert(bitmap),
						Path = _currentImageFilePath
					});
				//Image.ImageValue = Convert(bitmap);
				//Image.Path = _currentImageFilePath;

				_addPictureImageView.SetImageBitmap(bitmap);
				bitmap = null;
			}

		}

		//TODO Extract me
		byte[] Convert(Bitmap bitmap)
		{

			byte[] bitmapData;
			using (var stream = new System.IO.MemoryStream())
			{
				bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
				bitmapData = stream.ToArray(); 
			}
			return bitmapData;
		}

		void BtnCamera_click(object sender, EventArgs e)
		{
			TakePictureIntent();
		}





		//TODO Extract me
		Bitmap GetImageBitmapFromFilePath(string fileName, int width, int height)
		{
			//Get dimensions of the file on disk
			BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
			BitmapFactory.DecodeFile(fileName, options);

			//Calculate ratio to resize image
			int outHeight = options.OutHeight;
			int outWidth = options.OutWidth;
			int inSampleSize = 1;

			if (outHeight > height || outWidth > width)
			{
				inSampleSize = outWidth > outHeight
					? outHeight / height
					: outWidth / width;
			}
			//Load the image and have bitmap factory resize it
			options.InSampleSize = inSampleSize;
			Bitmap resizedBitMap = null;
			try
			{
				resizedBitMap = BitmapFactory.DecodeFile(fileName);//,options
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex);
			}

			return resizedBitMap;
		}

		private File CreateImageFile()
		{
			// Create an image file name
			string imageFileName = String.Format("payments_{0}", Guid.NewGuid());
			File storageDir = GetExternalFilesDir(Android.OS.Environment.DirectoryPictures);
			File image = File.CreateTempFile(
				imageFileName,  /* prefix */
				".jpg",         /* suffix */
				storageDir      /* directory */
			);
			// Save a file: path for use with ACTION_VIEW intents
			_currentImageFilePath = image.AbsolutePath;
			return image;
		}

		void TakePictureIntent()
		{
			//Filesystem interface System.io....
			Intent takePictureIntent = new Intent(MediaStore.ActionImageCapture);
			// Ensure that there's a camera activity to handle the intent
			if (takePictureIntent.ResolveActivity(PackageManager) != null)
			{
				// Create the File where the photo should go
				File photoFile = null;
				try
				{
					photoFile = CreateImageFile();
				}
				catch (IOException ex)
				{
					// Error occurred while creating the File
				}
				// Continue only if the File was successfully created
				if (photoFile != null)
				{
					var photoURI = FileProvider.GetUriForFile(this, this.PackageName + ".fileprovider", photoFile);

					takePictureIntent.PutExtra(MediaStore.ExtraOutput, photoURI);

					StartActivityForResult(takePictureIntent, 1);
				}
			}
		}

		#endregion
	}
	
}
