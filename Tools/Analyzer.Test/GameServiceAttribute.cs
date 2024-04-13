using System;


namespace Game
{
    public abstract class GameService
    {

    }
    public class GameServiceAttribute : Attribute
    {
        public GameServiceAttribute(GameServiceLifeSpan lifeSpan)
        {
            LifeSpan = lifeSpan;
        }

        public GameServiceLifeSpan LifeSpan { get; }
    }
    public enum GameServiceLifeSpan
    {
        None
    }
}
