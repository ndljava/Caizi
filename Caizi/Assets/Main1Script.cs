using UnityEngine;
using System.Collections;

/// <summary>
/// 主答题ui
/// </summary>
public class Main1Script : MonoBehaviour
{

	public UILabel lastTimeLbl;
	public UILabel rightLbl;
	public UILabel wrongLbl;
	public UILabel countLbl;
	public UILabel startTimeLbl;

	public UIButton tmpBtn;
	public UILabel tmpLbl;

	/// <summary>
	/// 当前章节
	/// </summary>
	private int currentChapterCount;

	/// <summary>
	/// 章节数
	/// </summary>
	private int chapterCount;

	private ArrayList idiomsList;

	private int rightCount;
	private int wrongCount;

	private LevelInfo info;
 
	private float currentTime = 0.0f;
	private int currentTimeCount = 0;

	private int currentChapterid = 0;

	private ArrayList btnList;
  
	private int toteIntegral;

	// Use this for initialization
	void Start ()
	{

		//this.currentChapterCount = 1;
		print (GameDataManger.currentSelectNodeInfo);
		Hashtable ht = GameDataManger.currentSelectNodeInfo as Hashtable;
		info = new LevelInfo (ht);
		this.chapterCount = info.Level_Quantity;
 
		this.btnList = new ArrayList ();

		this.idiomsList = new ArrayList ();
		tmpBtn.gameObject.SetActive (false);
		tmpLbl.gameObject.SetActive (false);

		this.rightLbl.text = "0";
		this.wrongLbl.text = "0";

		//开始
		this.startChapter ();
	}

	void onGoClick (GameObject g)
	{
		print (g.name);
		ArrayList al = DataManger.IdiomsDic as ArrayList;
		Hashtable idinfo = null;

		this.removeAllUI ();
		this.StopAllCoroutines ();

		bool isRight = false;
		foreach (Hashtable ht in al) {
			int id = int.Parse ((string)ht ["ID"]);
			if (id == this.currentChapterid) {

				if (((string)ht ["idiom_correct"]) == g.name) {
					this.rightCount++;
					isRight = true;
				} else {
					this.wrongCount++;
					isRight = false;
				}

				break;
			}
		}


		this.rightLbl.text = "" + this.rightCount;
		this.wrongLbl.text = "" + this.wrongCount;

		 
		ArrayList sal = DataManger.SettingsDic as ArrayList;
		 
		int tc = 0;

		foreach (Hashtable ht in sal) {
			int id = int.Parse ((string)ht ["ID"]);
			if (id == 3) {
				tc = int.Parse ((string)ht ["Data"]);
				break;
			}
		}
		  

		al = DataManger.computeDic as ArrayList;
		idinfo = null;

		foreach (Hashtable ht in al) {
			int id = int.Parse ((string)ht ["idiom_quantity"]);
			if (id == this.info.Level_Quantity) {
				idinfo = ht;
				break;
			}
		}

		int cintegral = this.getHashObjToInt (idinfo ["correct_fraction"]);
		int lastTime = tc * 20 - this.currentTimeCount;
		//if(tc*20-this.currentTimeCount>5){
		cintegral = cintegral + lastTime * this.getHashObjToInt (idinfo ["one_fraction"]);
		//}
 
		this.toteIntegral += cintegral;

		string cname = "caizi_zhanjie_jifen_name_" + GameDataManger.currentSelectNodeID + "_" + this.currentChapterCount;
		string ctype = "caizi_zhanjie_jifen_type_" + GameDataManger.currentSelectNodeID + "_" + this.currentChapterCount;
 
		if (isRight)
			NetManager.instanse ().save (cname, ctype, "" + this.toteIntegral, onLccd);
		
		this.startChapter ();

	}

	private void onLccd (object oj)
	{
		print ("jifen" + oj);
	}

	private int getHashObjToInt (object str)
	{
		return int.Parse ((string)str);
	}

	// Update is called once per frame
	void Update ()
	{
 
	}

	/// <summary>
	/// 三秒倒计时
	/// </summary>
	/// <returns>The last time effect.</returns>
	private IEnumerator startLastTimeEffect ()
	{
		
		for (int i = 3; i >= 1; i--) {
			this.startTimeLbl.text = "" + (i);
			yield return new WaitForSeconds (1);
		}

		this.startTimeLbl.gameObject.SetActive (false);

		//this.StartCoroutine (this.randomChapter ());
		this.randomChapter ();
	}

	/// <summary>
	/// 生成成语数据ui，东西多比较卡，所以用协程；
	/// </summary>
	/// <returns>The chapter.</returns>
	private void randomChapter ()
	{
		//yield return 0;

		string[] standRandRect = this.info.Extraction_Library1.Split (',');
		string[] extrRandRect = this.info.Extraction_Library2.Split (',');

		int randId = 0;
		//System.Random rd = new System.Random ((int)System.DateTime.Now.Ticks*this.currentChapterCount);
		if (this.currentChapterCount <= this.info.Library1_Quantity) {
			randId = Random.Range (int.Parse (standRandRect [0]), int.Parse (standRandRect [1])); 
		} else {
			randId = Random.Range (int.Parse (extrRandRect [0]), int.Parse (extrRandRect [1]));
			/*while (true) {
				if (this.idiomsList.IndexOf (randId) == -1)
					break;	 

				randId = rd.Next (int.Parse (extrRandRect [0]), int.Parse (extrRandRect [1]));
			}*/
		}

		//randId = 1;
		ArrayList al = DataManger.IdiomsDic as ArrayList;
		Hashtable idinfo = null;

		foreach (Hashtable ht in al) {
			int id = int.Parse ((string)ht ["ID"]);
			if (id == randId) {
				
				idinfo = ht;
				break;
			}
		}

		this.currentChapterid = randId;

		this.idiomsList.Add (randId);

		int btnCount = this.info.Difficulty;
		int randTick = 0;
		int sp = (460 - btnCount * 120) / 2;

		ArrayList errid = new ArrayList ();

		//	print ("btnCount:" + btnCount);
		for (int i = 0; i < btnCount; i++) {

			//print (i);
			//System.Random rdr = new System.Random ((int)System.DateTime.Now.Ticks*btnCount);
			randTick = Random.Range (1, btnCount + 1);
			while (true) {
				if (errid.IndexOf (randTick) == -1)
					break;
				
				randTick = Random.Range (1, btnCount + 1);
			}
				
			errid.Add (randTick);
			//print ("randTick:" + randTick);
			GameObject go = (GameObject)Instantiate (tmpBtn.gameObject);
			go.transform.parent = this.gameObject.transform;
			go.transform.localPosition = new Vector3 (-200 + sp + randTick * 120, -167, 0);
			go.transform.localScale = new Vector3 (0.6f, 1, 1);

			go.SetActive (true);

			//print ("i:" + i);
			if (i == 0) {
				go.GetComponentInChildren<UILabel> ().text = "" + idinfo ["idiom_correct"];
				go.name = "" + idinfo ["idiom_correct"];
			} else {
				go.GetComponentInChildren<UILabel> ().text = "" + idinfo ["idiom_error" + (i + 1)];
			}


			this.btnList.Add (go);

			UIEventListener.Get (go).onClick = onGoClick;
		}

		this.StartCoroutine (this.randomLastTimeWord ());
	}

	/// <summary>
	/// 答题倒计时
	/// </summary>
	/// <returns>The last time word.</returns>
	private IEnumerator randomLastTimeWord ()
	{

		ArrayList al = DataManger.SettingsDic as ArrayList;
		Hashtable idinfo = null;
		int tc = 0;

		foreach (Hashtable ht in al) {
			int id = int.Parse ((string)ht ["ID"]);
			if (id == 3) {
				tc = int.Parse ((string)ht ["Data"]);
				break;
			}
		}

		 
		int tn = tc * 20;

		int i = 1;
		while (i <= tn) {
			this.lastTimeLbl.text = "" + i;
			this.currentTimeCount = i;
			if (i % tc == 0)
				this.randomWordEffect (i);
			
			yield return new WaitForSeconds (1);
			i++;
		}

		//答错；


	}

	private ArrayList randWordXTick = new ArrayList ();
	private ArrayList randWordYTick = new ArrayList ();

	private ArrayList randWordTick = new ArrayList ();

	private void randomWordEffect (int index)
	{
		if (index > 15)
			return;
  
		int cx = Random.Range (0, 480);
		int cy = Random.Range (0, 180);
		 
		bool br = false;
		 
		 
		while (true) {
			 
			if (this.randWordTick.Count > 0) {
				foreach (GameObject ggo in this.randWordTick) {
					if (cx < ggo.transform.localPosition.x-10 || cx > ggo.transform.localPosition.x + ggo.GetComponentInChildren<UILabel> ().localSize.x*ggo.transform.localScale.x) {
						br = true;
						break; 
					}

				}

			} else
				br = true;

			if (br)
				break;

			cx = Random.Range (1, 480);
		}

		this.randWordXTick.Add (cx);

		br = false;
		while (true) {
			if (this.randWordTick.Count > 0) {
				foreach (GameObject ggo in this.randWordTick) {
					if (cy < ggo.transform.localPosition.y-10 || cy > ggo.transform.localPosition.y + ggo.GetComponentInChildren<UILabel> ().localSize.y*ggo.transform.localScale.y) {
						br = true;
						break;
					}
				}
			} else
				br = true;
			
			if (br)
				break;

			cy = Random.Range (1, 180);
		}

 		

		this.randWordYTick.Add (cy);
 
 
		ArrayList al = DataManger.WordsDic as ArrayList;
		Hashtable idinfo = null;

		foreach (Hashtable ht in al) {
			int id = int.Parse ((string)ht ["ID"]);
			if (id == this.currentChapterid) {
				idinfo = ht;
				break;
			}
		}

		string words = (string)idinfo ["Words" + index];
		string[] wctx = words.Split (',');
		string[] ctx = null;

		foreach (GameObject wo in this.randWordTick) {
			ctx = wo.name.Split ('_');
			if (int.Parse (ctx [1]) == index) {
				
				if (int.Parse (ctx [2]) == 1) {
					wo.GetComponentInChildren<UILabel> ().text = wctx [0] + "" + wo.GetComponentInChildren<UILabel> ().text;
				} else if (int.Parse (ctx [2]) == 2) {
					wo.GetComponentInChildren<UILabel> ().text = wo.GetComponentInChildren<UILabel> ().text + wctx [0];
				}

				return;
			}
		}

		GameObject go = (GameObject)Instantiate (tmpLbl.gameObject);
		go.transform.parent = this.gameObject.transform;
		go.transform.localPosition = new Vector3 (-150 + cx, -111 + cy, 1);
 

		ctx = wctx;

		go.name = "caizi_" + ctx [1] + "_" + ctx [3];

		UILabel bLbl = go.GetComponentInChildren<UILabel> ();
		bLbl.text = "" + ctx [0];

		bLbl.color = Color.red;
		bLbl.width = (((string)ctx [0]).Length + int.Parse ((string)ctx [2])) * 20;
		bLbl.height = 20;
 
		print ("wwwww:" + bLbl.width);

		if (int.Parse (ctx [3]) == 1) {
			bLbl.alignment = NGUIText.Alignment.Right;
		} else if (int.Parse (ctx [3]) == 2) {
			bLbl.alignment = NGUIText.Alignment.Left;
		}
 
		//bLbl.text = "" + ctx [0];
  
		float fa = (Random.value + 0.4f);
		go.transform.localScale = new Vector3 (fa, fa, 1);
 	 
		go.SetActive (true);

		randWordTick.Add (go);

	}

	private void removeAllUI ()
	{
		 
		foreach (GameObject go in this.randWordTick) {
			Destroy (go);
		}

		foreach (GameObject go in this.btnList) {
			Destroy (go);
		}

		this.lastTimeLbl.text = "0";
	}

	private void startChapter ()
	{
		this.currentChapterCount++;
		this.countLbl.text = "第" + this.currentChapterCount + "关 " + this.chapterCount;


		this.startTimeLbl.gameObject.SetActive (true);
 
		this.randWordTick.Clear ();
		this.randWordXTick.Clear ();
		this.randWordYTick.Clear ();
		this.StartCoroutine (this.startLastTimeEffect ());
	}

}
