using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoixCharacter : MonoBehaviour
{

    [SerializeField] private GameObject fermiere;
    [SerializeField] private GameObject fermier;

    public bool check;

    // Start is called before the first frame update
    void Start()
    {

        if (ParametresParties.Instance.charac == false)
        {
            fermiere.SetActive(true);
            fermier.SetActive(false);   
        }
        else
        {
            fermier.SetActive(true);
            fermiere.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
