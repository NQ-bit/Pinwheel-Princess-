using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayCutscene : MonoBehaviour
{
    [SerializeField] PlayableDirector timeline;
    void playCutscene() {
        timeline.Play();
    }
}
