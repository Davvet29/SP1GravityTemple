using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeTextScript : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    [TextArea(3, 10)]
    public List<string> strings = new();
    private int currentText = 0;
    public void Start()
    {
        ChangeText();
    }

    public void ChangeText()
    {
        if (currentText+1 <= strings.Count)
        {
            text.text = strings[currentText];
            currentText++;
        }
        if (currentText+1 > strings.Count)
        {
            this.gameObject.SetActive(false);
        }
    }
}
