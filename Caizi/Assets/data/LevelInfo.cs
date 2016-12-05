using System;
using System.Collections;


/// <summary>
/// ID	Level_scores	Difficulty	Level_Quantity	Extraction_Library1	Library1_Quantity	Extraction_Library2	idioms_coding1	
/// </summary>
public class LevelInfo
{
	 
	public int ID;
	public float Level_scores;
	public int Difficulty;
	public int Level_Quantity;
	public string Extraction_Library1;
	public int Library1_Quantity;
	public string Extraction_Library2;
	public int idioms_coding1;

	public LevelInfo (Hashtable ht)
	{
		this.ID = int.Parse((string)ht["ID"]);
		this.Level_scores = float.Parse((string)ht ["Level_scores"]);
		this.Difficulty = int.Parse((string)ht ["Difficulty"]);
		this.Level_Quantity = int.Parse((string)ht ["Level_Quantity"]);
		this.Extraction_Library1 = (string)ht ["Extraction_Library1"];
		this.Library1_Quantity = int.Parse((string)ht ["Library1_Quantity"]);
		this.Extraction_Library2 = (string)ht ["Extraction_Library2"];
		this.idioms_coding1 = int.Parse((string)ht ["idioms_coding1"]);
	}
}
 

