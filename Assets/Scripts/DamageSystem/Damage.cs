using UnityEngine;

public class Damage 
{
    public int amount;
    public enum Type
    {
        PHYSICAL, ARCANE, NATURE, FIRE, ICE, DARK, LIGHT
    }
    public Type type;
    public Damage(int amount, Type type)
    {
        this.amount = amount;
        this.type = type;
    }

    public static Type TypeFromString(string type)
    {
        string t = type.ToLower();
        if (t == "arcane") return Type.ARCANE;
        if (t == "nature") return Type.NATURE;
        if (t == "fire") return Type.FIRE;
        if (t == "ice") return Type.ICE;
        if (t == "dark") return Type.DARK;
        if (t == "light") return Type.LIGHT;
        return Type.PHYSICAL;
    }
}
