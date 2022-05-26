using System;
using UnityEngine;

namespace VitaSoftware.Tutorials
{
    public class TutorialLinker : MonoBehaviour
    {
        [SerializeField] private GameObject tutorialParent;
        [SerializeField] private GameObject relatedGameObject;

        private void Awake()
        {
            tutorialParent.transform.SetParent(relatedGameObject.transform);
        }
    }
}