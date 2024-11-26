using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveGame : MonoBehaviour
{
    public string savePath;

    public int[] a;
    public int[] b;

    public int[] loada;
    public int[] loadb;
    private void Awake()
    {
        savePath = Path.Combine(Directory.GetCurrentDirectory(), "Save.txt");
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
            sw.WriteLine(string.Join(",", a));
            sw.WriteLine(string.Join(",", b));
        }
    }

    public void Load()
    {
        using (StreamReader sr = new StreamReader(File.Open(savePath, FileMode.Open)))
        {
           
            string[] arrayData1 = sr.ReadLine().Split(',');
            loada = new int[arrayData1.Length];
            for (int i = 0; i < arrayData1.Length; i++)
            {
                loada[i] = int.Parse(arrayData1[i]);
            }
            
            string[] arrayData2 = sr.ReadLine().Split(',');
            loadb = new int[arrayData2.Length];
            for (int i = 0; i < arrayData2.Length; i++)
            {
                loadb[i] = int.Parse(arrayData2[i]);
            }
        }
    }

    
}