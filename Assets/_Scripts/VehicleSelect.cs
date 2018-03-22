using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSelect : MonoBehaviour
{
    Dropdown ddVehSelect;
    string ddVehOption;
    int ddVehValue;
  
    public GameObject vehicle;
    public GameObject jeep;
    public GameObject truck;

    void Start()
    {
        ddVehSelect = GetComponent<Dropdown>();
        vehicle = GameObject.FindGameObjectWithTag("Jeep");
        vehSelection();
    }

    void Update()
    {
        vehSelection();
    }

    public void vehSelection()
    {
        ddVehValue = ddVehSelect.value;
        ddVehOption = ddVehSelect.options[ddVehValue].text;

        if (ddVehOption == "Jeep")
        {
            vehicle = jeep;
            //Debug.Log(vehicle);
            jeep.SetActive(true);
            truck.SetActive(false);
        }
        else if (ddVehOption == "Monster Truck")
        {
            vehicle = truck;
            //Debug.Log(vehicle);
            truck.SetActive(true);
            jeep.SetActive(false);
        }
        //Debug.Log(vehInit.vehicle);
    }
}