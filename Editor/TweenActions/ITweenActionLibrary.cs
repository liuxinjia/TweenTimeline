namespace Cr7Sund.TweenTimeLine
{
    public interface ITweenActionLibrary
    {
        void AddEffect(TweenActionEffect effect, string category = "Custom");
        void ApplyToSettings();
    }
}
