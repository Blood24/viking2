using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResouresePosition : MonoBehaviour
{

    
    public List<Transform> rowResourses;
    int index = 0;
    [SerializeField]
    public GameObject toInstantiate;
    Animator animator;

    public string type;

    public Transform lastt;


    float speed = 3f;
    int stopTime = 200;
    bool isMinning = false;

    void Start()
    {
        animator = GetComponent<Animator>();

    }

    [Obsolete]
    private void Update()
    {
        movePlayer();
        reActiveAll();
    }

    [Obsolete]
    private void movePlayer()
    {

        if (rowResourses.Count > 0 )
        {
            if (index != 0) {
                if (rowResourses[index].transform.gameObject.active && !rowResourses[index - 1].transform.gameObject.active)
                {

                    speed = 3f;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(rowResourses[index].transform.position.x, 0.5f, rowResourses[index].transform.position.z), speed * Time.deltaTime);
                    Quaternion desRotation = Quaternion.LookRotation(rowResourses[index].transform.position - transform.position);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, desRotation.eulerAngles.y, 0), 1500 * Time.deltaTime);

                    if (!isMinning)
                    {
                        animator.SetBool("forword", true);
                    }
                }
            } 
            else if (index ==0) {
                if (rowResourses[index].transform.gameObject.active)
                {

                    speed = 3f;
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(rowResourses[index].transform.position.x, 0.5f, rowResourses[index].transform.position.z), speed * Time.deltaTime);
                    Quaternion desRotation = Quaternion.LookRotation(rowResourses[index].transform.position - transform.position);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, desRotation.eulerAngles.y, 0), 1500 * Time.deltaTime);

                    if (!isMinning)
                    {
                        animator.SetBool("forword", true);
                    }
                }
            }
            
            
        }

    }

     void reActiveAll()
    {

        if (rowResourses[rowResourses.Count - 1] == null)
        {

            foreach (Transform t in rowResourses)
            {
                t.gameObject.SetActive(true);
            }
            index = 0;

        }
        
        
    }




    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "RawResources")
        {
            
            if (stopTime > 0)
            {
                animator.SetBool("forword", false);
                animator.SetBool("Minning", true);
                isMinning = true;
                speed = 0f;
                stopTime -= 2;
            }
            else
            {

                 animator.SetBool("Minning", false);
                 animator.SetBool("forword", true);
                UiManager uiManager = GameObject.Find("EventSystem").GetComponent<UiManager>();

                switch (type)
                {
                    case "red":

                        uiManager.redToCollect.Add(Instantiate(toInstantiate, new Vector3(rowResourses[index].position.x,0.4f, rowResourses[index].position.z), Quaternion.identity).transform);
                        rowResourses[index].gameObject.SetActive(false);
                        index += 1;

                        break;
                    case "blue":
                        uiManager.blueToCollect.Add(Instantiate(toInstantiate, new Vector3(rowResourses[index].position.x, 0.4f, rowResourses[index].position.z), Quaternion.identity).transform);
                        rowResourses[index].gameObject.SetActive(false);
                        index += 1;

                        break;
                    case "iron":
                        uiManager.ironToCollect.Add(Instantiate(toInstantiate, new Vector3(rowResourses[index].position.x, 0.4f, rowResourses[index].position.z), Quaternion.identity).transform);
                        rowResourses[index].gameObject.SetActive(false);
                        index += 1;

                        break;
                    case "gold":
                        uiManager.goldToCollect.Add(Instantiate(toInstantiate, new Vector3(rowResourses[index].position.x, 0.4f, rowResourses[index].position.z), Quaternion.identity).transform);
                        rowResourses[index].gameObject.SetActive(false);
                        index += 1;

                        break;
                    case "tree":
                        uiManager.treeToCollect.Add(Instantiate(toInstantiate, new Vector3(rowResourses[index].position.x, 0.05f, rowResourses[index].position.z), Quaternion.identity).transform);
                        rowResourses[index].gameObject.SetActive(false);
                        index += 1;

                        break;
                }


                if (index == rowResourses.Count)
                {
                    transform.position = lastt.transform.position;
                    speed = 0f;
                    animator.SetBool("Minning", false);
                    animator.SetBool("forword", false);
                    index = 0;
                }
                else
                {
                    speed = 3f;
                    stopTime = 100;
                    animator.SetBool("Minning", false);
                    animator.SetBool("forword", true);
                }

                isMinning = false;

            }
        }

      
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "RowResources")
        {
            Debug.Log("Exit");
        }

    }
}
