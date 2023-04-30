namespace Tic_Tac_Toe;

using ToolBar;

internal class GameController
{
    public GameController(int sizeOfMap)
    {
        _sizeOfMap = sizeOfMap;
        borderRight = sizeOfMap - 1;
    }

    private Field[] _fields = new Field[36];
    private readonly Field _player = new(11, 11, _radiusOfField);

    private readonly int _sizeOfMap;

    private const int _radiusOfField = 3;

    private byte _currentField;

    private bool _isTicToe;
    private bool _isTacToe;

    private readonly int borderLeft = Console.WindowLeft + 3;
    private readonly int borderRight;

    private readonly int borderTop = Console.WindowTop + 2;
    private readonly int borderBottom = Console.WindowHeight - 2;

    private Field[] DrawFields(int radius)
    {
        // start location
        int x = (borderLeft + radius) - 1;
        int y = borderTop + radius;

        Field[] fields = new Field[_sizeOfMap * _sizeOfMap];

        int currentFieldId = 0;

        for (int i = 0; i < _sizeOfMap; i++)
        {
            for (int j = 0; j < _sizeOfMap; j++)
            {
                Field currentField = new(x, y, radius);
                currentField.DrawField();

                fields[currentFieldId] = currentField;

                // next field
                x += radius * 2;
                currentFieldId++;
            }

            // reset x
            x = (borderLeft + radius) - 1;
            // next line 
            y += radius * 2;
        }

        return fields;
    }

    private void Update()
    {
        ConsoleColor fieldColor = ConsoleColor.White;

        while (true)
        {
            // if X
            if (_isTicToe)
                fieldColor = ConsoleColor.Red;

            // if O
            if (_isTacToe)
                fieldColor = ConsoleColor.Blue;

            _player.DrawField(fieldColor);

            ConsoleKeyInfo key = Console.ReadKey(true);

            _fields[_currentField].DrawField(ConsoleColor.White);

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (_player.X > borderLeft + _radiusOfField * 2)
                        _player.X -= _radiusOfField * 2;
                    break;

                case ConsoleKey.UpArrow:
                    if (_player.Y > borderTop + _radiusOfField * 2)
                        _player.Y -= _radiusOfField * 2;
                    break;

                case ConsoleKey.RightArrow:
                   if (_player.X < borderRight * _radiusOfField * 2)
                        _player.X += _radiusOfField * 2;
                    break;

                case ConsoleKey.DownArrow:
                    if (_player.Y < borderBottom - (_radiusOfField * 2))
                        _player.Y += _radiusOfField * 2;
                    break;
            }

            for (int i = 0; i < _fields.Length; i++)
            {
                // find the coordinates of the field with the same value as the player's 
                if (
                    _player.X == _fields[i].X
                    && _player.Y == _fields[i].Y
                    )
                {
                    _currentField = (byte)i;
                }
                //
            }

            // check who is win

            void Win(TicTacToe ticTacToe)
            {
                Console.Clear();

                Console.ForegroundColor = fieldColor;

                Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);

                Console.Write($"{ticTacToe} win!");

                Console.ForegroundColor = ConsoleColor.Gray;

                int item = MenuBar.Menu(new string[] { "Restart", "Exit" }, x: Console.WindowWidth / 2, y: Console.WindowHeight / 2 + 2);

                if (item == 1)
                    Start();
                if (item == 2)
                    return;
            }

            bool CheckWinnerPlayer(TicTacToe ticTacToe)
            {
                // vertical
                for (int i = 0, downField = _currentField, upField = _currentField;
                    i < _sizeOfMap; i++)
                {
                    // if field is marked, go to next
                    if (_fields[downField].TicTacToe == ticTacToe && _fields[upField].TicTacToe == ticTacToe)
                    {
                        // if up and down field are in the end of map, go to next
                        if (_fields[upField].Y < borderTop + _radiusOfField * 2 &&
                            _fields[downField].Y > borderBottom - (_radiusOfField * 2))
                        {
                            // win
                            Win(ticTacToe);
                            return true;
                        }
                    }
                    else break;
                    
                    // go to next field (up, down)
                    if (_fields[downField].Y < borderBottom - (_radiusOfField * 2))
                        downField += _sizeOfMap;

                    if (_fields[upField].Y > borderTop + _radiusOfField * 2)
                        upField -= _sizeOfMap;
                }

                // horizontal
                for (int i = 0, rightField = _currentField, leftField = _currentField;
                    i < _sizeOfMap; i++)
                {
                    // if right and left field are in the end of map, go to next
                    if (_fields[rightField].TicTacToe == ticTacToe && _fields[leftField].TicTacToe == ticTacToe)
                    {
                        // if right and left field are in the end of map, go to next
                        if (_fields[rightField].X > borderRight * _radiusOfField * 2 &&
                           _fields[leftField].X < borderLeft + _radiusOfField * 2)
                        {
                            // win
                            Win(ticTacToe);
                            return true;
                        }
                    }
                    else break;

                    // go to next field (right, left)
                    if (_fields[rightField].X < borderRight * _radiusOfField * 2)
                        rightField += 1;

                    if (_fields[leftField].X > borderLeft + _radiusOfField * 2)
                        leftField -= 1;
                }

                // left diagonal
                for (int i = 0, leftDiagonal = 0; i <= _sizeOfMap; i++)
                {
                    // if diagonal fields are marked, go to next
                    if (_fields[leftDiagonal].TicTacToe == ticTacToe)
                    {
                        if (i == _sizeOfMap)
                        {
                            Win(ticTacToe);
                            return true;
                        }
                    }
                    else break;

                    // go to the next field in diagonal
                    if (leftDiagonal < _fields.Length - 1)
                        leftDiagonal += _sizeOfMap + 1;
                }

                // right diagonal
                for (int i = 0, rightDiagonal = _sizeOfMap - 1; i <= _sizeOfMap; i++)
                {
                    if (_fields[rightDiagonal].TicTacToe == ticTacToe)
                    {
                        if (i == _sizeOfMap)
                        {
                            Win(ticTacToe);
                            return true;
                        }
                    }
                    else break;


                    if (rightDiagonal < _fields.Length - _sizeOfMap)
                        rightDiagonal += _sizeOfMap - 1;
                }

                return false;
            }

            // change player and mark current field
            if (key.Key != ConsoleKey.Enter)
                continue;

            else if (!_fields[_currentField].IsMarked)
            {
                bool isWin = false;

                if (_isTicToe)
                {
                    _fields[_currentField].MarkField(TicTacToe.Tic);

                    isWin = CheckWinnerPlayer(TicTacToe.Tic);

                    _isTacToe = true;
                    _isTicToe = false;
                }
                else
                {
                    _fields[_currentField].MarkField(TicTacToe.Tac);

                    isWin = CheckWinnerPlayer(TicTacToe.Tac);

                    _isTacToe = false;
                    _isTicToe = true;
                }

                if (isWin)
                    return;
            }
            //
        }
    }


    public void Start()
    {
        _fields = DrawFields(_radiusOfField);

        _isTicToe = true;
        _isTacToe = false;

        _currentField = 4;

        Update();
    }
}