namespace Day6;

public static class ProblemSolver
{
    public static ulong Solve(IReadOnlyList<Problem> problems)
    {
        ulong sum = 0;

        foreach (Problem problem in problems)
        {
            ulong result = Solve(problem);
            Console.WriteLine($"{problem} = {result}");
            sum += result;
        }

        return sum;
    }

    private static ulong Solve(Problem problem)
    {
        if (problem.Operation is Operation.Add)
            // Aggregate implementation needed because Sum does not have an overload for ulong.
            return problem
                .Numbers
                .Aggregate((agg, num) => agg + num);

        return problem
            .Numbers
            .Aggregate((agg, num) => agg * num);
    }
}