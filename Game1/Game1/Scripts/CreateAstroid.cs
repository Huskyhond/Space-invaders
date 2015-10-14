using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Scripts
{
    class CreateAstroid : Instruction
    {
        public override InstructionResult Execute(float deltaTime)
        {
            return InstructionResult.DoneAndCreateAstroid;
        }

        public override Instruction Reset()
        {
            return new CreateAstroid();
        }
    }
}
