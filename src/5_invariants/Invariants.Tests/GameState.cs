namespace Invariants.Tests
{
    public class GameState
    {
        public string CreatorId { get; set; }

        public GameState When(IEvent @event) => this;

        public GameState When(GameCreated @event)
        {
            CreatorId = @event.PlayerId;
            return this;
        }
    }
}