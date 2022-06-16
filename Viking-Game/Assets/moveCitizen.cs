using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCitizen : MonoBehaviour
{
    public List<Transform> paths;
    public bool access,isBusy = false;
    int last = 0;
    int ran = 1;
    public string characterType;

    void Update()
    {
        if (!isBusy)
        {
            if (!access)
            {

                transform.position = Vector3.MoveTowards(transform.position, new Vector3(paths[ran].position.x, 1f, paths[ran].position.z - 0.80f), 9f * Time.deltaTime);
                last = ran;

            }
            else
            {
                ran = Random.Range(0, 3);
                access = false;
            }
        }
        else
        {
            moveToWeapensMaker();
        }

    }

    public void moveToWeapensMaker()
    {

        GameObject mm = GameObject.Find("EventSystem").GetComponent<UiManager>().Items[18];

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(mm.transform.position.x, 1f, mm.transform.position.z), 6f * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isBusy)
        {
            if (collision.gameObject.tag == "citizenStation")
            {

                access = true;

            }
        }

        if (collision.gameObject.tag == "WeapansMaker")
        {
            UiManager uiManager = GameObject.Find("EventSystem").GetComponent<UiManager>();
            PlayerPrefs.SetInt("Citizen", PlayerPrefs.GetInt("Citizen", 0) - 1);

            switch (characterType) {
                case "gaurd1":
                    uiManager.instantiateGuard();
                    Destroy(gameObject);
                    break;
                case "gaurd2":
                    uiManager.instantiateGuard2();
                    Destroy(gameObject);
                    break;
                case "MinerRedCrystal":
                    uiManager.instantiateCrystalRed();
                    Destroy(gameObject);
                    break;
                case "MinerBlueCrystal":
                    uiManager.instantiateCrystalBlue();
                    Destroy(gameObject);
                    break;
                case "MinerIron":
                    uiManager.instantiateIron();
                    Destroy(gameObject);
                    break;
                case "MinerGold":
                   uiManager.instantiateGold();
                    Destroy(gameObject);
                    break;
                case "TreeCutter":
                    uiManager.instantiateTreeCutter();
                    Destroy(gameObject);
                    break;
                case "RedCollector":
                    uiManager.instantiateRedCollector();
                    Destroy(gameObject);
                    break;
                case "BlueCollector":
                    uiManager.instantiateBlueCollector();
                    Destroy(gameObject);
                    break;
                case "IronCollector":
                    uiManager.instantiateIronCollector();
                    Destroy(gameObject);
                    break;
                case "GoldCutter":
                    uiManager.instantiateGoldCollector();
                    Destroy(gameObject);
                    break;
                case "TreeCollector":
                    uiManager.instantiateTreeCollector();
                    Destroy(gameObject);
                    break;

            }

            
        }
    }


}
