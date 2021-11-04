﻿using Assets.HeroEditor4D.Common.CharacterScripts;
using HeroEditor4D.Common.Enums;
using UnityEngine;

namespace Assets.HeroEditor4D.Common.ExampleScripts
{
    /// <summary>
    /// An example of how to change character's appearance.
    /// </summary>
    public class CharacterAppearance : MonoBehaviour
    {
        public Character4D Character;

        public void SetRandomAppearance()
        {
            SetRandomHair();
            SetRandomEyes();
            SetRandomMouth();
        }

        public void SetRandomHair()
        {
            var randomIndex = Random.Range(0, Character.SpriteCollection.Hair.Count);
            var randomItem = Character.SpriteCollection.Hair[randomIndex];

            Character.SetBody(randomItem, BodyPart.Hair);

            var randomColor = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));

            SetHairColor(randomColor);
        }

        public void SetHairColor(Color color)
        {
            Character.Parts.ForEach(i => i.HairRenderer.color = color);
        }

        public void SetRandomEyes()
        {
            var randomIndex = Random.Range(0, Character.SpriteCollection.Eyes.Count);
            var randomItem = Character.SpriteCollection.Eyes[randomIndex];

            Character.SetBody(randomItem, BodyPart.Eyebrows);
        }

        public void SetRandomMouth()
        {
            var randomIndex = Random.Range(0, Character.SpriteCollection.Mouth.Count);
            var randomItem = Character.SpriteCollection.Mouth[randomIndex];

            Character.SetBody(randomItem, BodyPart.Mouth);
        }
    }
}