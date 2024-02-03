using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class TokenInstance : MonoBehaviour
    {
        public AudioClip tokenCollectAudio;
        public bool randomAnimationStartTime = false;
        public Sprite[] idleAnimation, collectedAnimation;

        internal Sprite[] sprites = new Sprite[0];
        internal SpriteRenderer _renderer;

        internal int tokenIndex = -1;
        internal TokenController controller;
        internal int frame = 0;
        internal bool collected = false;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            if (randomAnimationStartTime)
                frame = Random.Range(0, sprites.Length);
            sprites = idleAnimation;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.gameObject.GetComponent<PlayerController>();
            if (player != null) OnPlayerEnter(player);
        }

        void OnPlayerEnter(PlayerController player)
        {
            if (collected) return;

            frame = 0;
            sprites = collectedAnimation;
            if (controller != null)
                collected = true;

            // Increment player's experience by 1 (you need to replace Experience with your actual experience script).
            player.GetComponent<Experience>().IncrementExperience(20);


            GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>().value = player.GetComponent<Experience>().GetExperiencePercentage();

            var ev = Schedule<PlayerTokenCollision>();
            ev.token = this;
            ev.player = player;
        }
    }
}