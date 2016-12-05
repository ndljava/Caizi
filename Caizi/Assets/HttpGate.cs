using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Reflection;
using System.IO;
using UnityEngine;


 
public class HttpGate:MonoBehaviour
{
 
	/// <summary>
	/// AssetBundles\Android\p\2016\yangchuyang\100\0\101\config
	/// </summary>
	/// <param name="fname">Fname.</param>
	/// <param name="callback">Callback.</param>
	/// <param name="sheet">Sheet.</param>
	public static void Send (string fname, Action<object> callback = null, int sheet = 1)
	{

		string urlip = "http://218.245.6.131";
		string urlpath = "cardgame/gamecall.php";
		string pmethod = "system::get_excel";
		int formate = 2;
		string fileUrl = "AssetBundles/Android/p/2016/yangchuyang/100/0/101/config/" + fname + ".xlsx";

		//sheet = (sheet==0?1:sheet);

		string path = urlip + "/" + urlpath + "?pmethod=" + pmethod + "&sheet=" + sheet + "&format=" + formate + "&excel=" + fileUrl;
		Debug.Log (path);
		/**
		 * 这个必须的
		 * MonoBehaviour 是必须在GameObject里才能正常执行的；
		 * */
		GameObject go = new GameObject ("HttpGate");
		HttpGate gate = go.AddComponent<HttpGate> ();
		gate.StartCoroutine (gate.wwwRequest (path, callback));

		//StartCoroutine(wwwRequest(path));
	}


	IEnumerator wwwRequest (string path, Action<object> callback)
	{
		//Debug.Log (path);
		WWW ww = new WWW (path);
		 
		yield return ww;
		 
		if (ww.error != null) {
			Debug.Log (ww.error);
		} else {
			//Debug.Log (ww.text);

			if (callback != null) {
				string[] str=new string[2];
				str [0] = path;
				str [1] = ww.text;
				callback (str);
			}
		}

	}

}
 
