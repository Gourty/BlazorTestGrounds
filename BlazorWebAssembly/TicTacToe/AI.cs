using BlazorWebAssembly.TicTacToe.Enums;

namespace BlazorWebAssembly.TicTacToe;

public class AI
{
    private readonly Difficulty difficulty;

    public AI(Symbol symbol = Symbol.O, Difficulty difficulty = Difficulty.Hard)
    {
        Symbol = symbol;
        this.difficulty = difficulty;
    }

    public Symbol Symbol { get; }


    public (int row, int col) TakeTurn(TicTacToeBoard ticTacToeBoard)
    {

        switch (difficulty)
        {
            case Difficulty.Easy:
                return TakeRandomTurn(ticTacToeBoard);
            case Difficulty.Normal:
                return TakeNormalTurn(ticTacToeBoard);
            case Difficulty.Hard:
                return TakeHardTurn(ticTacToeBoard);
            default:
                return (0, 0);
        }
    }

    private (int row, int col) TakeRandomTurn(TicTacToeBoard ticTacToeBoard)
    {
        var t = ticTacToeBoard.GetFreeSquares();
        var result = t[new Random().Next(t.Count)].Id;
        Console.WriteLine(result);
        return result;
    }

    private (int row, int col) TakeNormalTurn(TicTacToeBoard ticTacToeBoard)
    {
        return WinningMove(ticTacToeBoard, Symbol) ?? TakeRandomTurn(ticTacToeBoard);
    }

    private (int row, int col) TakeHardTurn(TicTacToeBoard ticTacToeBoard)
    {
        var result = WinningMove(ticTacToeBoard, Symbol.O);

        if(result == null)
        {
            result = WinningMove(ticTacToeBoard, Symbol.X);
        }

        return result ?? TakeRandomTurn(ticTacToeBoard);
    }

    private (int row, int col)? WinningMove(TicTacToeBoard ticTacToeBoard, Symbol symbol)
    {
        foreach (var square in ticTacToeBoard.GetFreeSquares())
        {
            if (CheckWin(square, ticTacToeBoard, symbol))
            {
                Console.WriteLine(square.Id);
                return square.Id;
            }
        }
        return null;
    }

    private bool CheckWin(TicTacToeSquare ticTacToeSquare, TicTacToeBoard ticTacToeBoard, Symbol symbol)
    {
        ticTacToeSquare.AssignSymbol(symbol);
        var result = ticTacToeBoard.CheckWin(ticTacToeSquare.Id.row, ticTacToeSquare.Id.col,symbol);
        ticTacToeSquare.RemoveSymbol();
        Console.WriteLine(result);
        return result;
    }
}

