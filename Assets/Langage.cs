using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Langage : MonoBehaviour
{
    public static int indexLanguage = 2; //StartWithFrench
    public static AudioSource audios;
    public TextMeshProUGUI languages;
    public TextMeshProUGUI versus;
    public TextMeshProUGUI solo;
    public TextMeshProUGUI txtFrench;
    public TextMeshProUGUI txtEnglish;
    public AudioClip musiqueMenu;
    public void TextLanguages ()
    {
        if (indexLanguage == 1) //1 = english
        {
            languages.text = "Languages";
            versus.text = "Versus";
            solo.text = "Solo";
            txtFrench.text = "French";
            txtEnglish.text = "English";
        }
        if (indexLanguage == 2) //2 = french
        {
            languages.text = "Langues";
            versus.text = "Contre";
            solo.text = "Seul";
            txtFrench.text = "Français";
            txtEnglish.text = "Anglais";
        }
    }
    public void FrenchLanguage ()
    {
        indexLanguage = 2;
        TextLanguages();
    }
    public void EnglishLanguage()
    {
        indexLanguage = 1;
        TextLanguages();
    }
    public void Musique()
    {
        audios.clip = musiqueMenu;
        audios.Play();
    }
    public void Mute()
    {
        audios.mute = !audios.mute;
    }
    // Update is called once per frame
    void Start()
    {
        audios = GetComponent<AudioSource>();
        Musique();
        TextLanguages();
    }
}
