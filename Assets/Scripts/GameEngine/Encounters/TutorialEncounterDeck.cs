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

        public TutorialEncounterDeck(Tutorial.TutorialSequence tutorial)
        {
            deck.Add(
                    new Encounter(0, new[] { Tags.NO_UI }.ToList(), "Prefabs/TitleScreen", null,
                        new TitleExecutable())
            );

            deck.Add(new Encounter(
                0,
                new[] { Tags.Tutorial, Tags.NO_COMMENTS }.ToList(),
                TUTORIAL_PREFAB_PATH,
                null,
                new TutorialExecutable(tutorial),
                new TextPrefabData("Prefabs/Tutorial/door_open", "Enter the dungeon")
            ));
            
            deck.Add(new Encounter(
                0,
                new[] { Tags.Tutorial, Tags.NO_COMMENTS }.ToList(),
                TUTORIAL_PREFAB_PATH,
                null,
                new TutorialEmptyExecutable(),
                new TextPrefabData("Prefabs/Tutorial/door_open 1", "Enter the dungeon")
            ));
            
            deck.Add(new Encounter(
                0,
                new[] { Tags.Tutorial, Tags.NO_COMMENTS }.ToList(),
                TUTORIAL_PREFAB_PATH,
                null,
                new TutorialEmptyExecutable(),
                new TextPrefabData("Prefabs/Tutorial/door_open 2", "Enter the dungeon")
            ));
            
            deck.Add(
                new Encounter(
                    5,
                    new[] { Tags.Tutorial, Tags.Stressful, Tags.Blocking }.ToList(),
                    TUTORIAL_PREFAB_PATH,
                    null,
                    new TutorialEncounterExecutable(tutorial, 5),
                    new TextPrefabData(
                        "Prefabs/Tutorial/door2", ""
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