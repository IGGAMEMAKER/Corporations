using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateView : View
{
    public Image Panel;
    public Text Text;

    public Image ThemeImage;

    int date;

    public override void ViewRender()
    {
        base.ViewRender();
    }

    public void SetEntity(int currDate)
    {
        date = currDate;

        Render();
    }

    void Render()
    {
        Panel.color = GetPanelColor(CurrentIntDate == date);

        var dateDescription = Format.GetDateDescription(date);

        Text.text = (dateDescription.day + 1) + "\n" + dateDescription.monthLiteral;
        // Format.FormatDate(date, false);

    }
}
