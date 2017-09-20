namespace C17_Ex05.BasicDataTypes
{
    class TwoDimensionalPositiveRange
    {
        private PositiveRange m_RangeX;
        private PositiveRange m_RangeY;

        public PositiveRange X
        {
            get
            {
                return m_RangeX;
            }
        }

        public PositiveRange Y
        {
            get
            {
                return m_RangeY;
            }
        }

        public TwoDimensionalPositiveRange(uint i_MinX, uint i_MaxX, uint i_MinY, uint i_MaxY)
        {
            m_RangeX = new PositiveRange(i_MinX, i_MaxX);
            m_RangeY = new PositiveRange(i_MinY, i_MaxY);
        }
    }
}
