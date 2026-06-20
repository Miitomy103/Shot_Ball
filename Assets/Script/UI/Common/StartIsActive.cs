using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIsActive : MonoBehaviour
{
    [SerializeField] bool isActive = true;

    private void Start()
    {
        gameObject.SetActive(isActive);
    }
}
