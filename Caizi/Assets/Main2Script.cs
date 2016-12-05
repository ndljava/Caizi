using UnityEngine;
using System.Collections;

public class Main2Script : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		GameObject btn = GameObject.Find ("Button");
		GameObject integralLbl = GameObject.Find ("integralLbl");


		GameObject quitBtn = GameObject.Find ("QuitBtn");
		UIEventListener.Get (quitBtn).onClick = onQuitClick;

		GameObject descBtn = GameObject.Find ("DescBtn");
		UIEventListener.Get (descBtn).onClick = onDescClick;

		btn.SetActive (false);
		integralLbl.SetActive (false);

		ArrayList data = DataManger.LevelDic as ArrayList;

		Hashtable ht;
		GameObject go;
		for (int i = 0; i < data.Count; i++) {
			 
			ht = data [i] as Hashtable;
			if (ht ["ID"].ToString ().ToCharArray () [0] != '1')
				return;

			go = Instantiate (integralLbl);
			go.transform.parent = this.transform;
			go.transform.localScale = Vector3.one;
			go.transform.localPosition = new Vector3 (-367 + Mathf.Floor(i%7) * 150, 86-Mathf.Floor(i/7)*120, 0);
			go.GetComponent<UILabel> ().text = "积分:";

			go.SetActive (true);


			go = Instantiate (btn);
			go.transform.parent = this.transform;
			go.transform.localScale =new Vector3(0.4f,1,1);
			go.transform.localPosition = new Vector3 (-350 + Mathf.Floor(i%7) * 150, 43-Mathf.Floor(i/7)*120, 0);
			go.name ="关卡:" + ht["ID"];

			UILabel gch = go.GetComponentInChildren<UILabel> ();
			gch.text="关卡:"+ht["ID"];
			gch.transform.localScale =new Vector3(1.8f,1,1);

			go.SetActive (true);


			UIEventListener.Get (go).onClick = onLoadClick;
		}
 
	}

	void onDescClick (GameObject go)
	{
		print (go.name);

		Application.LoadLevelAsync ("Caizi3");
		Application.UnloadLevel ("Caizi2");

	}

	void onLoadClick (GameObject go)
	{
		print (go.name);
		 
		GameDataManger.currentSelectNodeID = go.name.Split (':')[1]; 

		Application.LoadLevelAsync ("Caizi1");
		Application.UnloadLevel ("Caizi2");
	}

	void onQuitClick (GameObject go)
	{
		print (go.name);
		Application.Quit ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
 

}
