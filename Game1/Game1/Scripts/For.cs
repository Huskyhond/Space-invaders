using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Scripts
{
    class For : Instruction
    {
        private int start, end, i;
        private Instruction body;

        public For(int start, int end, Instruction body)
        {
            this.start = start;
            this.end = end;
            this.body = body;
            this.i = start;
        }

        public override InstructionResult Execute(float deltaTime)
        {
            if (i <= end)
            {
                var execute = body.Execute(deltaTime);
                switch (execute)
                {
                    case InstructionResult.Done:
                        i++;
                        body = body.Reset();
                        return InstructionResult.Running;
                    case InstructionResult.DoneAndCreateAstroid:
                        i++;
                        body = body.Reset();
                        return InstructionResult.Running;
                }
                return execute;
            }
            else
            {
                return InstructionResult.Done;
            }
        }

        public override Instruction Reset()
        {
            return new For(start, end, body.Reset());
        }
    }
}
