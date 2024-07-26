using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Blizzard.Helpers
{
	public class BundleHelper
	{
		public static AssetBundle LoadIfNotLoaded(string name)
		{
			name = name.Replace('\\', '/');
			if (bundles.ContainsKey(name))
			{
				return bundles[name];
			}
			Console.WriteLine("Loading bundle " + name);
			AssetBundle assetBundle = AssetBundle.LoadFromFile(name);
			bundles[name] = assetBundle;
			return assetBundle;
		}

		public static void PatchUIObject(GameObject gameObject)
		{
            foreach (var text in gameObject.GetComponentsInChildren<TextMeshProUGUI>(true))
            {
                text.font = defaultFontAsset;
            }
        }

		public static Dictionary<string, AssetBundle> bundles = new Dictionary<string, AssetBundle>();

		public static string AssetBundlePath = "Blizzard/Content/";
		public static string AssetBetaBundlePath = AssetBundlePath + "BetaContent/";

#pragma warning disable
		public static TMP_FontAsset defaultFontAsset;
#pragma warning restore
	}
}