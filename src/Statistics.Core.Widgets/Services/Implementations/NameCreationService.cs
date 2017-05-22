using System;

namespace Statistics.Core.Widgets
{
    public sealed class NameCreationService : INameCreationService
    {
        public string CreateName(string prefix)
        {
            prefix = prefix ?? string.Empty;
            return $"{prefix}_{Guid.NewGuid():N}";
        }
    }
}
