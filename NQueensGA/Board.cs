using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NQueensGA
{
    class Board
    {
        private int[] board;
        private int fitness;
        private int size;

        /// <summary>
        /// Board Constructor
        /// </summary>
        /// <param name="board">Represents a board</param>
        public Board(int[] board)
        {
            this.board = board;
            fitness = calculateFitness();
            size = board.Length;
        }

        /// <summary>
        /// calculateFitness - Determines the fitness of a board.
        /// </summary>
        /// <returns>Fitness of board</returns>
        public int calculateFitness()
        {
            int fitness = 0;
            bool result;

            /* Check each queen on the board, and if it is not
               attacking another queen, increase the fitness of
               the board by one.
            */
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 1; j < board.Length; j++)
                {
                    result = attacking(board[i], i, board[j], j);

                    if (result == false)
                    {
                        fitness++;
                    }
                }
            }

            return fitness;
        }

        /// <summary>
        /// solved - Determines if a board is solved
        /// </summary>
        /// <returns></returns>
        public bool solved()
        {
           bool result;

            for(int i = 0; i < board.Length; i++)
            {
                for(int j = 1; j < board.Length; j++)
                {
                    result = attacking(board[i], i, board[j], j);

                    if(result == true)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// attacking - Checks if two queens are attacking
        /// </summary>
        /// <param name="queenA">First queen to check</param>
        /// <param name="colA">Column of first queen</param>
        /// <param name="queenB">Second queen to check</param>
        /// <param name="colB">Column of second queen</param>
        /// <returns></returns>
        public Boolean attacking(int queenA, int colA, int queenB, int colB)
        {
            // If queen is checking itself
            if (colA == colB)
            {
                return false;
            }

            // Check diagonal attacks
            if (Math.Abs(queenB - queenA) == Math.Abs(colB - colA))
            {
                return true;
            }
            else if (queenA == queenB) // Check Row
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// mutate - Performs a mutation on a single queen
        /// </summary>
        public void mutate()
        {
            Random r = new Random();

            int randomPosition = r.Next(0, size);
            int randomQueenValue = r.Next(1, size + 1);

            board[randomPosition] = randomQueenValue;
        }

        /// <summary>
        /// getBoard - Returns the board as an array
        /// </summary>
        /// <returns>board</returns>
        public int[] getBoard()
        {
            return (int [])board.Clone();
        }

        /// <summary>
        /// getFitness - Returns the fitness of board
        /// </summary>
        /// <returns>Fitness of board</returns>
        public int getFitness()
        {
            return fitness;
        }

        /// <summary>
        /// toString - Returns string representation of board
        /// </summary>
        /// <returns>String representing board</returns>
        public String toString()
        {
            String str = String.Empty;

            for(int i = 0; i < board.Length; i++)
            {
                str += board[i] + " ";
            }

            return str;
        }

        /// <summary>
        /// printBoard - Prints a visual representation of board
        /// </summary>
        public void printBoard()
        {
            string str = String.Empty;

            for(int i = 0; i < board.Length; i++)
            {
                for(int j = 0; j < board.Length; j++)
                {
                    if(board[j] == i + 1)
                    {
                        str += "Q ";
                    }
                    else
                    {
                        str += ". ";
                    }
                }

                str += "\n\n";
            }

            Console.Out.Write(str);
        }
    }
}
