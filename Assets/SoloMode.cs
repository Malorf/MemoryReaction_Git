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
    public GameObject prefabStats;
    public Canvas myCanvas;
    public GameObject background;
    private GameObject goStat;
    public GameObject[] gameObjectImages;
    public Sprite[] images;
    public string[] names;
    public AudioClip[] audioLanguage;
    public Sprite[] animalsImages;
    public string[] animalsNames;
    public Sprite[] foodImages;
    public string[] foodNames;
    public AudioClip[] animalsInEnglish;
    public AudioClip[] animalsInFrench;
    public AudioClip[] foodInEnglish;
    public AudioClip[] foodInFrench;
    private int numberOfImages;
    public GridLayoutGroup grid;
    IDictionary<Sprite, String> myDictionary = new Dictionary<Sprite, String>();
    IList<string> myListName = new List<string>();
    IList<AudioClip> myListAudio = new List<AudioClip>();
    public TextMeshProUGUI nameOfImageToFind;
    private int randomName;
    public static int nbeErrors;
    public static bool record;

    public AudioSource audioSolo;
    public AudioSource voiceSolo;
    public AudioClip soundBad;
    public AudioClip soundGood;
    public AudioClip soundEnd;

    public GameObject goMalus;

    private string translate;

    private void Start()
    {
        audioSolo = gameObject.AddComponent<AudioSource>();
        audioSolo.playOnAwake = false; //true by default -> sound is played on start and we don't want it
        voiceSolo = gameObject.AddComponent<AudioSource>();
        voiceSolo.playOnAwake = false; 
    }
    public void CreateGrid()
    {
        numberOfImages = GridThemeSolo.scaleGrid * GridThemeSolo.scaleGrid;
        for (int i = 0; i < numberOfImages; i++)
        {
            prefabImage.GetComponent<Image>().sprite = images[i];
            GameObject image = Instantiate(prefabImage, transform.parent);
            image.transform.SetParent(myCanvas.transform);
            image.transform.GetChild(0).gameObject.GetComponent<Button>().onClick.AddListener(OnClickImage);
        }
        gameObjectImages = GameObject.FindGameObjectsWithTag("Image"); // stocking every instantiate items
        switch (GridThemeSolo.scaleGrid) //ImagesSizeDependOnScalingChoosed
        {
            case 2:  //GridSizeIs 2x2
                grid.cellSize = new Vector2(400, 400);
                startingTime = 10f; //timeForMemorize
                break;
            case 3:  //GridSizeIs 3x3
                grid.cellSize = new Vector2(300, 300);
                startingTime = 10f;
                break;
            case 4: //GridSizeIs 4x4
                grid.cellSize = new Vector2(240, 240);
                startingTime = 20f;
                break;
            case 5: //GridSizeIs 5x5
                grid.cellSize = new Vector2(180, 180);
                startingTime = 30f;
                break;
        }
    }

    public void RotateGrid()
    {
        foreach (GameObject goImages in gameObjectImages)
        {
            if (goImages != null) //PreventMinorIssueOn"gameObjectImages"
            {
                Animator animImages = goImages.GetComponent<Animator>(); 
                animImages.SetBool("Rotate90°", true); //ImagesFlipAt90° All bricks button are up
                switch (GridThemeSolo.scaleGrid) //ImagesSizeDependOnScalingChoosed
                {
                    case 2:  //SizeIs 2x2
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
    public void DestroyStats()
    {
        if (goStat != null)
        Destroy(goStat);
    }

    public void Dictionary() //ImageAssociatedWithName //remplacer tout par "images" et "names" ?
    {
        for (int i = 0; i < numberOfImages; i++)
        {
            myDictionary.Add(images[i], names[i]);
        }
    }
    public void Reset()
    {
        DestroyDictionary();
        DestroyGrid();
        DestroyStats();
        myListName.Clear();
        myListAudio.Clear();
        nameOfImageToFind.text = "";
        StopAllCoroutines();
        nbeErrors = 0;
        
    }
    public void DestroyDictionary()
    {
        myDictionary.Clear();
    }
    public void ThemeAndLanguageArray() //Initialise lists and arrays with rights parameters
    {
        switch (GridThemeSolo.themeNumber)
        {
            case 1:  //ThemeIsAnimal
                images = animalsImages; 
                names = animalsNames;
                switch (Langage.indexLanguage)
                {
                    case 1: 
                        audioLanguage = animalsInEnglish;
                        break;
                    case 2: 
                        audioLanguage = animalsInFrench;
                        break;
                }
                break;
        
            case 2:  //ThemeIsFood
                images = foodImages;
                names = foodNames;
                switch (Langage.indexLanguage)
                {
                    case 1: 
                        audioLanguage = foodInEnglish;
                        break;
                    case 2: 
                        audioLanguage = foodInFrench;
                        break;
                }
                break;
        }
    }
    public void Shuffle()
    {
        for (int i = 0; i < numberOfImages; i++) //Shuffle (need to shuffle all arrays at the same time(or bug when you change one parameter)) "25 or numberOfImages"
        {
            string temp1 = animalsNames[i];
            Sprite temp2 = animalsImages[i];
            AudioClip temp3 = animalsInEnglish[i];
            AudioClip temp4 = animalsInFrench[i];
            AudioClip temp5 = foodInEnglish[i];
            AudioClip temp6 = foodInFrench[i];
            string temp7 = foodNames[i];
            Sprite temp8 = foodImages[i];
            int randomIndex = Random.Range(i, 25);
            animalsNames[i] = animalsNames[randomIndex];
            animalsImages[i] = animalsImages[randomIndex];
            foodNames[i] = foodNames[randomIndex];
            foodImages[i] = foodImages[randomIndex];
            animalsInEnglish[i] = animalsInEnglish[randomIndex];
            animalsInFrench[i] = animalsInFrench[randomIndex];
            foodInEnglish[i] = foodInEnglish[randomIndex];
            foodInFrench[i] = foodInFrench[randomIndex];
            animalsNames[randomIndex] = temp1;
            animalsImages[randomIndex] = temp2;
            animalsInEnglish[randomIndex] = temp3;
            animalsInFrench[randomIndex] = temp4;
            foodInEnglish[randomIndex] = temp5;
            foodInFrench[randomIndex] = temp6;
            foodNames[randomIndex] = temp7;
            foodImages[randomIndex] = temp8;
        }
    }
    public void StartOrPlayAgain()
    {
        Reset();
        Shuffle();
        ThemeAndLanguageArray();
        CreateGrid();
        Dictionary();
        ListNames();
        currentTime = startingTime;
        timerText.text = string.Format("{0:0}:{1:00}", Mathf.Floor(0 / 60), 0 % 60);
        Memorize();
    }
    public void ListNames()
    {
        for (int i = 0; i < numberOfImages; i++)
        {
            myListName.Add(names[i]);
            myListAudio.Add(audioLanguage[i]);
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
                Stats();
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
                Stats();
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
        Animator malus = goMalus.GetComponent<Animator>();
        if (myDictionary[ButtonPrefab.mySpritePrefabImage] == myListName[randomName]) //if there is a match
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
            malus.SetBool("TurnON", true);
            nbeErrors++;
        }
        IEnumerator DelayBeforeReturninBrick()
        {
            yield return new WaitForSeconds(1f);
            localAnim.SetBool("Rotate90°", true);
            localButton.enabled = true;
            malus.SetBool("TurnON", false);
        }
    }

    public static float currentTime = 0f;
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

    public void Stats()
    {
        GameObject stats = Instantiate(prefabStats, transform.parent);
        goStat = stats;
        stats.transform.SetParent(background.transform);
        stats.transform.localPosition = new Vector3(-450, -1070, 0);
        stats.transform.localScale = new Vector3(0.6f, 0.6f ,0.6f);
        Records();
    }

    public void Records()
    {
        bool localRecord = false;
        if (PlayerPrefs.GetFloat("record2x2") == 0) //Initialize if first time playing
        {
            PlayerPrefs.SetFloat("record2x2", 100000);
        }
        if (PlayerPrefs.GetFloat("record3x3") == 0) //Initialize if first time playing
        {
            PlayerPrefs.SetFloat("record3x3", 100000);
        }
        if (PlayerPrefs.GetFloat("record4x4") == 0) //Initialize if first time playing
        {
            PlayerPrefs.SetFloat("record4x4", 100000);
        }
        if (PlayerPrefs.GetFloat("record5x5") == 0) //Initialize if first time playing
        {
            PlayerPrefs.SetFloat("record5x5", 100000);
        }
        if (GridThemeSolo.scaleGrid == 2 && currentTime < PlayerPrefs.GetFloat("record2x2"))
        {
            PlayerPrefs.SetFloat("record2x2", currentTime);
            localRecord =true;
        }
        if (GridThemeSolo.scaleGrid == 3 && currentTime < PlayerPrefs.GetFloat("record3x3"))
        {
            PlayerPrefs.SetFloat("record3x3", currentTime);
            localRecord = true;
        }
        if (GridThemeSolo.scaleGrid == 4 && currentTime < PlayerPrefs.GetFloat("record4x4"))
        {
            PlayerPrefs.SetFloat("record4x4", currentTime);
            localRecord = true;
        }
        if (GridThemeSolo.scaleGrid == 5 && currentTime < PlayerPrefs.GetFloat("record5x5"))
        {
            PlayerPrefs.SetFloat("record5x5", currentTime);
            localRecord = true;
        }
        record = localRecord;
    }
    public void Mute()
    {
        audioSolo.mute = !audioSolo.mute;
        voiceSolo.mute = !voiceSolo.mute;
    }
    //Only translation
    public void InFrench()  
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
            translate = "Eléphant";
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
            translate = "Zèbre";
        }
        if (translate == "Apple")
        {
            translate = "Pomme";
        }
        if (translate == "Bananas")
        {
            translate = "Bananes";
        }
        if (translate == "Cheese")
        {
            translate = "Fromage";
        }
        if (translate == "Chicken")
        {
            translate = "Poulet";
        }
        if (translate == "Chips")
        {
            translate = "Frites";
        }
        if (translate == "Chocolate")
        {
            translate = "Chocolat";
        }
        if (translate == "Coffee")
        {
            translate = "Café";
        }
        if (translate == "Donuts")
        {
            translate = "Beignets";
        }
        if (translate == "Eggs")
        {
            translate = "Oeufs";
        }
        if (translate == "Grape")
        {
            translate = "Raisin";
        }
        if (translate == "Hamburger")
        {
            translate = "Hamburger";
        }
        if (translate == "Icecream")
        {
            translate = "Glace";
        }
        if (translate == "Kiwi")
        {
            translate = "Kiwi";
        }
        if (translate == "Lemon")
        {
            translate = "Citron";
        }
        if (translate == "Milk")
        {
            translate = "Lait";
        }
        if (translate == "Orange")
        {
            translate = "Orange";
        }
        if (translate == "Pancakes")
        {
            translate = "Crêpes";
        }
        if (translate == "Peach")
        {
            translate = "Pêche";
        }
        if (translate == "Pineapple")
        {
            translate = "Ananas";
        }
        if (translate == "Pizza")
        {
            translate = "Pizza";
        }
        if (translate == "Strawberry")
        {
            translate = "Fraise";
        }
        if (translate == "Watermelon")
        {
            translate = "Pastèque";
        }
        if (translate == "Pear")
        {
            translate = "Poire";
        }
        if (translate == "Bread")
        {
            translate = "Pain";
        }
        if (translate == "Cherry")
        {
            translate = "Cerise";
        }
    }
}
