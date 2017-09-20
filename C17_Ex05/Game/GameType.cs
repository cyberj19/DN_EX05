using C17_Ex05.BasicDataTypes;

namespace C17_Ex05.Game
{
    class GameType
    {
        public enum eGameType
        {
            PlayerVsPlayer = 1,
            PlayerVsComputer = 2,
        }

        private static readonly PositiveRange sr_EnumRange = new PositiveRange(1, 2);
        private readonly eGameType r_GameType;
        
        public static uint MinVal
        {
            get
            {
                return sr_EnumRange.Min;
            }
        }

        public static uint MaxVal
        {
            get
            {
                return sr_EnumRange.Max;
            }
        }

        public eGameType Type
        {
            get
            {
                return r_GameType;
            }
        }

        public GameType(uint i_GameType)
        {
            r_GameType = (GameType.eGameType)i_GameType;
        }

        public GameType(eGameType i_GameType)
        {
            r_GameType = i_GameType;
        }
    }
}
