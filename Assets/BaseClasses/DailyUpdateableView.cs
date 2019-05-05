using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class DailyUpdateableView : View
    , IAnyDateListener
{
    void Start()
    {
        ListenDateChanges(this);
    }

    public abstract void Render();

    void IAnyDateListener.OnAnyDate(GameEntity entity, int date)
    {
        Render();
    }
}
