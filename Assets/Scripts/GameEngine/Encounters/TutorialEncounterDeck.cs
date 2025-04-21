using System.Collections.Generic;
using System.Linq;
using GameEngine.EncounterData;
using GameEngine.Encounters.EncounterData;

namespace GameEngine.Encounters
{
    public class TutorialEncounterDeck : EncountersDeck
    {
        private string TUTORIAL_PREFAB_PATH = "Prefabs/Tutorial/Tutorial";
        private List<Encounter> deck = new();
        private int currentEncounter;
        
        public TutorialEncounterDeck()
        {
            deck.Add(
                new Encounter(
                    0,
                    new[] { Tags.Tutorial }.ToList(),
                    TUTORIAL_PREFAB_PATH,
                    new TutorialExecutable(),
                    new DoubleTextData("", "click to continue")
                )
            );
            // deck.Add(
            //     new Encounter(
            //         0,
            //         new[] { Tags.Tutorial }.ToList(),
            //         TUTORIAL_PREFAB_PATH,
            //         new TutorialExecutable(),
            //         new DoubleTextData("Congrats! You reached the depths of the internet., ", "click to continue")
            //     )
            // );
            // deck.Add(
            //     new Encounter(
            //         0,
            //         new[] { Tags.Tutorial }.ToList(),
            //         TUTORIAL_PREFAB_PATH,
            //         new TutorialExecutable(),
            //         new DoubleTextData(
            //             "Here you may encounter stressful content. Watch your stress meter, or you will rage quit :)",
            //             "click to continue"
            //         ))
            // );
            // deck.Add(
            //     new Encounter(
            //         0,
            //         new[] { Tags.Tutorial }.ToList(),
            //         TUTORIAL_PREFAB_PATH,
            //         new TutorialExecutable(),
            //         new DoubleTextData(
            //             "National Health Association states if your comments get more likes then post, you get your stress relieved!", ""
            //         ))
            //     );
            // deck.Add(
            //     new Encounter(
            //         4,
            //         new[] { Tags.Tutorial, Tags.Stressful, Tags.Blocking }.ToList(),
            //         TUTORIAL_PREFAB_PATH,
            //         new EnemyExecutable(4),
            //         new DoubleTextData(
            //             "Don't believe? Try it!", "Open comments to post yours ------->"
            //         ))
            //     );
            //
            deck.Add(
                new Encounter(
                    0,
                    new[] { Tags.Tutorial }.ToList(),
                    TUTORIAL_PREFAB_PATH,
                    new TutorialExecutable(),
                    new DoubleTextData(
                        "To completely relax, and reach 0 stress, you need to comment!", "Click to continue"
                    )));
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