namespace UT99BotRandomizer
{
    public class Appearance
    {
        public string model;
        public string skin;
        public string face;
        public string voice;
        public string name;

        public override bool Equals(object obj)
        {
            if (obj is not Appearance)
            {
                return false;
            }

            return name == ((Appearance)obj).name;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override string ToString()
        {
            return name;
        }
    }
}