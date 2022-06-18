using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect_Items : MonoBehaviour
{
    [SerializeField]
    Transform Path;

    [SerializeField]
    GameObject Repository;

    [SerializeField]
    string nameOfResourses;

    [SerializeField]
    int revenue;
    public List<Transform> itemsToCollect;

    Animator animator;
    float speed = 5f;
    int index = 0;

    bool pickedUp = false;
    bool go = true;
    bool back = false;

     void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        collectItems();

    }

    private void collectItems()
    {
        switch (nameOfResourses) {
            case "RedCrystals":
                itemsToCollect = GameObject.Find("EventSystem").GetComponent<UiManager>().redToCollect;
                break;
            case "BlueCrystals":
                itemsToCollect = GameObject.Find("EventSystem").GetComponent<UiManager>().blueToCollect;
                break;
            case "Iron":
                itemsToCollect = GameObject.Find("EventSystem").GetComponent<UiManager>().ironToCollect;
                break;
            case "Gold":
                itemsToCollect = GameObject.Find("EventSystem").GetComponent<UiManager>().goldToCollect;
                break;
            case "Trees":
                itemsToCollect = GameObject.Find("EventSystem").GetComponent<UiManager>().treeToCollect;
                break;
        }
        if (itemsToCollect.Count > 0 && index < itemsToCollect.Count && !pickedUp)
        {
            
            if (go)
            {
                animator.SetBool("Minning", false);
                animator.SetBool("forword", true);
                transform.position = Vector3.MoveTowards(transform.position, Path.position, speed * Time.deltaTime);
                
                Debug.Log("goo");
            }
            else
            {
                animator.SetBool("Minning", false);
                animator.SetBool("forword", true);
                transform.position = Vector3.MoveTowards(transform.position,new Vector3(itemsToCollect[index].transform.position.x, 0.5f, itemsToCollect[index].transform.position.z) , speed * Time.deltaTime);
                Debug.Log("goo else");

            }
        }
        else if (pickedUp)
        {
            if (back)
            {
                animator.SetBool("Minning", false);
                animator.SetBool("forword", true);
                transform.position = Vector3.MoveTowards(transform.position,new Vector3(Repository.transform.position.x, 0.5f, Repository.transform.position.z), speed * Time.deltaTime);
                itemsToCollect[index].transform.position = Vector3.MoveTowards(itemsToCollect[index].transform.position, transform.position, speed * Time.deltaTime);

            }
            else
            {
                animator.SetBool("Minning", false);
                animator.SetBool("forword", true);
                transform.position = Vector3.MoveTowards(transform.position, Path.position, speed * Time.deltaTime);
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Repository")
        {
            

            print("Repository");
            PlayerPrefs.SetInt(nameOfResourses, PlayerPrefs.GetInt(nameOfResourses, 0) + revenue);
            Destroy(itemsToCollect[index].gameObject);
            

            index += 1;
            if (index == itemsToCollect.Count)
            {
                
                itemsToCollect.Clear();
                switch (nameOfResourses)
                {
                    case "RedCrystals":
                        GameObject.Find("EventSystem").GetComponent<UiManager>().redToCollect.Clear();
                        GameObject.Find("EventSystem").GetComponent<UiManager>().RedResourses.ForEach(Obj =>
                        {
                            Obj.gameObject.SetActive(true);
                        }
                        );
                        break;
                    case "BlueCrystals":
                        GameObject.Find("EventSystem").GetComponent<UiManager>().blueToCollect.Clear();
                        GameObject.Find("EventSystem").GetComponent<UiManager>().BlueResourses.ForEach(Obj =>
                        {
                            Obj.gameObject.SetActive(true);
                        }
                        );
                        break;
                    case "Iron":
                        GameObject.Find("EventSystem").GetComponent<UiManager>().ironToCollect.Clear();
                        GameObject.Find("EventSystem").GetComponent<UiManager>().IronResourses.ForEach(Obj =>
                        {
                            Obj.gameObject.SetActive(true);
                        }
                        );
                        break;
                    case "Gold":
                        GameObject.Find("EventSystem").GetComponent<UiManager>().goldToCollect.Clear();
                        GameObject.Find("EventSystem").GetComponent<UiManager>().GoldResourses.ForEach(Obj =>
                        {
                            Obj.gameObject.SetActive(true);
                        }
                        );
                        break;
                    case "Trees":
                        GameObject.Find("EventSystem").GetComponent<UiManager>().treeToCollect.Clear();
                        GameObject.Find("EventSystem").GetComponent<UiManager>().TreeResources.ForEach(Obj =>
                        {
                            Obj.gameObject.SetActive(true);
                        }
                        );
                        break;

                }
                index = 0;

            }
           

            pickedUp = false; 
            go = true;
            back = false;

        }

        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Collectables")
        {
            pickedUp = true;
            animator.SetBool("Minning", true);
            animator.SetBool("forword", false);

        }

        if (collision.gameObject.tag == "cube")
        {
            if (!pickedUp)
            {
                go = false;
            }
            else
            {
                back = true;
            }

        }
    }
}
