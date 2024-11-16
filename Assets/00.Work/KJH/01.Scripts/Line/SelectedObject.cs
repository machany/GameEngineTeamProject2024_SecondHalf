using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SelectedObject
{
    public int CurrentLineInputRed { get; set; }
    public int CurrentLineInputBlue { get; set; }
    public int CurrentLineInputGreen { get; set; }
    public int CurrentLineOutputRed { get; set; }
    public int CurrentLineOutputBlue { get; set; }
    public int CurrentLineOutputGreen { get; set; }
}
