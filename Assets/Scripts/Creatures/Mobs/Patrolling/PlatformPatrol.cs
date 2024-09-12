using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PixelCrew.Creatures
{

    public class PlatformPatrol : Patrol
    {
        [SerializeField] private LayerMask _layerCheck;
        public override IEnumerator DoPatrol()
        {
            throw new System.NotImplementedException();
        }
    }

}