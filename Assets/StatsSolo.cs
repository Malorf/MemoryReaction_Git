using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsSolo : MonoBehaviour
{
    public Image star1;
    public Image star2;
    public Image star3;

    public TextMeshProUGUI timerNbe;
    public TextMeshProUGUI mistakesNbe;
    public TextMeshProUGUI recordNbe;
    public TextMeshProUGUI newRecord;

    public Image medalTime;
    public Image medalMistakes;
    public Image medalRecord;

    void Start()
    {
       timerNbe.text = string.Format("{0:0}:{1:00}", Mathf.Floor(SoloMode.currentTime / 60), SoloMode.currentTime % 60);
       mistakesNbe.text = "" + SoloMode.nbeErrors;
       RecordWithSize();
       NewRecord();
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
}
