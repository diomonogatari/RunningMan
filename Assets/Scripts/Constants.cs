
namespace Assets.Scripts
{
    public static class Constants
    {
        public static class AnimationVariables
        {
            public const string stopRunningTrigger = "StopRunning";
        }

        public static class Input
        {
            public const string horizontal = "Horizontal";
            public const string jump = "Jump";
        }

        public static class Scenes
        {
            public const string game = "Game";
            public const string homeMenu = "Menu";
        }

        public static class Tags
        {
            public const string enemy = "Enemy";
            public const string gameController = "GameController";
            public const string mainCamera = "MainCamera";
            public const string player = "Player";
            public const string newPhase = "NewPhase";
            public const string evenFaster= "EvenFaster";
            public const string finish = "Finish";
            public const string sun = "Sun";
            public const string ground = "Ground";
            public const string notGround = "NotGround";
        }

        public static class Prefs
        {
            public const string score = "Score";
        }
        public static class Collectables
        {
            public const string coin = "GoldCoin";
        }

        public static class TimeUnits
        {
            public const float second = 1;
            public const float minute = second * 60;
            public const float hour = minute * 60;
            public const float day = hour * 24;
        }
    }
}
