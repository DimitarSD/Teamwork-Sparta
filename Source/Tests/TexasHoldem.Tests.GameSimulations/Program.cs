namespace TexasHoldem.Tests.GameSimulations
{
    using System;

    using TexasHoldem.Tests.GameSimulations.GameSimulators;

    public static class Program
    {
        public static void Main()
        {
            //SimulateGames(new SmartVsAlwaysCallPlayerSimulation());
            //SimulateGames(new SmartVsDummyPlayerSimulator());
            //SimulateGames(new SmartVsSmartPlayerSimulator());
            //SimulateGames(new AlwaysCallPlayersGameSimulation());
            //SimulateGames(new SpartaVsDummySimlation());
            //Console.WriteLine("Sparta vs DummyPlayer - FIGHT!");
            //SimulateGames(new SpartaVsDummySimlation());
            //Console.WriteLine("Sparta vs AlwaysCallPlayer - FIGHT!");
            //SimulateGames(new SpartaVsAlwaysCallSimulation());
            //Console.WriteLine("Sparta vs SmartPlayer - FIGHT!");
            //SimulateGames(new SpartaVsSmartSimulation());

            Console.WriteLine("Sparta vs TEST - FIGHT!");
            SimulateGames(new SpartaVTESTBBOTSimulation());
            //Console.WriteLine("Sparta vs Stone - FIGHT!");
            //SimulateGames(new SpartaVSpartaSimulation());
        }


        private static void SimulateGames(IGameSimulator gameSimulator)
        {
            Console.WriteLine($"Running {gameSimulator.GetType().Name}...");

            var simulationResult = gameSimulator.Simulate(10000);

            Console.WriteLine(simulationResult.SimulationDuration);
            Console.WriteLine($"Total games: {simulationResult.FirstPlayerWins:0,0} - {simulationResult.SecondPlayerWins:0,0}");
            Console.WriteLine($"Hands played: {simulationResult.HandsPlayed:0,0}");
            Console.WriteLine(new string('=', 75));
        }
    }
}
