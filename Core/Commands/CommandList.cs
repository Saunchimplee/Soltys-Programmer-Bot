using System;
using System.Collections.Generic;

namespace Soltys.ProgrammerBot.Core.Commands
{
    public class CommandList : GameObject
    {
        LinkedList<Command> commands = new LinkedList<Command>();
        readonly TimeSpan timeBetweenExecution = TimeSpan.FromMilliseconds(750);
        TimeSpan accumulateTime;
        public LinkedList<Command> Commands { get { return commands; } }
        public CommandList(Game currentGame)
            : base(currentGame)
        {
        }

        public void AddCommands(LinkedList<Command> commands)
        {
            this.commands = commands;
        }
        public void Clear()
        {
            commands.Clear();
        }
        public void OnLogicUpdate(GameTime e)
        {
            accumulateTime = accumulateTime.Add(e.ElapsedGameTime);

            if (accumulateTime > timeBetweenExecution)
            {
                while (commands.Count != 0 && commands.First.Value.IsNOP())
                {
                    commands.RemoveFirst();
                }
                if (commands.Count != 0)
                {
                    Command command = commands.First.Value;
                    var node = commands.First;

                    if (command.Check())
                    {
                        command.Excecute();
                    }

                    accumulateTime = TimeSpan.Zero;
                    commands.Remove(node);


                }
                else
                {
                    accumulateTime = TimeSpan.Zero;
                    GameHandler.LogicUpdate -= OnLogicUpdate;
                    return;
                }
            }
        }
        public void Exectute()
        {
            GameHandler.LogicUpdate += OnLogicUpdate;
            accumulateTime = TimeSpan.Zero;
        }
    }
}
