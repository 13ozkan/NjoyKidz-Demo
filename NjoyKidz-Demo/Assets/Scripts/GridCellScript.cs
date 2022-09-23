using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellScript : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickSound, _correctSound;
    [SerializeField] private GameObject _xSpriteGO;
    public int x, y;
    public bool isActive = false;
    

    private void OnMouseDown()
    {
        if (isActive)
            return;
        SetXSpriteGO(true);
        _audioSource.PlayOneShot(_clickSound);
        var neighbors = GridController.Instance.CheckNeighborhood(x, y);

        if (neighbors.Count < 3)
            return;

        foreach (var neighbor in neighbors)
        {
            neighbor.SetXSpriteGO(false);
        }
        _audioSource.PlayOneShot(_correctSound);
        UIController.Instance.AddScore();
    }

    private void SetXSpriteGO(bool active)
    {
        _xSpriteGO.SetActive(active);
        isActive = active;
    }
}
