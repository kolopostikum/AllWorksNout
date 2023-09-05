using System;
using System.Collections.Generic;

namespace Seminar
{
    public enum Commandos
    {
        move,
        insertChar,
        delete
    }

    public class CommandPreRealisation
    {
        private LinkedList<Command> StackCommand;
        private LinkedList<Command> StackUndo;

        public class Command
        {
            public Commandos command;
            public string data;

            public Command(Commandos command, string data)
            {
                this.command = command;
                this.data = data;
            }
        }

        public void Undo()
        {
            if (StackCommand.Count == 0)
                throw new Exception();
            switch (StackCommand.Last.Value.command)
            {
                case Commandos.delete:
                    StackUndo.AddLast(StackCommand.Last);
                    InsertChar(StackCommand.Last.Value.data[0]);
                    StackCommand.RemoveLast();
                    StackCommand.RemoveLast();
                    break;
                case Commandos.insertChar:
                    StackUndo.AddLast(StackCommand.Last);
                    DeleteChar();
                    StackCommand.RemoveLast();
                    StackCommand.RemoveLast();
                    break;
                case Commandos.move:
                    StackUndo.AddLast(StackCommand.Last);
                    var row = - int.Parse(StackCommand.Last.Value.data.Split()[0]);
                    var col = - int.Parse(StackCommand.Last.Value.data.Split()[1]);
                    MoveCursorTo(row, col);
                    StackCommand.RemoveLast();
                    StackCommand.RemoveLast();
                    break;
                default:
                    break;
            }
        }

        public void Redo()
        {
            if (StackUndo.Count == 0)
                throw new Exception();
            switch (StackUndo.Last.Value.command)
            {
                case Commandos.delete:
                    DeleteChar();
                    StackUndo.RemoveLast();
                    break;
                case Commandos.insertChar:
                    InsertChar(StackUndo.Last.Value.data[0]);
                    StackUndo.RemoveLast();
                    break;
                case Commandos.move:
                    var row = int.Parse(StackUndo.Last.Value.data.Split()[0]);
                    var col = int.Parse(StackUndo.Last.Value.data.Split()[1]);
                    MoveCursorTo(row, col);
                    StackUndo.RemoveLast();
                    break;
                default:
                    break;
            }

        }

        public void MoveCursorTo(int row, int col)
        {
            var data = row + " " + col;
            StackCommand.AddLast(new Command(Commandos.move, data));
        }

        public void InsertChar(char c)
        {
            StackCommand.AddLast(new Command(Commandos.insertChar, c.ToString()));
        }

        public void DeleteChar()
        {
            var data = 'c';
            StackCommand.AddLast(new Command(Commandos.delete, data.ToString()));
        }
    }
}
