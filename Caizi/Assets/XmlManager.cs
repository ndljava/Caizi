using UnityEngine;
using System.Collections;
using System.Xml;
using System.IO;
using System.Collections.Generic;

public class XmlManager {

	private XmlDocument _xdoc;

	public XmlManager(string path){
		_xdoc = new XmlDocument ();
		_xdoc.Load(path);
	}
 
	public XmlElement getNodeToElement(string aid,string value){
		XmlNode xnode = _xdoc.SelectSingleNode ("datas");
		foreach (XmlNode xn in xnode) {
			XmlElement xe = (XmlElement)xn;
			if (xe.GetAttribute (aid) == value) {
				return xe;
			}
		}

		return null;
	}

	public List<XmlElement> getNodeToElementAllByAid(string aid,string value){
		XmlNode xnode = _xdoc.SelectSingleNode ("datas");

		List<XmlElement> axml = new List<XmlElement> ();
		 
		foreach (XmlNode xn in xnode) {
			XmlElement xe = (XmlElement)xn;
			if (xe.GetAttribute (aid) == value) {
				axml.Add (xe);
			}
		}

		return axml;
	}

	public int getXmlLength(){
		int i = 0;
		XmlNode xnode = _xdoc.SelectSingleNode ("datas");
		foreach (XmlNode xn in xnode) {
			i++;
		}
		return i;
 
	}

}
