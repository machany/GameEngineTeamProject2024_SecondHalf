using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITalkImage
{
    List<Sprite> Sprite { get; set; }
    void NextSprite(Sprite sprite);
}
