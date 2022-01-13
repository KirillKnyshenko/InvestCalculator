using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class YearSumManager : MonoBehaviour
{
    public static YearSumManager instance;

    public List<YearSum> fieldList;
    public GameObject shablon;
    public GameObject parent;
    [SerializeField] private Color color;

    private void Start()
    {
        instance = this;

        foreach (Transform child in parent.transform)
        {
            YearSum yearSum = child.GetComponent<YearSum>();
            if (yearSum != null)
            {
                fieldList.Add(yearSum);
            }
        }
    }

    public void CreateYearField(int year, float sum)
    {
        var field = Instantiate(shablon, parent.transform);

        if (year%2 == 0)
        {
            Image image = field.transform.GetComponent<Image>();
            if (image != null)
            {
                image.color = color;
            }
        }

        YearSum yearSum = field.GetComponent<YearSum>();
        fieldList.Add(yearSum);
        yearSum.yearText.text = year.ToString();
        string MoneyPreArray = sum.ToString("N", CultureInfo.CreateSpecificCulture("fr-CA"));
        int MoneyArray = MoneyPreArray.IndexOf(",");
        if (MoneyArray > 0)
            MoneyPreArray = MoneyPreArray.Substring(0, MoneyArray);
        yearSum.sumText.text = MoneyPreArray;
    } 
    
    public void ResetYearField()
    {
        for (int i = 0; i < fieldList.Count; i++)
        {
            Destroy(fieldList[i].gameObject);
        }

        fieldList = new List<YearSum>();
    }
}
