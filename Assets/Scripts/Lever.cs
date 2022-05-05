using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool isOn;
    void Update()
    {
        Animator anim = GetComponent<Animator>();
        Component comp = GetComponent<Component>(); bool isOn_bis = anim.GetBool("IsOn"); if (Input.GetMouseButtonDown(0)) // Clique gauche
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Select
                if (hit.transform.name == "LeverBaseTop" && hit.transform.name == comp.name)
                {
                    if (isOn_bis == false)
                    {
                        isOn = true;
                        anim.SetBool("IsOn", isOn);
                    }
                    if (isOn_bis == true)
                    {
                        isOn = false;
                        anim.SetBool("IsOn", isOn);
                    }
                }
                if (hit.transform.name == "LeverBaseBottom" && hit.transform.name == comp.name)
                {
                    if (isOn_bis == false)
                    {
                        isOn = true;
                        anim.SetBool("IsOn", isOn);
                    }
                    if (isOn_bis == true)
                    {
                        isOn = false;
                        anim.SetBool("IsOn", isOn);
                    }
                }
            }
        }
    }
}


