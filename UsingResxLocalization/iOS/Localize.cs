using System;
using Xamarin.Forms;
using Foundation;
using System.Runtime.CompilerServices;
using System.Threading;

[assembly:Xamarin.Forms.Dependency(typeof(UsingResxLocalization.iOS.Localize))]

namespace UsingResxLocalization.iOS
{
	public class Localize : UsingResxLocalization.ILocalize
	{
		public void SetLocale ()
		{
			var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
			var netLocale = iosLocaleAuto.Replace ("_", "-");
			System.Globalization.CultureInfo ci;
			try {
				ci = new System.Globalization.CultureInfo (netLocale);
			} catch {
				ci = GetCurrentCultureInfo ();
			}
			Thread.CurrentThread.CurrentCulture = ci;
			Thread.CurrentThread.CurrentUICulture = ci;

			Console.WriteLine ("SetLocale: " + ci.Name);
		}

		public System.Globalization.CultureInfo GetCurrentCultureInfo ()
		{
//			#region not sure why this isn't working for me (in simulator at least)
//			var iosLocaleAuto = NSLocale.AutoUpdatingCurrentLocale.LocaleIdentifier;
//			var iosLanguageAuto = NSLocale.AutoUpdatingCurrentLocale.LanguageCode;
//			Console.WriteLine ("nslocaleid:" + iosLocaleAuto);
//			Console.WriteLine ("nslanguage:" + iosLanguageAuto);
//
//			var iosLocale = NSLocale.CurrentLocale.LocaleIdentifier;
//			var iosLanguage = NSLocale.CurrentLocale.LanguageCode;
//
//			var netLocale = iosLocale.Replace ("_", "-");
//			var netLanguage = iosLanguage.Replace ("_", "-");
//
//			Console.WriteLine ("ios:" + iosLanguage + " " + iosLocale);
//			Console.WriteLine ("net:" + netLanguage + " " +  netLocale);
//
//			// doesn't seem to affect anything (well, i didn't expect it to affect UIKit controls)
////			var ci = new System.Globalization.CultureInfo ("JA-jp");
////			System.Threading.Thread.CurrentThread.CurrentCulture = ci;

//			#endregion
			var netLanguage = "en";
			// HACK: not sure why NSLocale isn't ever returning correct data
			if (NSLocale.PreferredLanguages.Length > 0) {
				var pref = NSLocale.PreferredLanguages [0];

				// HACK: Apple treats portuguese fallbacks in a strange way
				// https://developer.apple.com/library/ios/documentation/MacOSX/Conceptual/BPInternational/LocalizingYourApp/LocalizingYourApp.html
				// "For example, use pt as the language ID for Portuguese as it is used in Brazil and pt-PT as the language ID for Portuguese as it is used in Portugal"
				if (pref.Substring(0,2) == "pt")
				{
					if (pref == "pt")
						pref = "pt-BR"; // get the correct Brazilian language strings from the PCL RESX (note the local iOS folder is still "pt")
					else
						pref = "pt-PT"; // Portugal
				}
				netLanguage = pref.Replace ("_", "-");
				Console.WriteLine ("preferred:" + netLanguage);
			}
			return new System.Globalization.CultureInfo(netLanguage);
		}
	}
}

