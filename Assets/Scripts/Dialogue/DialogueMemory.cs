using UnityEngine;

public static class DialogueMemory {
    public static bool personOneTalkedTo;


    public static bool AllEvidenceInLevel1()
    {
        return personOneTalkedTo;
    }
}
