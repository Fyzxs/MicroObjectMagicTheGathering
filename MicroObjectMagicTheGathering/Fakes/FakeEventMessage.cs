namespace MicroObjectMagicTheGathering.Fakes {
    public class FakeEventMessage : IEventMessage
    {
        public class Builder
        {
            public FakeEventMessage Build()
            {
                return new FakeEventMessage { };
            }
        }

        private FakeEventMessage() { }
    }
}