using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game1.Scripts
{
    public enum InstructionResult {
        Running,
        Done,
        RunningAndCreateAstroid,
        DoneAndCreateAstroid
    }

    public abstract class Instruction
    {
        public abstract InstructionResult Execute(float deltaTime);
        public abstract Instruction Reset();
    }
}
