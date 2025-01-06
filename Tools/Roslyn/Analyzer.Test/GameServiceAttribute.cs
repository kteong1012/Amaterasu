using System;


namespace Game
{
    public abstract class GameService
    {

    }
    public class GameServiceAttribute : Attribute
    {
        public GameServiceAttribute(GameServiceDomain domain)
        {
            Domain = domain;
        }

        public GameServiceDomain Domain { get; }
    }
    public enum GameServiceDomain
    {
        None,
        Game
    }
}
