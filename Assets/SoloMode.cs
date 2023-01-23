using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;

public class SoloMode : MonoBehaviour
{
    public GameObject prefabImage;
    public Canvas myCanvas;
    public GameObject[] gameObjectImages;
    public Sprite[] animalsImages;
    public string[] animalsNames;
    public AudioClip[] animalsInEnglish;
    public AudioClip[] animalsInFrench;
    private int numberOfImages;
    public GridLayoutGroup grid;
    IDictionary<Sprite, String> myAnimals = new Dictionary<Sprite, String>();
    IList<string> myListName = new List<string>();
    IList<AudioClip> myListAudio = new List<AudioClip>();
    public TextMeshProUGUI nameOfImageToFind;
    private int randomName;

    public AudioSource audioSolo;
    public AudioSource voiceSolo;
    public AudioClip soundBad;
    public AudioClip soundGood;
    public AudioClip soundEnd;

    private string translate;
    public void CreateGrid()
    {
        numberOfImages = GridThemeSolo.scaleGrid * GridThemeSolo.scaleGrid;
        for (int i = 0; i < numberOfImages; i++)
        {
            {
                switch(GridThemeSolo.themeNumber) //ImagesDependOnThemeChoosed
                {
                    case 1:  //ThemeIsAnimal
                    prefabImage.GetComponent<Image>().sprite = animalsImages[i]; //ImagesAreAnimals
                        break;   
                }
                GameObject image = Instantiate(prefabImage, transform.parent);
                image.transform.SetParent(myCanvas.transform);
                image.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(OnClickImage);
            }
        }
        gameObjectImages = GameObject.FindGameObjectsWithTag("Image"); // stocking every instantiate items
        switch (GridThemeSolo.scaleGrid) //ImagesSizeDependOnScalingChoosed
        {
            case 2:  //GridSizeIs 2x2
                grid.cellSize = new Vector2(400, 400);
                startingTime = 10f;
                break;
            case 3:  //GridSizeIs 3x3
                grid.cellSize = new Vector2(300, 300);
                startingTime = 10f;
                break;
            case 4: //GridSizeIs 4x4
                grid.cellSize = new Vector2(240, 240);
                startingTime = 15f;
                break;
            case 5: //GridSizeIs 5x5
                grid.cellSize = new Vector2(180, 180);
                startingTime = 20f;
                break;
        }
    }

    public void RotateGrid()
    {
        foreach (GameObject goImages in gameObjectImages)
        {
            if (goImages != null) //PreventMinorIssueOn"gameObjectImages"
            {
                Animator images = goImages.GetComponent<Animator>(); 
                images.SetBool("Rotate90°", true); //ImagesFlipAt90° All bricks button are up
                switch (GridThemeSolo.scaleGrid) //ImagesSizeDependOnScalingChoosed
                {
                    case 2:  //SizeIs 3x3
                        goImages.transform.GetChild(0).localScale = new Vector3(1.3f, 1.3f, 1.3f);
                        break;
                    case 3:  //SizeIs 3x3
                        goImages.transform.GetChild(0).localScale = new Vector3(1.1f, 1.1f, 1.1f);
                        break;
                    case 4: //SizeIs 4x4
                        goImages.transform.GetChild(0).localScale = new Vector3(0.85f, 0.85f, 0.85f);
                        break;
                    case 5: //SizeIs 4x4
                        goImages.transform.GetChild(0).localScale = new Vector3(0.70f, 0.70f, 0.70f);
                        break;
                }
            }
        }
    }
    public void DestroyGrid()
    {
        foreach (GameObject goImages in gameObjectImages)
        {
            Destroy(goImages);
        }
    }

    public void DictionnaryAnimals() //ImageAssociatedWithName
    {
        for (int i = 0; i < numberOfImages; i++)
        {
            {
                myAnimals.Add(animalsImages[i], animalsNames[i]);
            }
        }
    }
    public void Reset()
    {
        DestroyDictionnary();
        DestroyGrid();
        myListName.Clear();
        myListAudio.Clear();
        nameOfImageToFind.text = "";
        StopAllCoroutines();
    }
    public void DestroyDictionnary()
    {
        myAnimals.Clear();
    }

    public void ShuffleImages() //StartTheGameOrPlayAgain
    {
        Reset();
        for (int i = 0; i < numberOfImages; i++) //Shuffle
        {
            string temp1 = animalsNames[i];
            Sprite temp2 = animalsImages[i];
            AudioClip temp3 = animalsInEnglish[i];
            AudioClip temp4 = animalsInFrench[i];
            int randomIndex = Random.Range(i, numberOfImages);
            animalsNames[i] = animalsNames[randomIndex];
            animalsImages[i] = animalsImages[randomIndex];
            animalsInEnglish[i] = animalsInEnglish[randomIndex];
            animalsInFrench[i] = animalsInFrench[randomIndex];
            animalsNames[randomIndex] = temp1;
            animalsImages[randomIndex] = temp2;
            animalsInEnglish[randomIndex] = temp3;
            animalsInFrench[randomIndex] = temp4;
        }
        CreateGrid();
        DictionnaryAnimals();
        ListNames();
        currentTime = startingTime;
        audioSolo = gameObject.AddComponent<AudioSource>();
        voiceSolo = gameObject.AddComponent<AudioSource>();
        Memorize();
    }
    public void ListNames()
    {
        for (int i = 0; i < numberOfImages; i++)
        {
            myListName.Add(animalsNames[i]);
            switch (Langage.indexLanguage)
            {
                case 1 :
                    myListAudio.Add(animalsInEnglish[i]);
                    break;
                case 2:
                    myListAudio.Add(animalsInFrench[i]);
                    break;
            }
        }
    }
    [SerializeField] TextMeshProUGUI timerText;

    public void TimerTick()
    {
        timerText.enabled = true;
        if (myListName.Count > 0)
        {
            timerText.text = string.Format("{0:0}:{1:00}", Mathf.Floor(currentTime / 60), currentTime % 60);  //Time in min + sec
            StartCoroutine(DelayBeforeTickTimer());
        }
        if (myListName.Count <= 0)
        {
            timerText.text = string.Format ("{0:0}:{1:00}", Mathf.Floor(currentTime/60), currentTime%60);
        }
        IEnumerator DelayBeforeTickTimer()
        {
            yield return new WaitForSeconds(1f);
            currentTime += 1;
            TimerTick();
        }
    }
    public void NameApparition() //ARandomNameAppeared
    {
        if (Langage.indexLanguage == 1) //GameInEnglish
        {
            if (myListName.Count > 0) //Get a random name on the list
            {
                randomName = Random.Range(0, myListName.Count);
                voiceSolo.clip = myListAudio[randomName]; //AudioName
                voiceSolo.Play();
                nameOfImageToFind.text = myListName[randomName]; //TextName
            }
            if (myListName.Count <= 0) //all names have been played
            {
                audioSolo.clip = soundEnd;
                audioSolo.Play();
                nameOfImageToFind.text = "Congrats !"; //Temporary (will be replaced by a panel stats of the game)
            }
        }
        if (Langage.indexLanguage == 2) //GameInFrench
        {
            if (myListName.Count > 0)
            {
                randomName = Random.Range(0, myListName.Count);
                translate = myListName[randomName];
                voiceSolo.clip = myListAudio[randomName]; 
                voiceSolo.Play();
                InFrench();
                nameOfImageToFind.text = translate;
            }
            if (myListName.Count <= 0)
            {
                audioSolo.clip = soundEnd;
                audioSolo.Play();
                nameOfImageToFind.text = "Félicitations !";
            }
        }
    }
    public void OnClickImage() //Main function for the player
    {
        Animator localAnim = ButtonPrefab.prefab.GetComponent<Animator>(); //An animator local variable for each Button (prevent to get an animator shared by every buttons)
        localAnim.SetBool("Rotate90°", false); //Reveal the image
        Button localButton = ButtonPrefab.prefab.GetComponentInChildren<Button>();
        localButton.enabled = false;//prevent from spamming button
        if (myAnimals[ButtonPrefab.mySpritePrefabImage] == myListName[randomName]) //if there is a match
        {
            audioSolo.clip = soundGood;
            audioSolo.Play();
            myListName.Remove(myListName[randomName]); //removing the found name of the list 
            myListAudio.Remove(myListAudio[randomName]);
            NameApparition(); // new name
        }
        else // if not
        {
            StartCoroutine(DelayBeforeReturninBrick()); //1sec before hiding again
            audioSolo.clip = soundBad;
            audioSolo.Play();
            currentTime += 5;
        }
        IEnumerator DelayBeforeReturninBrick()
        {
            yield return new WaitForSeconds(1f);
            localAnim.SetBool("Rotate90°", true);
            localButton.enabled = true;
        }
    }

    private float currentTime = 0f;
    private float startingTime = 10f;
    [SerializeField] TextMeshProUGUI countDownText;

    public void Memorize()
    {
        countDownText.enabled = true;
        if (currentTime > 0)
        {
            countDownText.text = currentTime.ToString("0");
            StartCoroutine(DelayBeforeTickCd());
        }
        if (currentTime <= 0)
        {
            currentTime = 0;
            RotateGrid();
            NameApparition();
            countDownText.enabled = false;
            TimerTick();
        }
        IEnumerator DelayBeforeTickCd()
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1;
            Memorize();
        }
    }

    //Only translation
    public void InFrench()  //We could also create an other array of names in french.. ? make a "randoNameEnglish" + "randomNameFrench" => Issue with dictionnary where names are in english
    {
        if (translate == "Bear")
        {
            translate = "Ours";
        }
        if (translate == "Camel")
        {
            translate = "Chameau";
        }
        if (translate == "Cat")
        {
            translate = "Chat";
        }
        if (translate == "Cow")
        {
            translate = "Vache";
        }
        if (translate == "Dog")
        {
            translate = "Chien";
        }
        if (translate == "Dolphin")
        {
            translate = "Dauphin";
        }
        if (translate == "Duck")
        {
            translate = "Canard";
        }
        if (translate == "Eagle")
        {
            translate = "Aigle";
        }
        if (translate == "Elephant")
        {
            translate = "Elephant";
        }
        if (translate == "Fish")
        {
            translate = "Poisson";
        }
        if (translate == "Flamingo")
        {
            translate = "Flamant Rose";
        }
        if (translate == "Fox")
        {
            translate = "Renard";
        }
        if (translate == "Giraffe")
        {
            translate = "Girafe";
        }
        if (translate == "Horse")
        {
            translate = "Cheval";
        }
        if (translate == "Kangaroo")
        {
            translate = "Kangourou";
        }
        if (translate == "Lion")
        {
            translate = "Lion";
        }
        if (translate == "Pig")
        {
            translate= "Cochon";
        }
        if (translate == "Rabbit")
        {
            translate = "Lapin";
        }
        if (translate == "Sheep")
        {
            translate = "Mouton";
        }
        if (translate == "Squirrel")
        {
            translate = "Ecureuil";
        }
        if (translate == "Tiger")
        {
            translate = "Tigre";
        }
        if (translate == "Turtle")
        {
            translate = "Tortue";
        }
        if (translate == "Wildebeest")
        {
            translate = "Gnou";
        }
        if (translate == "Wolf")
        {
            translate = "Loup";
        }
        if (translate == "Zebra")
        {
            translate = "Zebre";
        }
    }
}
