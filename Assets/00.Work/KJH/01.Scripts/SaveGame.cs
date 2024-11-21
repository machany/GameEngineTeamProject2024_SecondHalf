using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveGame : MonoBehaviour
{
    public string savePath = @"Assets/00.Work\KJH\01.Scripts\SaveGame.txt";

    public int a;
    public int[] b;

    public int loada;
    public int[] loadb;


    private void Awake()
    {
        Save();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Load();
        }
    }

    public void Save()
    {
        using (StreamWriter sw = new StreamWriter(File.Open(savePath, FileMode.OpenOrCreate)))
        {
            sw.WriteLine(a);
            sw.WriteLine(string.Join(",", b));
        }
    }

    public void Load()
    {
        using (StreamReader sr = new StreamReader(File.Open(savePath, FileMode.Open)))
        {
            loada = int.Parse(sr.ReadLine());
            
            string[] arrayData = sr.ReadLine().Split(',');
            loadb = new int[arrayData.Length];
            for (int i = 0; i < arrayData.Length; i++)
            {
                loadb[i] = int.Parse(arrayData[i]);
            }
        }
    }
}