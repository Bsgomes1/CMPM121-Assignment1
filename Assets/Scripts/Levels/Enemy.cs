public class Enemy
{
    public string Name { get; set; }
    public int Sprite { get; set; }
    public int HP { get; set; }
    public int Speed { get; set; }
    public int Damage { get; set; }

    public Enemy(string name, int sprite, int hp, int speed, int damage)
    {
        Name = name;
        Sprite = sprite;
        HP = hp;
        Speed = speed;
        Damage = damage;
    }
}