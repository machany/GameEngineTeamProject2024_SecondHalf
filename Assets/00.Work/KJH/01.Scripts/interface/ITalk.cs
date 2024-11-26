using System.Collections.Generic;

public interface ITalk
{
    List<string> Text { get; set; }

    void NextText(string text);
}
