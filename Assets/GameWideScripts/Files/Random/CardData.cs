using UnityEngine;


[CreateAssetMenu(fileName ="Card", menuName ="Cards", order = 1)]
public class CardData : ScriptableObject
{
    public CardID cardID;
    public Suite suite;
    [Range(1,10)]
    public int value;
    public Color colour;
    public string description;
    public Sprite icon;
}

public enum Suite { Diamond, Heart, Club, Spade}
public enum CardID { Dinosaur, Elephant, Crocodile, Monkey, Jester, Keyboard}
