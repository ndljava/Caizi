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
	public static void Send (string fname, Action<string[]> callback = null, int sheet = 1)
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


	IEnumerator wwwRequest (string path, Action<string[]> callback)
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

	/// <summary>
	/// 保存数据
	/// </summary>
	/// <param name="value">Value.</param>
	/// <param name="callback">Callback.</param>
	/// <param name="sheet">Sheet.</param>
	public static void Save (string mname,string type,string value)
	{
 
//HTTP_GET_JSON({
//        "method":"user::do_sort",
//        "name":"身份证号码",
//        "type":"证件",
//        "value":"12345"
//    },false);
		 

		string urlip = "http://218.245.6.131";
		string urlpath = "cardgame/gamecall.php";
		string pmethod = "user::do_sort";

		//string mname = "caizi_name";
		//string type = "caizi_type";

		string path = urlip + "/" + urlpath + "?method=" + pmethod + "&name=" + mname + "&type=" + type + "&value=" + value+"";
		path += "&cookies[entype]=1&cookies[auth]=u0001|88888888";
		Debug.Log (path);
		/**
		 * 这个必须的
		 * MonoBehaviour 是必须在GameObject里才能正常执行的；
		 * */
		GameObject go = new GameObject ("HttpGate");
		HttpGate gate = go.AddComponent<HttpGate> ();
		gate.StartCoroutine (gate.wwwRequest (path,null));
	}

	/// <summary>
	/// Get 一条数据.
	/// </summary>
	/// <param name="mname">Mname.</param>
	/// <param name="type">Type.</param>
	/// <param name="callback">Callback.</param>
	/// <param name="sheet">Sheet.</param>
	public static void Get (string mname,string type,Action<string[]> callback = null)
	{

		//HTTP_GET_JSON({
		//        "method":"user::do_sort",
		//        "name":"身份证号码",
		//        "type":"证件",
		//        "value":"12345"
		//    },false);


		string urlip = "http://218.245.6.131";
		string urlpath = "cardgame/gamecall.php";
		string pmethod = "user::do_sort";

		//string mname = "caizi_name";
		//string type = "caizi_type";

		string path = urlip + "/" + urlpath + "?method=" + pmethod + "&name=" + mname + "&type=" + type;
		path += "&op=get&cookies[entype]=1&cookies[auth]=u0001|88888888";
		Debug.Log (path);

		/**
		 * 这个必须的
		 * MonoBehaviour 是必须在GameObject里才能正常执行的；
		 * */
		GameObject go = new GameObject ("HttpGate");
		HttpGate gate = go.AddComponent<HttpGate> ();
		gate.StartCoroutine (gate.wwwRequest (path, callback));
	}

}
 
