using System;

namespace Statistics.Core.Widgets
{
    public sealed class WidgetItem
    {
        public DateTime CreateDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Layout { get; set; }
        public string Code { get; set; }
        public IWidget Context { get; set; }
    }
}
