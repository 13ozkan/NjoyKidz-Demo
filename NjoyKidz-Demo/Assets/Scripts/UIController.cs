using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Instance;
using DG.Tweening;

public class UIController : MonoSingleton<UIController>
{
    [SerializeField] private TextMeshProUGUI _sbText;
    [SerializeField] private GameObject _sbImageGO;
    [SerializeField] private Button _rebuildButton;
    [SerializeField] private TMP_InputField _numberGrid;

    private int _score;


    private void Start()
    {
        _rebuildButton.onClick.AddListener(() =>
        {
            GridController.Instance.n = Convert.ToInt32(_numberGrid.text);
            GridController.Instance.CreateGrid();
        });
    }

    private void Update()
    {
        RotateCoin();
    }

    public void AddScore()
    {
        _score++;
        _sbText.text = $"{_score}";
    }

    public void RotateCoin()
    {
        _sbImageGO.transform.DOLocalRotate(new Vector3(0,-360, 0), 2f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear);
    }

}
