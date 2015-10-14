using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Scripts
{
    class While: Instruction
    {
        private Instruction instruction;

        public While(Instruction instruction)
        {
            this.instruction = instruction;
        }

        public override InstructionResult Execute(float deltaTime)
        {
            switch (instruction.Execute(deltaTime))
            {
                case InstructionResult.Done:
                    this.instruction = this.instruction.Reset();
                    break;
                case InstructionResult.RunningAndCreateAstroid:
                    return InstructionResult.RunningAndCreateAstroid;
                case InstructionResult.DoneAndCreateAstroid:
                    this.instruction = this.instruction.Reset();
                    break;
            }
            return InstructionResult.Running;
        }

        public override Instruction Reset()
        {
            return new While(instruction.Reset());
        }

    }
}
