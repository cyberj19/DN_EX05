using System;
using System.Text;
using C17_Ex05.BasicDataTypes;

namespace C17_Ex05.Utils
{
    static class ConsoleUtils
    {
        private const string k_YesStr = "yes";
        private const string k_NoStr = "no";
        private const uint k_StartingBoardRowIndex = 1;
        private const char k_BoardColumnsDelimiter = '|';
        private const uint k_NumberOfBorderDelimetersPerColumn = 4;
        private const char k_BorderChar = '=';

        // Write board's prefix line (Top-celling titles)
        private static void writeBoardPrefixLine(uint i_Length)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append(" ");
            for (uint i = 1; i <= i_Length; i++)
            {
                stringBuilder.AppendFormat(" {0}  ", i);
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        public static void DrawBoard<T>(Board<T> i_Board)
        {
            StringBuilder boardStrBuilder = new StringBuilder();

            writeBoardPrefixLine(i_Board.Cols);
            for (uint currRow = k_StartingBoardRowIndex; currRow <= i_Board.Rows; currRow++)
            {
                boardStrBuilder.AppendFormat("{0}{1}", currRow, k_BoardColumnsDelimiter);
                for (uint currCol = 0; currCol < i_Board.Cols; currCol++)
                {
                    string cellStr = i_Board.Get(new Point(currCol, currRow - k_StartingBoardRowIndex)).ToString();

                    boardStrBuilder.AppendFormat(" {0} {1}", cellStr, k_BoardColumnsDelimiter);
                }

                boardStrBuilder.AppendLine(string.Empty); // newline
                // generate line border
                for (uint currCol = 0; currCol <= (i_Board.Cols * k_NumberOfBorderDelimetersPerColumn); currCol++)
                {
                    boardStrBuilder.Append(k_BorderChar);
                }

                // add another one at the end of the line
                boardStrBuilder.Append(k_BorderChar);
                boardStrBuilder.AppendLine(string.Empty); // newline
            }

            Console.Write(boardStrBuilder.ToString());
        }
        
        // Get a positive number from the user, in 'i_InputRange' range
        public static uint GetPositiveNumberFromUser(string i_MessageForUser, PositiveRange i_InputRange)
        {
            return (uint)GetPositiveNumberFromUser(i_MessageForUser, i_InputRange, null);
        }

        // Get a positive number from the user. If the user inserts 'i_ExcludingStr', the function will return null instead
        public static uint? GetPositiveNumberFromUser(string i_MessageForUser, PositiveRange i_InputRange, string i_ExcludingStr)
        {
            string userInputStr;
            uint currUserNumericInput;
            bool isValidInput;
            uint? retUserInput = null;

            System.Console.WriteLine(i_MessageForUser);
            do
            {
                userInputStr = System.Console.ReadLine();
                if ((i_ExcludingStr != null) && (i_ExcludingStr == userInputStr))
                {
                    break;
                }

                isValidInput = uint.TryParse(userInputStr, out currUserNumericInput) && i_InputRange.IsInRange(currUserNumericInput);
                if (!isValidInput)
                {
                    System.Console.WriteLine("Invalid input! Please try again:");
                }
                else
                {
                    retUserInput = currUserNumericInput;
                }
            }
            while (!isValidInput);

            return retUserInput;
        }

        // Prompt user for yes/no question
        public static bool PromptQuestion(string i_Question)
        {
            string lastInput = null;

            Console.Write(i_Question);
            Console.WriteLine(" Please choose: yes/no:");
            while ((lastInput == null) || ((lastInput != k_YesStr) && (lastInput != k_NoStr)))
            {
                if (lastInput != null)
                {
                    Console.WriteLine("Bad value. Please insert yes/no:");
                }

                lastInput = System.Console.ReadLine().ToLower();
            }

            return lastInput == k_YesStr;
        }

        // Get's point input from the user.
        // 'i_ExcludingStr' is another input that we can recv. in that case will return null
        public static Point? GetPointFromUser(TwoDimensionalPositiveRange i_Range, string i_ExcludingStr, uint i_DecreaseBY)
        {
            Point? retPoint = null;
            uint? row = GetPositiveNumberFromUser("Insert Row Number:", i_Range.Y, i_ExcludingStr);
            uint? col = null;

            if (row.HasValue)
            {
                col = GetPositiveNumberFromUser("Insert Col Number:", i_Range.X, i_ExcludingStr);
            }

            if (row.HasValue && col.HasValue)
            {
                retPoint = new Point(col.Value - i_DecreaseBY, row.Value - i_DecreaseBY);
            }

            return retPoint;
        }
    }
}
