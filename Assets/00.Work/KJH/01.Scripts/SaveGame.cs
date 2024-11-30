using System;
using UnityEngine;
using System.IO;

public class SaveGame : MonoSingleton<SaveGame>
{
    // 상대 경로
    private static readonly string savePath = Path.Combine(Directory.GetCurrentDirectory(), "Save.txt");

    public void Save(string saveData)
    {
        using StreamWriter sw = new StreamWriter(File.Open(savePath, FileMode.OpenOrCreate));

        sw.WriteLine(saveData);
    }

    public void Save(string[] saveData)
    {
        using StreamWriter sw = new StreamWriter(File.Open(savePath, FileMode.OpenOrCreate));

        foreach (var data in saveData)
            sw.WriteLine(data);
    }

    private string ReadLineAt(StreamReader sr, int targetLine)
    {
        string lineData = null;

        for (int i = 0; i < targetLine; ++i)
        {
            lineData = sr.ReadLine();

            if (lineData == null)
            {
                Debug.Log($"지정한 dataLine({targetLine})보다 파일이 작습니다. null 리턴");
                return null;
            }
        }

        return lineData;
    }

    /// <summary>
    /// 한 줄을 읽는 Load
    /// </summary>
    /// <param name="dataLine">읽을 줄</param>
    public string Load(int dataLine)
    {
        using StreamReader sr = new StreamReader(File.Open(savePath, FileMode.Open));

        return ReadLineAt(sr, dataLine);
    }
    
    /// <summary>
    /// 여러 줄을 읽는 Load
    /// </summary>
    /// <param name="dataLine">처음 읽을 줄</param>
    /// <param name="repeatNum">반복 횟수</param>
    public string[] Load(int dataLine, int repeatNum)
    {
        try
        {
            using StreamReader sr = new StreamReader(File.Open(savePath, FileMode.Open));
            string[] lineData = new string[repeatNum];
        
            for (int i = 0; i < repeatNum; ++i)
            {
                lineData[i] = ReadLineAt(sr, dataLine + i);
            
                if (lineData[i] == null)
                {
                    Debug.Log($"dataLine + repeatNum({dataLine + i})보다 파일이 작습니다. null 배열 리턴");
                    return null;
                }
            }

            return lineData;
        }
        catch (Exception e)
        {
            return null;
        }
        
    }
}