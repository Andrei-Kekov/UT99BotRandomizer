using System;

namespace UT99BotRandomizer
{
    public class StatRandomizer
    {
        private Random _rng;
        private const float SkillMin = -3f;
        private const float SkillMax = 3f;
        private const float SkillIncrement = 1f;
        private const int SkillSteps = 6;
        private const float AccuracyMin = -1f;
        private const float AccuracyMax = 1f;
        private const float AccuracyIncrement = 0.05f;
        private const int AccuracySteps = 40;
        private const float AlertnessMin = -1f;
        private const float AlertnessMax = 1f;
        private const float AlertnessIncrement = 0.05f;
        private const int AlertnessSteps = 40;
        private const float CampingMin = 0f;
        private const float CampingMax = 1f;
        private const float CampingIncrement = 0.1f;
        private const int CampingSteps = 10;
        private const float StrafingMin = -1f;
        private const float StrafingMax = 1f;
        private const float StrafingIncrement = 0.25f;
        private const int StrafingSteps = 8;
        private const float CombatStyleMin = -1f;
        private const float CombatStyleMax = 1f;
        private const float CombatStyleIncrement = 0.5f;
        private const int CombatStyleSteps = 4;

        public StatRandomizer(Random rng) => _rng = rng;

        public float GetRandomSkill()
        {
            return SkillMin + SkillIncrement * _rng.Next(SkillSteps + 1);
        }

        public float GetRandomAccuracy()
        {
            return AccuracyMin + AccuracyIncrement * _rng.Next(AccuracySteps + 1);
        }
        public float GetRandomAlertness()
        {
            return AlertnessMin + AlertnessIncrement * _rng.Next(AlertnessSteps + 1);
        }
        public float GetRandomCamping()
        {
            return CampingMin + CampingIncrement * _rng.Next(CampingSteps + 1);
        }
        public float GetRandomStrafing()
        {
            return StrafingMin + StrafingIncrement * _rng.Next(StrafingSteps + 1);
        }

        public float GetRandomCombatStyle()
        {
            return CombatStyleMin + CombatStyleIncrement * _rng.Next(CombatStyleSteps + 1);
        }
    }
}