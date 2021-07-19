using System;

namespace CommandPattern.Interfaces
{
    public interface IGuidIdentity
    {
         Guid Id { get; set; }
         int? OracleId { get; set; }
    }
}