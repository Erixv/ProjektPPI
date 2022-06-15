using System;

namespace PPI
{
    class Robot : Space
    {
        protected int PositionX;
        protected int PositionY;

        private bool Start;
        public bool Message;

        private int FirstX;
        private int FirstY;

        public int State = 0;
        

        public Robot(int x, int y, int positionX, int positionY, int state)
        : base(x, y)
        {
            PositionX = FirstX = positionX;
            PositionY = FirstY = positionY;
            State = state;
            if ((positionX == 0) && (positionY == 0))
                Start = true;
            else
                Start = false;
        }
        public override void Create()
        {
            base.Create();
            for (int i = hight - 1; i >= 0; i--)
            {
                if (i < 10)
                {
                    Console.Write("0" + i + "   ");
                }
                else 
                    Console.Write( i + "   ");
                for (int j = 0; j < length; j++)
                {
                    if (PositionX == j && PositionY == i)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Display(State);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        if (position[j, i] == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("@  ");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.Write("O  ");
                        }

                    }
                }
                Console.WriteLine();
            }
            Console.Write("\nX/Y   ");
            for (int i = 0; i < length; i++)
            {
                if (i < 10)
                {
                    Console.Write(i + "  ");
                }
                else
                    Console.Write(i + " ");
            }
            Console.WriteLine("");
            if (Message == true)
            {
                ERROR();
            }
        }
        public void Display(int value)
        {
            State = value;
            switch (value)
            {
                case < 0:
                    Display(value + 4);
                    break;
                case >= 4:
                    Display(value - value %3);
                    break;
                case 0:
                    Console.Write(">  ");
                    break;

                case 1:
                    Console.Write("v  ");
                    break;

                case 2:
                    Console.Write("<  ");
                    break;
                case 3:
                    Console.Write("^  ");
                    break;

                default:
                    Console.Write("Incorrect value");
                    break;
            }
        }
        public void Move(int value, int movement)
        {
            State = value;
            if(value < 0) { Move(value +4,movement); } else if (value >= 4) { value = value%3; };
            switch (value)
            {

                case 0:
                    Right(movement);
                    break;

                case 1:
                    Down(movement);
                    break;

                case 2:
                    Left(movement);
                    break;
                case 3:
                    Up(movement);
                    break;

                default:
                    Console.WriteLine("Incorrect value");
                    break;
            }
        }


        public void Left(int vector)
        {
            if (space(-vector, 0))
            {
                for (int i = vector; i > 0; i--)
                {
                    SavePosition();
                    PositionX--;
                }
            }
            else
                Message = true;
        }
        public void Right(int vector)
        {
            if (space(vector, 0))
            {
                for (int i = vector; i > 0; i--)
                {
                    SavePosition();
                    PositionX++;
                }
            }
            else
                Message = true;
        }
        public void Up(int vector)
        {
            if (space(0, vector))
            {
                for (int i = vector; i > 0; i--)
                {
                    SavePosition();
                    PositionY++;
                }
            }
            else
                Message = true;
        }
        public void Down(int vector)
        {
            if (space(0, -vector))
            {
                for (int i = vector; i > 0; i--)
                {
                    SavePosition();
                    PositionY--;
                }
            }
            else
                Message = true;
        }
        private void ERROR()
        {
            Console.WriteLine("Invalid move");
        }
        private bool space(int LeftX, int LeftY)
        {
            if ((length > (PositionX + LeftX) && (PositionX + LeftX) >= 0) && (hight > (PositionY + LeftY) && (PositionY + LeftY) >= 0))
            {
                return true;
            }
            else
                return false;
        }
        public void Reset()
        {
            string quest;
            if (Start == true)
            { PositionX = 0; PositionY = 0; }
            else
            {
                do
                {
                    Console.WriteLine("Start from [0;0] ? Y/N");
                    quest = Console.ReadLine().ToUpper();
                } while (quest != "Y" && quest != "N");
                if (quest == "Y")
                { PositionX = 0; PositionY = 0; }
                else
                { PositionX = FirstX; PositionY = FirstY; }
            }
            Console.WriteLine("Position set to [" + PositionX + ";" + PositionY + "]");
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < hight; j++)
                {
                    position[i, j] = false;
                }
            }
        }
        private void SavePosition()
        {
            position[PositionX, PositionY] = true;
        }
        
        
    }
}
