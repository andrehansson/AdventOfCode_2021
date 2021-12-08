using AoCHelper;
using System;

namespace AdventOfCode_2021
{
    class Program
    {
        static void Main(string[] args)
        {
            SolverConfiguration solverConfiguration = new();
            solverConfiguration.ShowConstructorElapsedTime = true;
            solverConfiguration.ShowTotalElapsedTimePerDay = true;
            solverConfiguration.ElapsedTimeFormatSpecifier = "F2";

            //Solver.SolveLast(solverConfiguration);
            Solver.SolveAll(solverConfiguration);
            //Solver.Solve(solverConfiguration, 5);
        }
    }
}
