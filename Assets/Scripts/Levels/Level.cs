using System.Collections.Generic;

public class Level
{
    public string Name { get; set; }
    public int Waves { get; set; }
    public List<Spawn> Spawns { get; set; }
}

public class Spawn
{
    public string Enemy { get; set; }
    public string Count { get; set; }
    public string HP { get; set; }
    public string Speed { get; set; }
    public string Damage { get; set; }
    public List<int> Sequence { get; set; }
    public string Location { get; set; }
    public string Delay { get; set; }
}