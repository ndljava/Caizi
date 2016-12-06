using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class GameCaizi : MonoBehaviour
{

	private int loadCount;
	 
	// Use this for initialization
	void Start ()
	{
		/**
		tex jsScript = (tex)this.gameObject.GetComponent<tex>();
		//Debug.Log(jsScript);

		object[] json_data=(object[])jsScript.web_request ("http://218.245.6.131/cardgame/gamecall.php?pmethod=system::get_excel&sheet=1&format=2&excel=AssetBundles/Android/p/2016/sunzhenyuan/300/0/200/config/动态加载模型.xlsx");
		//Debug.Log(json_data);
		for (int i = 0; i < json_data.Length; i++) {
			Hashtable row = (Hashtable)json_data [i];
			Debug.Log ("i=" + i + " 名称=" + row ["名称"]);
		}
*/



		this.loadInit ();
	}

	private void loadInit ()
	{
		NetManager.instanse ().send ("GuessIdioms_Idioms_001", loadComplete, 1);
		NetManager.instanse ().send ("GuessIdioms_Level_003", loadComplete, 1);
		NetManager.instanse ().send ("GuessIdioms_Settings_004", loadComplete, 1);
		NetManager.instanse ().send ("GuessIdioms_Words_002", loadComplete, 1);
		NetManager.instanse ().send ("zuma_compute_005", loadComplete, 1);

		/*
		//TextAsset ta = (TextAsset)Resources.Load ("json");
		//print (ta.text);
		//print (Application.dataPath);
		string str = File.ReadAllText (Application.dataPath + "/resources/GuessIdioms_Level_003.txt");
  
		//string[] str=File.ReadAllLines(Application.dataPath + "/resources/GuessIdioms_Idioms_001.txt");
	  
		object oj = NGUIJson.jsonDecode (str);
  
		ArrayList al = oj as ArrayList;
		//print (al.Count); 
		DataManger.LevelDic = al;

		str = File.ReadAllText (Application.dataPath + "/resources/GuessIdioms_Idioms_001.txt");
		al = NGUIJson.jsonDecode (str) as ArrayList;
		//print (al.Count); 
		DataManger.IdiomsDic = al;

		str = File.ReadAllText (Application.dataPath + "/resources/GuessIdioms_Words_002.txt");
		al = NGUIJson.jsonDecode (str) as ArrayList;
		//al = JsonUtility.FromJson as ArrayList;
		//print (al.Count); 
		DataManger.WordsDic = al;

		str = File.ReadAllText (Application.dataPath + "/resources/GuessIdioms_Settings_004.txt");
		al = NGUIJson.jsonDecode (str) as ArrayList;
		//al = JsonUtility.FromJson as ArrayList;
		//print (al.Count); 
		DataManger.SettingsDic = al;
	s	 
		str = File.ReadAllText (Application.dataPath + "/resources/zuma_compute_005.txt");
		al = NGUIJson.jsonDecode (str) as ArrayList;
		//al = JsonUtility.FromJson as ArrayList;
		//print (al.Count); 
		DataManger.computeDic = al;

		*/
		//loadComplete (null);
	}


	private void loadComplete (string[] ctx)
	{
		
		 
		string url = ctx [0];
		url=url.Substring(url.LastIndexOf ('/')+1);
		string fname = url.Substring (0, url.LastIndexOf ('.'));
		ArrayList al=NGUIJson.jsonDecode(ctx [1]) as ArrayList;

//		print (fname);
//		print (ctx [1]);

		switch (fname) {
		case "GuessIdioms_Idioms_001":
			DataManger.IdiomsDic = al;
			break;
		case "GuessIdioms_Level_003":
			DataManger.LevelDic = al;
			break;
		case "GuessIdioms_Settings_004":
			DataManger.SettingsDic = al;
			break;
		case "GuessIdioms_Words_002":
			DataManger.WordsDic = al;
			break;
		case "zuma_compute_005":
			DataManger.computeDic = al;
			break;
		}

		this.loadCount++;

		if (this.loadCount >= 5)
			Application.LoadLevelAsync ("Caizi2");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
