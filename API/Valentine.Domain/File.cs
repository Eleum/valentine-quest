using System;

namespace Valentine.Domain
{
    public abstract class File
    {
        public Guid Id { get; set; }

        public Guid AreaId { get; set; }

        public Area Area { get; set; }
    }
}
