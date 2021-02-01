using System.Collections.Generic;
using System.Linq;

namespace Invariants.Tests
{
    public static class Game
    {
        public static IEnumerable<IEvent> Handle(JoinGame command, GameState state)
        {
            if (command.PlayerId == state.CreatorId) yield break;

            yield return new GameStarted {GameId = command.GameId, PlayerId = command.PlayerId};
            yield return new RoundStarted {GameId = command.GameId, Round = 1};
        }

        public static IEnumerable<IEvent> Handle(JoinGame command, IEvent[] events)
        {
            var gameCreated = events.OfType<GameCreated>().Last();
            if (gameCreated.PlayerId == command.PlayerId) yield break;

            yield return new GameStarted {GameId = command.GameId, PlayerId = command.PlayerId};
            yield return new RoundStarted {GameId = command.GameId, Round = 1};
        }
    }
}