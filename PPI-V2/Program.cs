using System;

namespace PPI
{
    class Program
    {
        static void Main()
        {
            string Input, command;
            int end_possition;
            int NumValue = 0;
            bool is_number, is_command;
            
            Console.WriteLine("PPI\n");
            Robot Bot = Begin();
            Console.Clear();
            Bot.Create();
            int state = 0;
            while (true)
            {
                Console.Write("Enter fun:");
                Input = Console.ReadLine() + ";";
                Input = DeleteBlank(Input.ToUpper());
                is_command = false;
                Bot.Message = false;
                do
                {
                    end_possition = Input.IndexOf(';');
                    if (end_possition > -1)
                    {
                        command = Input.Remove(end_possition);     
                        is_number = ContainsNum(command);
                        if (is_number)                                          
                        {
                            NumValue = GetNum(command);             
                            command = GetFun(command);
                        }
                        switch (command) 
                        {
                            case "TURNLEFT":
                                is_command = true;
                                Bot.Display(state = state-1);
                                break;

                            case "TURNRIGHT":
                                is_command = true;
                                Bot.Display(state=state+1);
                                break;
                            case "FORWARD":
                                is_command = true;
                                if (is_number)
                                    Bot.Move(state,NumValue);
                                else
                                    Bot.Move(state,1);
                                break;
                            case "RESET":
                                is_command = true;
                                Bot.Reset();
                                break;

                            case "SKIP":
                                is_command = true;
                                break;

                            case "HELP":
                                Help();
                                break;

                            case "EXIT":
                                goto End;

                            default:
                                Console.WriteLine("Incorrect move - use 'help' function");
                                break;
                        }
                    }
                    Input = Input.Substring(end_possition + 1);
                } while (end_possition != -1);
                if (is_command)
                {
                    Console.Clear();
                    Bot.Create();
                }
            }
        End:
            Console.WriteLine("End");
            Console.ReadKey();
        }
        public static string DeleteBlank(string RawString)
        {
            string without_space = "";
            for (int i = 0; i < RawString.Length; i++)
            {
                if (RawString[i] != ' ')
                    without_space += RawString[i];
            }
            return without_space;
        }
        public static bool ContainsNum(string function)
        {
            for (int i = 0; i < function.Length; i++)
            {
                if (IsNum(function[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool IsNum(char user_c)
        {
            if (('0' <= user_c) && (user_c <= '9'))
            {
                return true;
            }
            return false;
        }
        public static int GetNum(string function)
        {
            int var = 0;
            string Temp = "";
            for (int i = 0; i < function.Length; i++)
            {
                if (IsNum(function[i]))
                {
                    Temp = function.Substring(i);
                    break;
                }
            }
            for (int i = 0; i < Temp.Length; i++)
            {
                if (!IsNum(Temp[i]))
                {
                    Temp = Temp.Remove(i);
                    break;
                }
            }
            for (int i = 0; i < Temp.Length; i++)
                var += (int)(ASCII(Temp[i]) * Math.Pow(10, Temp.Length - i - 1));
            return var;
        }
        public static int ASCII(int code)
        {
            int number = 0;
            int[] ascicode = { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57 };
            int[] val = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < ascicode.Length; i++)
            {
                if (code == ascicode[i])
                {
                    number = val[i];
                    break;
                }
            }
            return number;
        }
        public static string GetFun(string function)
        {
            string ReturnCmd = "";
            for (int i = 0; i < function.Length; i++)
            {
                if (IsNum(function[i]))
                {
                    ReturnCmd += function.Remove(i);
                    break;
                }
            }
            return ReturnCmd;
        }
        public static void Help()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Movement:");
            Console.WriteLine(" turn left\t\tturn right\n forward\t\tforward n\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" Functions:");
            Console.WriteLine(" reset\t\tskip\t\t exit");
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static Robot Begin()
        {
            int SizeX = 0,SizeY = 0, start_x = 0, start_y = 0;
            string answer, input_string;
            bool check;

            do
            {
                Console.WriteLine("Create field 8x8? Y/N");
                answer = Console.ReadLine().ToUpper();
            } while (answer != "Y" && answer != "N");
            if (answer == "Y")
            {
                SizeX = 8;
                SizeY = 8;
            }
            else
            {
                do
                {
                    Console.WriteLine("Enter field height [1;20]: ");
                    input_string = Console.ReadLine();
                    check = ContainsNum(input_string);
                    if (check)
                    {
                        SizeX = GetNum(input_string);
                    }
                } while ((check != true) || (check == true && SizeX > 20));
                Console.WriteLine("Heigth: " + SizeY);
                do
                {
                    Console.WriteLine("Enter field length [1;20]: ");
                    input_string = Console.ReadLine();
                    check = ContainsNum(input_string);
                    if (check)
                    {
                       SizeY = GetNum(input_string);
                    }
                } while ((check != true) || (check == true &&SizeY > 20));
                Console.WriteLine("Length: " +SizeY);
            }
            do
            {
                Console.WriteLine("Set starting position to [0;0] ? Y/N");
                answer = Console.ReadLine().ToUpper();
            } while (answer != "Y" && answer != "N");
            if (answer == "Y")
            {
                start_x = 0;
                start_y = 0;
            }
            else
            {
                do
                {
                    Console.WriteLine("Set starting heigth between values [0;" + (SizeX - 1) + "]");
                    input_string = Console.ReadLine();
                    check = ContainsNum(input_string);
                    if (check)
                    {
                        start_x = GetNum(input_string);
                    }
                } while ((check != true) || (check == true && start_x > (SizeX - 1)));
                do
                {
                    Console.WriteLine("Set starting heigth between values [0;" + (SizeY - 1) + "]");
                    input_string = Console.ReadLine();
                    check = ContainsNum(input_string);
                    if (check)
                    {
                        start_y = GetNum(input_string);
                    }
                } while ((check != true) || (check == true && start_y > (SizeY - 1)));
            }
            Robot temp = new Robot(SizeX,SizeY, start_x, start_y, 0);
            return temp;
        }
    }
}
