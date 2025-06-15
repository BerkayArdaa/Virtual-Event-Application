using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualEvent_SE410
{
    // Delegate definition for handling the event when a new Event object is added
    public delegate void EventAddedHandler(Event newEvent);

    // Class representing an Event entity with properties for all event-related data
    public class Event
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime RegistrationDeadline { get; set; }
        public int AvailableSlots { get; set; }
        public int Duration { get; set; }
    }
}
