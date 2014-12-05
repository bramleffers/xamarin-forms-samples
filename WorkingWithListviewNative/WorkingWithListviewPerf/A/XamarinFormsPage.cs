﻿using System;

using Xamarin.Forms;
using System.Collections.Generic;

namespace WorkingWithListviewPerf
{
	/// <summary>
	/// This page uses built-in Xamarin.Forms controls to display a fast-scrolling list.
	/// 
	/// It uses the built-in <c>TextCell</c> class which does not require special 'layout'
	/// and is therefore faster than building a custom ViewCell in Xamarin.Forms.
	/// </summary>
	public class XamarinFormsPage : ContentPage
	{
		public XamarinFormsPage ()
		{
			var tableItems = new List<string> ();
			for (var i = 0; i < 100; i++) {
				tableItems.Add (i + " row ");
			}

			var listView = new ListView ();
			listView.ItemsSource = tableItems;
			listView.ItemTemplate = new DataTemplate(typeof(TextCell));
			listView.ItemTemplate.SetBinding(TextCell.TextProperty, ".");

			listView.ItemSelected += async (sender, e) => {
				await Navigation.PushModalAsync (new DetailPage(e.SelectedItem));
			};

			Content = new StackLayout { 
				Padding = new Thickness (5, Device.OnPlatform(20,0,0), 5, 0),
				Children = {
					new Label {
						XAlign = TextAlignment.Center,
						Text = "Xamarin.Forms built-in ListView" 
					},
					listView
				}
			};
		}
	}
}