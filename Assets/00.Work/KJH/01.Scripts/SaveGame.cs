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
    }
    
    public void Save(int[] arr1, int[] arr2)
    {
        using StreamWriter sw = new StreamWriter(File.Open(savePath, FileMode.OpenOrCreate));

        for (int i = 0; i < arr1.Length; i++)
            sw.WriteLine(arr1[i]);
        for (int i = 0; i < arr2.Length; i++)
            sw.WriteLine(arr2[i]);
    }

    public void Load(int[] arr1, int[] arr2)
    {
        loada = new int[arr1.Length];
        loadb = new int[arr2.Length];
        
        using StreamReader sr = new StreamReader(File.Open(savePath, FileMode.Open));
        
        for (int i = 0; i < arr1.Length; i++)
            if (int.TryParse(sr.ReadLine(), out int value))
                loada[i] = value;
        for (int i = 0; i < arr2.Length; i++)
            if (int.TryParse(sr.ReadLine(), out int value))
                loadb[i] = value;
    }
}