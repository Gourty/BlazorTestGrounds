using System.Diagnostics.Contracts;
using System.Reflection.Metadata.Ecma335;
using BlazorWebAssembly.TicTacToe.Enums;

namespace BlazorWebAssembly.TicTacToe;

public class TicTacToeBoard
{

    public TicTacToeBoard(int boardSize = 3, Symbol turn = Symbol.X)
    {
        BoardSize = boardSize;
        Turn = turn;
        CreateBoard();
    }

    public int BoardSize { get; }
    public Symbol Turn { get; set; }
    public TicTacToeSquare[,] TicTacToeSquares { get; set; }
    public Symbol? Winner { get; private set; }
    public bool IsGameOver { get; set; }

    public void MarkSquare(int row, int col)
    {
        if (!IsValidId(row) || !IsValidId(col) || TicTacToeSquares[row, col].Symbol != null)
        {
            return;
        }
        TicTacToeSquares[row, col].AssignSymbol(Turn);
        if (CheckWin(row, col, Turn))
        {
            Winner = Turn;
            IsGameOver = true;
            return;
        }
        if (GetFreeSquares().Count == 0)
        {
            IsGameOver = true;
        }
    }

    public bool CheckWin(int row, int col, Symbol symbol)
    {

        return CheckRow(symbol, row) || CheckCol(symbol, col) || CheckDiagonalsaRight(symbol) || CheckDiagonalsLeft(symbol, 0, BoardSize - 1);
    }

    public Symbol? GetSquareSymbol(int row, int col)
    {
        if (IsValidId(row) && IsValidId(col))
        {

            return TicTacToeSquares[row, col].Symbol;
        }

        return null;
    }

    public void SwapTurn()
    {
        switch (Turn)
        {
            case Symbol.X:
                Turn = Symbol.O;
                break;
            case Symbol.O:
                Turn = Symbol.X;
                break;
            default:
                break;
        }
    }

    public List<TicTacToeSquare> GetFreeSquares()
    {
        return TicTacToeSquares.Cast<TicTacToeSquare>().Where(x => x.Symbol is null).ToList();
    }

    public bool CheckRow(Symbol symbol, int row)
    {
        for (int col = 0; col < BoardSize; col++)
        {
            if (TicTacToeSquares[row, col].Symbol != symbol)
                return false;
        }

        return true;
    }

    public bool CheckCol(Symbol symbol, int col)
    {
        for (int row = 0; row < BoardSize; row++)
        {
            if (TicTacToeSquares[row, col].Symbol != symbol)
                return false;
        }
        return true;
    }

    public bool CheckDiagonalsaRight(Symbol symbol, int row = 0, int col = 0)
    {
        var result = true;
        if (IsValidId(row) && IsValidId(col))
        {
            var t = TicTacToeSquares[row, col].Symbol;
            if (t.HasValue && t == symbol)
            {
                result = CheckDiagonalsaRight(symbol, row + 1, col + 1);
            }
            else
            {
                return false;
            }
        }
        return result;
    }

    public bool CheckDiagonalsLeft(Symbol symbol, int row, int col)
    {
        var result = true;
        if (IsValidId(row) && IsValidId(col))
        {
            var t = TicTacToeSquares[row, col].Symbol;
            if (t.HasValue && t == symbol)
            {
                result = CheckDiagonalsLeft(symbol, row + 1, col - 1);
            }
            else
            {
                return false;
            }
        }
        return result;
    }

    public void Restart()
    {
        Winner = null;
        IsGameOver = false;
        Turn = Symbol.X;
        CreateBoard();
    }

    private bool IsValidId(int id)
    {
        return id >= 0 && id < BoardSize;
    }

    private void CreateBoard()
    {
        TicTacToeSquares = new TicTacToeSquare[BoardSize, BoardSize];

        for (int row = 0; row < BoardSize; row++)
        {
            for (int col = 0; col < BoardSize; col++)
            {
                TicTacToeSquares[row, col] = new TicTacToeSquare(row, col);
            }
        }

    }
}
