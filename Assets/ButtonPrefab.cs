using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPrefab : MonoBehaviour
{
    public static Sprite mySpritePrefabImage;
    public GameObject refPrefab;
    public static GameObject prefab;
    public void MyChoise()
    {
        mySpritePrefabImage = refPrefab.GetComponent<Image>().sprite;
        prefab = refPrefab;
    }
}
