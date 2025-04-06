namespace GameEngine.Encounters
{
    public interface EncountersDeck
    {
        Encounter getCurrentEncounter();

        Encounter getNextEncounter();

        bool isEmpty();
    }
}