﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;


public class SpawnNotes : MonoBehaviour {
	public GameObject prefab;

	private struct position
	{
		public float x;
		public float y;
		public float z;
	}

	private struct note
	{
		public position pos;
		public long tick;
		public string hand;
		public bool spawned;
	
	}
	private long offsetticks;
	private List<note> MySongNotes;
	private long startingticks;

	// Use this for initialization
	void Awake () {

    string filePath = System.IO.Path.GetFullPath("TwinkleV1.txt");
		StreamReader sr = new StreamReader (filePath);
		startingticks = System.DateTime.Now.Ticks;
		MySongNotes = new List<note>();
		while (!sr.EndOfStream)
		{
			string Note = sr.ReadLine();
			long tempticks;
			note TempNote = new note();

			string[] tempinput = Note.Split(',');
			float.TryParse(tempinput[0], out TempNote.pos.x);
			float.TryParse(tempinput[1], out TempNote.pos.y);
			float.TryParse(tempinput[2], out TempNote.pos.z);
			long.TryParse(tempinput[3], out tempticks);
			TempNote.tick = tempticks - startingticks;
			MySongNotes.Add(TempNote);
		}
	
	
	}

	// Update is called once per frame
	void Update () {
		long currentticks = System.DateTime.Now.Ticks;
		offsetticks = currentticks - startingticks;
 
		foreach(var n in MySongNotes) {
			if (n.tick < offsetticks && n.spawned == false) {
				//spawn note
				Instantiate(prefab, new Vector3(n.pos.x, n.pos.y, n.pos.z), Quaternion.identity);
				//n.spawned = true;
			}
		}
	}
}