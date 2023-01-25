using Cake;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    private int lane = 3; //How much lane
    private int spawnPointNo = 35; //How much spawn point on one lane
    // Start is called before the first frame update
    void Start() {
    }

    public void StartGame() {
        GameObject.FindGameObjectWithTag("Player").transform.SetPositionAndRotation(Vector3.zero,Quaternion.identity);
        PlayerTriggerController.UIProcess();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("Finish", false);
        Player.PlayerController.currentSpeed = Player.PlayerController.baseSpeed;
        GameObject.Find("Level").GetComponent<TMP_Text>().text = "You Tried " + LevelController.level.ToString() + " Times";
        CakeController.SetCake(-CakeController.GetCake());
        GenerateSpawnPoint();
    }

    private void GenerateSpawnPoint() {
        GameObject spawnPointGO = GameObject.Find("SpawnPoint");
        for (int i = -4; i <= lane + 1; i += 4) {
            for (int j = 0; j <= spawnPointNo; j++) {
                if(Random.Range(0,2) != 0) {
                    Object[] textures = Resources.LoadAll("Prefabs", typeof(GameObject));
                    GameObject spawnPoint = Instantiate(textures[Random.Range(0, 4)] as GameObject);
                    spawnPoint.transform.SetParent(spawnPointGO.transform, false);
                    spawnPoint.transform.position = new Vector3(i, 0, j * 3);
                } else {
                    continue;
                }
            }
        }
    }
}
