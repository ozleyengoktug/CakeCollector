using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cake {
    public class CakeController : MonoBehaviour{
        private static int cakeCount = 0;
        public static int maxScore = 0;

        private void Start() {
            if (PlayerPrefs.HasKey("MaxScore"))
                maxScore = PlayerPrefs.GetInt("MaxScore");
            else
                maxScore = 0;
        }
        public static void SetCake(int amount) {
            cakeCount += amount;
            GameObject.Find("CakeCounter").GetComponent<TMP_Text>().text = cakeCount.ToString();
        }
        public static int GetCake() {
            return cakeCount;
        }
    }
}