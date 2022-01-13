using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CalculateManager : MonoBehaviour
{
    public static CalculateManager instance = new CalculateManager();

    [SerializeField] private TMP_InputField _startCapital;
    [SerializeField] private TMP_InputField _yearCount;
    [SerializeField] private TMP_InputField _percentGrowth;
    [SerializeField] private TMP_InputField _mounthInvest;

    [SerializeField] private ToggleGroup _typeCalculate;

    [SerializeField] private TMP_Text _resaultField;
    [SerializeField] private TMP_Text _errorReport;

    [SerializeField] private bool _isFieldsEmpty => (_startCapital.text == _emptyField || _yearCount.text == _emptyField || _percentGrowth.text == _emptyField || _mounthInvest.text == _emptyField);

    private const string _emptyField = "";

    public void ResetResult()
    {

        if (_isFieldsEmpty)
        {
            _errorReport.text = "Пожалуйста, заполните все поля!";
            _resaultField.text = _emptyField;

            foreach (var tab in TabGroup.instance.tabs)
            {
                tab.SetEnebled(false);
            }
        }
        else
        {
            _errorReport.text = _emptyField;
            float startCapital = float.Parse(_startCapital.text);
            float yearCount = float.Parse(_yearCount.text);
            float percentGrowth = float.Parse(_percentGrowth.text);
            float mounthInvest = float.Parse(_mounthInvest.text);

            float currentSumm = startCapital;
            float sumInvested = startCapital;

            int number = int.Parse(_typeCalculate.ActiveToggles().First().name[_typeCalculate.ActiveToggles().First().name.Length - 1].ToString());

            YearSumManager.instance.ResetYearField();
            for (int i = 0; i < yearCount; i++)
            {
                switch (number)
                {
                    case 1:
                        currentSumm += (12 * mounthInvest);
                        currentSumm += (currentSumm * percentGrowth) / 100;
                        break;
                    case 2:
                        currentSumm += (currentSumm * percentGrowth) / 100 + (12 * mounthInvest);
                        break;
                    default:
                        break;
                }
                sumInvested += (12 * mounthInvest);
                YearSumManager.instance.CreateYearField(i + 1, currentSumm);
            }

            PieGraph.instance.MakeGraph(sumInvested, currentSumm - sumInvested); 
            MoreInfo.instance.CreateMoreInfo(sumInvested, currentSumm - sumInvested, percentGrowth); 

            _resaultField.text = CorrectForm(currentSumm);

            foreach (var tab in TabGroup.instance.tabs)
            {
                tab.SetEnebled(true);
            }
        }
    }

    public string CorrectForm(float currentSumm)
    {
        string MoneyPreArray = currentSumm.ToString("N", CultureInfo.CreateSpecificCulture("fr-CA"));
        int MoneyArray = MoneyPreArray.IndexOf(",");
        if (MoneyArray > 0)
            MoneyPreArray = MoneyPreArray.Substring(0, MoneyArray);

        return MoneyPreArray;
    }
    
    public string CorrectForm(double currentSumm)
    {
        string MoneyPreArray = currentSumm.ToString("N", CultureInfo.CreateSpecificCulture("fr-CA"));
        int MoneyArray = MoneyPreArray.IndexOf(",");
        if (MoneyArray > 0)
            MoneyPreArray = MoneyPreArray.Substring(0, MoneyArray);

        return MoneyPreArray;
    }
}
