using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace PixelCrew.Components
{
    public class ScoreCounter : MonoBehaviour
    {
        public static float _score;

        public void SilverCounter()
        {
            _score += 1f;
            print(_score);
        }

        public void GoldCounter()
        {
            _score += 10f;
            print(_score);
            
        }

        public void StopCounter()
        {
            enabled = false;
        }


}
}