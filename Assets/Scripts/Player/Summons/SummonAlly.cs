using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonAlly : MonoBehaviour
{
    [SerializeField] private GameObject ally;

    private void OnEnable()
    {
        //Activates ally
        ally.SetActive(true);
    }
}
