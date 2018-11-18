using System;

namespace InitiativeRoller.Helpers
{
    public interface IRequireViewIdentification
    {
        Guid ViewId { get; }
    }
}
