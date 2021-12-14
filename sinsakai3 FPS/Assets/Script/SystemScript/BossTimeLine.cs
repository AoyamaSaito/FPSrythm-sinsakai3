using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;

public class BossTimeLine : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    void OnEnable()
    {
        CinemachineBrain cineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        var binding = director.playableAsset.outputs.First(c => c.streamName == "Cinemachine Track");
        director.SetGenericBinding(binding.sourceObject, cineBrain);
    }
}
