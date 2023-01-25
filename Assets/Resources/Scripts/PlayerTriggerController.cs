using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Cake {
    public class PlayerTriggerController : MonoBehaviour {
        int multiplier = 1;
        float increaseAmount = 7.5f;
        static TMP_Text skillText;

        private void Start() {
            if(skillText == null)
                skillText = GameObject.Find("SkillExplain").GetComponent<TMP_Text>();
        }

        private void OnTriggerEnter(Collider other) {
            if (other.tag == "Item") {
                CakeController.SetCake(1 * multiplier);
                Destroy(other.gameObject);
            } else if (other.tag == "Finish") {
                gameObject.GetComponent<Animator>().SetBool("Finish", true);
                SetLevel();
                CheckMaxScore();
                UIProcess("Your Max Score : " + CakeController.maxScore);
            } else if (other.tag == "Event") {
                EventCheck();
            } else if (other.tag == "Obstacle") {
                CakeController.SetCake(-3);
                skillText.text = "Lost 3 Cakes For Collide";
            }
        }

        public IEnumerator X2Event() {
            multiplier = 2;
            yield return new WaitForSeconds(3);
            multiplier = 1;
        }

        private void EventCheck() {
            // 0 => Topladýklarýnýn Yarýsý Gider
            // 1 => 5snliðine Hýz Artýþý
            // 2 => 3 snliðine x2 Toplama
            int randEvent = Random.Range(0, 3);
            switch (randEvent) {
                case 0:
                    CakeController.SetCake(-(CakeController.GetCake() / 2));
                    skillText.text = "Cake / 2";
                    break;
                case 1:
                    Player.PlayerController.currentSpeed += increaseAmount;
                    skillText.text = "Speed Up";
                    break;
                case 2:
                    StartCoroutine(X2Event());
                    skillText.text = "Cake x2";
                    break;
            }
        }

        private void CheckMaxScore() {
            if (CakeController.maxScore < CakeController.GetCake()) {
                CakeController.maxScore = CakeController.GetCake();
                Debug.Log(CakeController.maxScore);
                PlayerPrefs.SetInt("MaxScore", CakeController.maxScore);
            }
        }

        private void SetLevel() {
            LevelController.level++;
            PlayerPrefs.SetInt("Level", LevelController.level);
        }

        public static void UIProcess(string text = "") {
            Transform UICanvas = GameObject.Find("StartMenu").transform.GetChild(0);
            UICanvas.gameObject.SetActive(text == "" ? false : true);
            UICanvas.GetChild(1).GetComponent<TMP_Text>().text = text;
        }
    }
}
