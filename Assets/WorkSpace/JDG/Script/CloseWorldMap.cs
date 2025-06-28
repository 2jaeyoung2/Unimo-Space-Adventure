using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWorldMap : MonoBehaviour
{
    [SerializeField] GameObject _main;
    [SerializeField] GameObject _worldMap;
    [SerializeField] GameObject _root;

    public void ClickButton()
    {
        _main.SetActive(true);
        _worldMap.SetActive(false);
        _root.SetActive(false);
    }
}
