using UnityEngine;
using System.Collections;
using System;

public class NetManager
{

	private static NetManager _instance;

	 

	public NetManager ()
	{
		 
	}


	public static NetManager instanse ()
	{
		if (_instance == null)
			_instance = new NetManager ();

		return _instance;
	}

	 
	public void send (string fname, Action<string[]> callback, int sheet)
	{
		HttpGate.Send (fname, callback, sheet);
	}

	public void save (string fname, string type, string value, Action<object> callback = null, int sheet = 1)
	{
		HttpGate.Save (fname, type, value);
	}

}
