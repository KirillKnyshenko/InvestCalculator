using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoreInfo : MonoBehaviour
{
    public static MoreInfo instance;

    [SerializeField] private TMP_Text _percentOfSum, _percentOfYear, _invested, _reinvested;
    [SerializeField] private TMP_Text _percentOfSumLabel, _percentOfYearLabel;

    private void Start()
    {
        instance = this;
    }

    public void CreateMoreInfo(float invest, float grow, float percent)
    {
        float total = invest + grow;
        float percentOfSum = (total * percent) / 100;

        _percentOfSumLabel.text = Math.Round(percent, 0) + "% от " + CalculateManager.instance.CorrectForm(total) + " =";
        _percentOfSum.text = CalculateManager.instance.CorrectForm(Math.Round(percentOfSum, 0));

        _percentOfYearLabel.text = CalculateManager.instance.CorrectForm(Math.Round(percentOfSum, 0)) + " / 12" + " =";
        _percentOfYear.text = CalculateManager.instance.CorrectForm(Math.Round((percentOfSum / 12), 0));

        _invested.text = CalculateManager.instance.CorrectForm(Math.Round(invest, 0));

        _reinvested.text = CalculateManager.instance.CorrectForm(Math.Round(grow, 0));
    }
}
