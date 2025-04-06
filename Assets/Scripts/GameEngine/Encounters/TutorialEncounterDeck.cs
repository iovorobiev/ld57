using System.Collections.Generic;
using GameEngine.EncounterData;
using GameEngine.Encounters.EncounterData;

namespace GameEngine.Encounters
{
    public class TutorialEncounterDeck : EncountersDeck
    {
        private List<Encounter> deck = new();
        private int currentEncounter;
        
        public TutorialEncounterDeck()
        {
            deck.Add(
                new TutorialEncounter(0, "", "click to continue", hideComments: true)
                );
            deck.Add(
                new TutorialEncounter(0, "Congrats! You reached the depths of the internet.", "click to continue", hideComments: true)
                );
            deck.Add(new TutorialEncounter(0, "Here you may encounter stressful content. Watch your stress meter, or you will rage quit :)", "click to continue", "If this was for the first time...", hideComments: true));
            deck.Add(new TutorialEncounter(0, "National Health Association states if your comments get more likes then post, you get your stress relieved!", "", "Ah, fake news again...", hideComments: true));
            deck.Add(new EnemyEncounter(
                4, 
                new DoubleTextData("Don't believe? Try it!", "Open comments to post yours ------->"), 
                "Emm?..",
                "Prefabs/Enemy/DoubleTextEnemy",
                true));
            deck.Add(new TutorialEncounter(0, "To completely relax, and reach 0 stress, you need to comment!", "Click to continue", "Wow, this indeed felt better! I will comment until I reach 0 stress then..."));
        }
        
        public Encounter getCurrentEncounter()
        {
            return deck[currentEncounter];
        }

        public Encounter getNextEncounter()
        {
            return deck[currentEncounter + 1];
        }

        public void changePage()
        {
            currentEncounter++;
        }
        
        public bool isEmpty()
        {
            return currentEncounter == deck.Count - 1;
        }
    }
}