using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace UT99BotRandomizer
{
    class Program
    {
        private const string appearancesFileName = "appearances.txt";
        private const string weaponsFileName = "weapons.txt";
        private const string userIniFileName = "User.ini";
        private static Random rng;
        private static StatRandomizer statRandomizer;
        private static Appearance[] randomAppearances;
        private static string[] weapons;

        static void Main(string[] args)
        {
            rng = new Random();
            statRandomizer = new StatRandomizer(rng);
            Appearance[] allAppearances = LoadAppearances();
            randomAppearances = RandomizeAppearances(allAppearances);
            weapons = LoadWeapons();
            Bot[] bots = CreateBots();
            UpdateUserIni(bots);
        }

        private static Appearance[] LoadAppearances()
        {
            string[] lines = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, appearancesFileName));
            string[] fields;
            var appearances = new Appearance[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                fields = lines[i].Split('/');
                appearances[i] = new Appearance();
                appearances[i].model = fields[0];
                appearances[i].skin = fields[1];
                appearances[i].face = fields[2];
                appearances[i].voice = fields[3];
                appearances[i].name = fields[4];
            }

            return appearances;
        }

        private static string[] LoadWeapons()
        {
            return File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, weaponsFileName));
        }

        private static Bot[] CreateBots()
        {
            var bots = new Bot[32];

            for (int i = 0; i < 32; i++)
            {
                bots[i] = new Bot();
                bots[i].appearance = randomAppearances[i];
                bots[i].color = GetRandomColor();
                bots[i].accuracy = statRandomizer.GetRandomAccuracy();
                bots[i].alertness = statRandomizer.GetRandomAlertness();
                bots[i].camping = statRandomizer.GetRandomCamping();
                bots[i].combatStyle = statRandomizer.GetRandomCombatStyle();
                bots[i].skill = statRandomizer.GetRandomSkill();
                bots[i].strafing = statRandomizer.GetRandomStrafing();
                bots[i].jumpy = rng.Next(2);
                bots[i].weapon = weapons[rng.Next(weapons.Length)];
            }

            return bots;
        }

        private static Appearance[] RandomizeAppearances(Appearance[] allAppearances)
        {
            var vacantAppearances = new List<Appearance>(allAppearances);
            var randomAppearances = new Appearance[32];
            int j;

            for (int i = 0; i < 32; i++)
            {
                j = rng.Next(vacantAppearances.Count);
                randomAppearances[i] = vacantAppearances[j];
                vacantAppearances.RemoveAt(j);
            }

            return randomAppearances;
        }

        private static int GetRandomColor()
        {
            int color = rng.Next(5);
            return color == 4 ? 255 : color;
        }

        private static void UpdateUserIni(Bot[] bots)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, userIniFileName);
            string[] lines = File.ReadAllLines(path);
            int i = 0;
            int j;
            NumberFormatInfo nfi = NumberFormatInfo.InvariantInfo;

            while (lines[i] != "[Botpack.ChallengeBotInfo]")
            {
                i++;
            }

            i++;

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = $"BotFaces[{j}]={bots[j].appearance.face}";
            }

            i += 3;

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = $"BotNames[{j}]={bots[j].appearance.name}";
            }

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = $"BotTeams[{j}]={bots[j].color}";
            }

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = $"BotClasses[{j}]={bots[j].appearance.model}";
            }

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = $"BotSkins[{j}]={bots[j].appearance.skin}";
            }

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = $"VoiceType[{j}]={bots[j].appearance.voice}";
            }

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = string.Format(nfi, "BotSkills[{0}]={1:N6}", j, bots[j].skill);
            }

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = string.Format(nfi, "BotAccuracy[{0}]={1:N6}", j, bots[j].accuracy);
            }

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = string.Format(nfi, "CombatStyle[{0}]={1:N6}", j, bots[j].combatStyle);
            }

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = string.Format(nfi, "Alertness[{0}]={1:N6}", j, bots[j].alertness);
            }

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = string.Format(nfi, "Camping[{0}]={1:N6}", j, bots[j].camping);
            }

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = string.Format(nfi, "StrafingAbility[{0}]={1:N6}", j, bots[j].strafing);
            }

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = $"FavoriteWeapon[{j}]={bots[j].weapon}";
            }

            for (j = 0; j < 32; i++, j++)
            {
                lines[i] = $"BotJumpy[{j}]={bots[j].jumpy}";
            }

            File.WriteAllLines(path, lines);
        }
    }
}