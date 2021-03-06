﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void PlayGame  () {
        GameObject loading = new GameObject("LoadingText");
        loading.transform.SetParent(this.transform);
        loading.AddComponent<Text>().text = "Loading...";
        Text loadingText = loading.GetComponent<Text>();
        loadingText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        loadingText.transform.position = new Vector3(522, -21);
        loadingText.fontSize = 22;
        loadingText.alignment = TextAnchor.UpperCenter;
        Instantiate(loading);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
