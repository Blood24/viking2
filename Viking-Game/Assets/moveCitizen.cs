using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCitizen : MonoBehaviour
{
    public List<Transform> paths;
    public bool access,isBusy = false;
    int last = 0;
    int ran = 1;
    Animator animator;
    public string characterType;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isBusy)
        {
            if (!access)
            {
                
                animator.SetBool("forword", true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(paths[ran].position.x, 1f, paths[ran].position.z - 0.80f), 9f * Time.deltaTime);
                Quaternion desRotation = Quaternion.LookRotation(new Vector3(paths[ran].position.x, 1f, paths[ran].position.z - 0.80f) - transform.position);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, desRotation.eulerAngles.y, 0), 1500 * Time.deltaTime);

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
        animator.SetBool("forword", true);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(mm.transform.position.x, 1f, mm.transform.position.z), 6f * Time.deltaTime);
        Quaternion desRotation = Quaternion.LookRotation(mm.transform.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, desRotation.eulerAngles.y, 0), 1500 * Time.deltaTime);
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
            animator.SetBool("forword", false);
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
                case "GoldCollector":
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
