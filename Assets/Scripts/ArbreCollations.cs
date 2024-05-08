using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.AI;

public class ArbreCollations : MonoBehaviour
{

    public GameObject[] collationPrefabs;
    private float tempRpduction; 
    private void Start()
    {
        tempRpduction = Time.time; 
    }
    private void Update()
    {
        //Verifie si il ya deja une collation a cote de l'arbre ou s'il est le temps
        if (Time.time - tempRpduction >= 3f && !DejaCollation())
        {
            Vector3 distance = UnityEngine.Random.insideUnitCircle * 2;
            Vector3 position = transform.position +new Vector3(distance.x, 0f, distance.y);

            GameObject collationPrefab = collationPrefabs[UnityEngine.Random.Range(0, collationPrefabs.Length)];
            Instantiate(collationPrefab, position, Quaternion.identity);
            tempRpduction = Time.time;
        }

       
    }

    /// <summary>
    /// Methode pour la detection de collation 
    /// </summary>
    /// <returns></returns>
    private bool DejaCollation()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Collation"))
            {
                return true;
            }
        }
        return false;
    }
}
