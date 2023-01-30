using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsSolo : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Image star1;
    public Image star2;
    public Image star3;
    public Sprite fullStar;

    public TextMeshProUGUI timer;
    public TextMeshProUGUI mistakes;
    public TextMeshProUGUI record;

    public TextMeshProUGUI timerNbe;
    public TextMeshProUGUI mistakesNbe;
    public TextMeshProUGUI recordNbe;
    public TextMeshProUGUI newRecord;



    void Start()
    {
        timerNbe.text = string.Format("{0:0}:{1:00}", Mathf.Floor(SoloMode.currentTime / 60), SoloMode.currentTime % 60);
        mistakesNbe.text = "" + SoloMode.nbeErrors;
        RecordWithSize();
        NewRecord();
        NumberOfStars();
        Language();
    }
    public void RecordWithSize()
    {
        switch (GridThemeSolo.scaleGrid) //RecordDependOnScalingChoosed
        {
            case 2:  //SizeIs 2x2
                recordNbe.text = string.Format("{0:0}:{1:00}", Mathf.Floor(PlayerPrefs.GetFloat("record2x2") / 60), PlayerPrefs.GetFloat("record2x2") % 60);
                break;
            case 3:  //SizeIs 3x3
                recordNbe.text = string.Format("{0:0}:{1:00}", Mathf.Floor(PlayerPrefs.GetFloat("record3x3") / 60), PlayerPrefs.GetFloat("record3x3") % 60);
                break;
            case 4: //SizeIs 4x4
                recordNbe.text = string.Format("{0:0}:{1:00}", Mathf.Floor(PlayerPrefs.GetFloat("record4x4") / 60), PlayerPrefs.GetFloat("record4x4") % 60);
                break;
            case 5: //SizeIs 4x4
                recordNbe.text = string.Format("{0:0}:{1:00}", Mathf.Floor(PlayerPrefs.GetFloat("record5x5") / 60), PlayerPrefs.GetFloat("record5x5") % 60);
                break;
        }
    }
    public void NewRecord()
    {
        if (SoloMode.record == true)
        {
            switch (Langage.indexLanguage)
            {
                case 1:
                    newRecord.text = "New Record !";
                    break;
                case 2:
                    newRecord.text = "Nouveau Record !";
                    break;
            }
        }
    }

    public void NumberOfStars()
    {
        Animator animStar1 = star1.GetComponent<Animator>();
        Animator animStar2 = star2.GetComponent<Animator>();
        Animator animStar3 = star3.GetComponent<Animator>();
        switch (GridThemeSolo.scaleGrid) //StarsDependOnScalingChoosed
        {
            case 2:  //SizeIs 2x2
                if (SoloMode.currentTime < 5)
                {
                    star1.GetComponent<Image>().sprite = fullStar;
                    star2.GetComponent<Image>().sprite = fullStar;
                    star3.GetComponent<Image>().sprite = fullStar;
                    animStar1.SetBool("StarReward", true);
                    animStar2.SetBool("StarReward", true);
                    animStar3.SetBool("StarReward", true);
                }
                if (5 <= SoloMode.currentTime && SoloMode.currentTime < 15)
                {
                    star1.GetComponent<Image>().sprite = fullStar;
                    star2.GetComponent<Image>().sprite = fullStar;
                    animStar1.SetBool("StarReward", true);
                    animStar2.SetBool("StarReward", true);
                }
                if (15 <= SoloMode.currentTime && SoloMode.currentTime < 30)
                {
                    star1.GetComponent<Image>().sprite = fullStar;
                    animStar1.SetBool("StarReward", true);
                }
                break;
            case 3:  //SizeIs 3x3
                if (SoloMode.currentTime < 10)
                {
                    star1.GetComponent<Image>().sprite = fullStar;
                    star2.GetComponent<Image>().sprite = fullStar;
                    star3.GetComponent<Image>().sprite = fullStar;
                    animStar1.SetBool("StarReward", true);
                    animStar2.SetBool("StarReward", true);
                    animStar3.SetBool("StarReward", true);
                }
                if (10 <= SoloMode.currentTime && SoloMode.currentTime < 30)
                {
                    star1.GetComponent<Image>().sprite = fullStar;
                    star2.GetComponent<Image>().sprite = fullStar;
                    animStar1.SetBool("StarReward", true);
                    animStar2.SetBool("StarReward", true);
                }
                if (30 <= SoloMode.currentTime && SoloMode.currentTime < 60)
                {
                    star1.GetComponent<Image>().sprite = fullStar;
                    animStar1.SetBool("StarReward", true);
                }
                break;
            case 4: //SizeIs 4x4
                if (SoloMode.currentTime < 20)
                {
                    star1.GetComponent<Image>().sprite = fullStar;
                    star2.GetComponent<Image>().sprite = fullStar;
                    star3.GetComponent<Image>().sprite = fullStar;
                    animStar1.SetBool("StarReward", true);
                    animStar2.SetBool("StarReward", true);
                    animStar3.SetBool("StarReward", true);
                }
                if (20 <= SoloMode.currentTime && SoloMode.currentTime < 40)
                {
                    star1.GetComponent<Image>().sprite = fullStar;
                    star2.GetComponent<Image>().sprite = fullStar;
                    animStar1.SetBool("StarReward", true);
                    animStar2.SetBool("StarReward", true);
                }
                if (40 <= SoloMode.currentTime && SoloMode.currentTime < 120)
                {
                    star1.GetComponent<Image>().sprite = fullStar;
                    animStar1.SetBool("StarReward", true);
                }
                break;
            case 5: //SizeIs 5x5
                if (SoloMode.currentTime < 60)
                {
                    star1.GetComponent<Image>().sprite = fullStar;
                    star2.GetComponent<Image>().sprite = fullStar;
                    star3.GetComponent<Image>().sprite = fullStar;
                    animStar1.SetBool("StarReward", true);
                    animStar2.SetBool("StarReward", true);
                    animStar3.SetBool("StarReward", true);
                }
                if (60 <= SoloMode.currentTime && SoloMode.currentTime < 120)
                {
                    star1.GetComponent<Image>().sprite = fullStar;
                    star2.GetComponent<Image>().sprite = fullStar;
                    animStar1.SetBool("StarReward", true);
                    animStar2.SetBool("StarReward", true);
                }
                if (120 <= SoloMode.currentTime && SoloMode.currentTime < 240)
                {
                    star1.GetComponent<Image>().sprite = fullStar;
                    animStar1.SetBool("StarReward", true);
                }
                break;
        }
    }

    public void Language()
    {
        switch (Langage.indexLanguage)
        {
            case 1:
                title.text = "Summary";
                timer.text = "Time";
                mistakes.text = "Mistakes";
                record.text = "Record";
                break;
            case 2:
                title.text = "Résumé";
                timer.text = "Temps";
                mistakes.text = "Erreurs";
                record.text = "Record";
                break;
        }
    }
}
