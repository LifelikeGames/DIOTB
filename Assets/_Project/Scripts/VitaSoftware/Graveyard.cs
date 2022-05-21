using System;
using UnityEngine;

namespace VitaSoftware
{
    public class Graveyard : MonoBehaviour
    {
        [SerializeField] private GraveyardManager graveyardManager;
        [SerializeField] private Transform[] spots;

        private void Awake()
        {
            if(graveyardManager!=null)
                graveyardManager.Initialise(spots);
        }
    }
}