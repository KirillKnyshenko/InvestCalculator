using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PieGraph : MonoBehaviour
{
    public static PieGraph instance;

    [SerializeField] private Transform _pieGraph, _pointInvest, _pointGrow, _rotationInvestPoint, _rotationGrowPoint;
    [SerializeField] private Image _wedgeInvest, _wedgeGrow;
    [SerializeField] private TMP_Text _investText, _growText;

    private void Start()
    {
        instance = this;
    }

    public void MakeGraph(float invest, float grow)
    {
        float total = invest + grow;
        float zRotation = 0f;

        _wedgeInvest.fillAmount = invest / total;
        zRotation -= _wedgeInvest.fillAmount * 360f;

        _wedgeGrow.fillAmount = grow / total;
        _wedgeGrow.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, zRotation));

        float buttonAngle = _wedgeInvest.fillAmount * 360;
        _rotationInvestPoint.transform.eulerAngles = new Vector3(0, 0, -buttonAngle / 2);

        float newButtonAngle = _wedgeGrow.fillAmount * 360;
        _rotationGrowPoint.transform.eulerAngles = new Vector3(0, 0, -buttonAngle + (-newButtonAngle / 2));

        _investText.transform.position = (_pointInvest.transform.position + _pieGraph.transform.position) / 2;
        _investText.text = Math.Round(((invest / total) * 100), 2).ToString() + "%";

        _growText.transform.position = (_pointGrow.transform.position + _pieGraph.transform.position) / 2;
        _growText.text = Math.Round(((grow / total) * 100), 2).ToString() + "%";
    }
}
