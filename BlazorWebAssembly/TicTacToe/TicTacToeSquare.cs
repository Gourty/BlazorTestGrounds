using BlazorWebAssembly.TicTacToe.Enums;

namespace BlazorWebAssembly.TicTacToe;

public class TicTacToeSquare
{

    public void AssignSymbol(Symbol symbol)
    {
        if (IsValidMove())
            Symbol = symbol;
    }

    public void RemoveSymbol()
    {
        Symbol = null;
    }

    public bool IsValidMove() { return Symbol == null; }


    public Symbol? Symbol { get; private set; }
    public (int row, int col) Id { get; private set; }

    public TicTacToeSquare(int row, int col)
    {
        Id = (row, col);
    }
}
