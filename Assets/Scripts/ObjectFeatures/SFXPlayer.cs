using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaCactus.ObjectFeatures {
    public class SFXPlayer : MonoBehaviour {
        [SerializeField]
        AudioSource source;
        [SerializeField]
        AudioClip[] sfx;

        int current = 0;

        public void PlayNext() {
            current = (current + 1) % sfx.Length;
            source.PlayOneShot(sfx[current]);
        }

        public void PlayRandom() {
            current = Random.Range(0, sfx.Length);
            source.PlayOneShot(sfx[current]);
        }

        public void PlayPrevious() {
            current--;
            if (current < 0) {
                current = sfx.Length - 2;
            }
            source.PlayOneShot(sfx[current]);
        }

        public void PlayOffset(int offset) {
            int playAt = current + offset;
            if (playAt < 0) {
                playAt = sfx.Length-1 + playAt;
            }
            playAt = playAt % sfx.Length;
            source.PlayOneShot(sfx[playAt]);
        }
    }
}
