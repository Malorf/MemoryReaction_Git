using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Langage : MonoBehaviour
{
    public static bool french = true;
    public bool english = false;
    public TextMeshProUGUI languages;
    public TextMeshProUGUI versus;
    public TextMeshProUGUI solo;
    public TextMeshProUGUI txtFrench;
    public TextMeshProUGUI txtEnglish;
    public void TextLanguages ()
    {
        if (french == true)
        {
            languages.text = "Langues";
            versus.text = "Contre";
            solo.text = "Seul";
            txtFrench.text = "Français";
            txtEnglish.text = "Anglais";
        }
        else
        {
            languages.text = "Languages";
            versus.text = "Versus";
            solo.text = "Solo";
            txtFrench.text = "French";
            txtEnglish.text = "English";
        }
    }
    public void FrenchLanguage ()
    {
        french = true;
        TextLanguages();
    }
    public void EnglishLanguage()
    {
        french = false;
        TextLanguages();
    }
    // Update is called once per frame
    void Start()
    {
        TextLanguages();
    }
}
