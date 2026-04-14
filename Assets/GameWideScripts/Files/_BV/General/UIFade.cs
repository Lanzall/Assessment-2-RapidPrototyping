using UnityEngine;

public class UIFade : MonoBehaviour
{
    public void FadeIn(CanvasGroup _cvg) => FadeX.FadeIn(_cvg);
    public void FadeOut(CanvasGroup _cvg) => FadeX.FadeOut(_cvg);
}
