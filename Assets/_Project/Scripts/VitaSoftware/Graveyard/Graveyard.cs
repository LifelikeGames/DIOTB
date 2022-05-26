using UnityEngine;

namespace VitaSoftware.Graveyard
{
    public class Graveyard : MonoBehaviour
    {
        [SerializeField] private GraveyardManager graveyardManager;
        [SerializeField] private Transform gravestoneParent;
        [SerializeField] private Transform[] spots;

        private void Awake()
        {
            if(graveyardManager!=null)
                graveyardManager.Initialise(spots, gravestoneParent);
        }
    }
}