namespace C17_Ex05.BasicDataTypes
{
    struct Point
    {
        private uint m_X;
        private uint m_Y;

        public uint X
        {
            get
            {
                return m_X;
            }

            set
            {
                m_X = value;
            }
        }

        public uint Y
        {
            get
            {
                return m_Y;
            }

            set
            {
                m_Y = value;
            }
        }

        public Point(uint i_X, uint i_Y)
        {
            m_X = i_X;
            m_Y = i_Y;
        }
    }
}
