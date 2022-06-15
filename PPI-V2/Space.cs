namespace PPI
{
    class Space
    {
        protected int length;
        protected int hight;
        protected bool[,] position;
        protected Space(int x, int y)
        {
            length = x;
            hight = y;
            position = new bool[x, y];
        }
        virtual public void Create() { }
    }
}