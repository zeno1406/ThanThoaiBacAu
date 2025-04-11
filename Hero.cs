public class Hero
{
    public int ID { get; private set; }
    public int HP { get; set; }
    public int Drachma { get; set; }
    public int GreenMeteors { get; set; }
    public int RedMeteors { get; set; }
    public int NumberOfTitanDefeated { get; set; }

    public Hero(int id, int drachma)
    {
        ID = id;
        Drachma = drachma;
        GreenMeteors = 0;
        RedMeteors = 0;
        TitanDefeated = 0;

        switch (id)
        {
            case 1: HP = 999; break; //Perseus
            case 2: HP = 900; break; //Theses
            case 3: HP = 888; break; //Odysseus
            case 4: HP = 777; break; //Hercules
        }
    }
}