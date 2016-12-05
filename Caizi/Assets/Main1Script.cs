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

	private bool isTime = false;
	private float currentTime = 0.0f;
	private int currentTimeCount = 0;

	private int currentChapterid = 0;

	// Use this for initialization
	void Start ()
	{

		this.currentChapterCount = 1;
		print (GameDataManger.currentSelectNodeInfo);
		Hashtable ht = GameDataManger.currentSelectNodeInfo as Hashtable;
		info = new LevelInfo (ht);
		this.chapterCount = info.Level_Quantity;
 
		this.idiomsList = new ArrayList ();
		tmpBtn.gameObject.SetActive (false);
  
		//开始
		this.startChapter ();
	}

	void onGoClick (GameObject g)
	{
		print (g.name);


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

		print ("btnCount:" + btnCount);
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
			} else {
				go.GetComponentInChildren<UILabel> ().text = "" + idinfo ["idiom_error" + (i + 1)];
			}

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
		
		int i = 1;
		while (i <= 15) {
			this.lastTimeLbl.text = "" + i;
			this.randomWordEffect (i);
			yield return new WaitForSeconds (1);
			i++;
		}
	}

	private ArrayList randWordXTick = new ArrayList ();
	private ArrayList randWordYTick = new ArrayList ();

	private ArrayList randWordTick = new ArrayList ();

	private void randomWordEffect (int index)
	{
  
		int cx = Random.Range (0, 480);
		int cy = Random.Range (0, 180);
		/**
		bool br = false;
	 
		while (true) {
			
			foreach (GameObject ggo in randWordTick) {
				if(cx<ggo.transform.localPosition.x && cx>ggo.transform.localPosition.x+ggo.transform.localScale.x*120){
					br = true;
					break; 
				}
			}

			if (br)
				break;

			cx = Random.Range (1, 480);
		}

		this.randWordXTick.Add (cx);

		br = false;
		while (true) {
			foreach (GameObject ggo in randWordTick) {
				if(cy<ggo.transform.localPosition.y && cy>ggo.transform.localPosition.y+ggo.transform.localScale.y*20){
					br = true;
					break;
				}
			}

			if (br)
				break;

			cy = Random.Range (1, 180);
		}

		this.randWordYTick.Add (cy);
*/
		GameObject go = (GameObject)Instantiate (tmpLbl.gameObject);
		go.transform.parent = this.gameObject.transform;
		go.transform.localPosition = new Vector3 (-150 + cx, -111 + cy, 0);
		go.transform.localScale = new Vector3 (Random.value * 2, Random.value+1, 1);

		//go.transform.localPosition = new Vector3 (-150 + 260, 50, 0);
		//go.transform.localScale = new Vector3 (1, 1, 1);

		ArrayList al = DataManger.WordsDic as ArrayList;
		Hashtable idinfo = null;

		foreach (Hashtable ht in al) {
			int id = int.Parse ((string)ht ["ID"]);
			if (id == this.currentChapterid) {
				idinfo = ht;
				break;
			}
		}

		go.GetComponentInChildren<UILabel>().text = "" + idinfo ["Words" + index];

		randWordTick.Add (go);

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
