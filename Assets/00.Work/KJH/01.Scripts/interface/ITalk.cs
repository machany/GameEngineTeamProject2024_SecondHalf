using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITalk
{
    string[] Text { get; set; }

    void NextText(string text);
    
    
}
