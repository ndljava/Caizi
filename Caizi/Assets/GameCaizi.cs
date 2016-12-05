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

		//NetManager.instanse ().send ("GuessIdioms_Idioms_001", loadComplete);
		//NetManager.instanse ().send ("GuessIdioms_Level_003", loadInit);
		//NetManager.instanse ().send ("GuessIdioms_Settings_004", loadInit);
		//NetManager.instanse ().send("GuessIdioms_Words_002");
		//NetManager.instanse ().send ("zuma_compute_005", loadInit);


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


		loadComplete (null);
	}


	private void loadComplete (string[] ctx)
	{
		/*
		string url = ctx [0];
		string fname = url.Substring (url.LastIndexOf ('/'));

		switch (fname) {
			case "GuessIdioms_Idioms_001":
				break;
			case "GuessIdioms_Level_003":
				break;
			case "GuessIdioms_Settings_004":
				break;
			case "GuessIdioms_Words_002":
				break;
			case "zuma_compute_005":
				break;
		}

		this.loadCount++;

		if(this.loadCount==5)
*/
		Application.LoadLevelAsync ("Caizi2");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
