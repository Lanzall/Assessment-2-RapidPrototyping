using UnityEngine;

public class TVManager : MonoBehaviour
{
    public TVInteraction[] allTVs;
    public TVInteraction currentTV;

    public void ActivateTV(TVInteraction tv)
    {
        currentTV = tv;

        foreach (var t in allTVs)
        {
            if (t != tv)
            {
                t.Deactivate();
            }
        }
    }
}
