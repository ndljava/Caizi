using System;
using System.Collections;

public class GameDataManger
{

	/// <summary>
	/// 当前选择的章节id
	/// </summary>
	public static string currentSelectNodeID;

	public static Hashtable currentSelectNodeInfo{
		get{
			ArrayList al = DataManger.LevelDic as ArrayList;
			 
			foreach (Hashtable ht in al) {
				if ((string)ht ["ID"] == currentSelectNodeID)
					return ht;
			}

			return null;

		}
		 
	}

	public GameDataManger ()
	{
	}
}


