using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static int level = 1;

    private void Start() {
        if (PlayerPrefs.HasKey("Level"))
            level = PlayerPrefs.GetInt("Level");
        else
            level = 1;
        GameObject.Find("Level").GetComponent<TMP_Text>().text = "You Tried " + LevelController.level.ToString() + " Times";
    }
}
