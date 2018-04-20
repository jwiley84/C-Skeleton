using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace skeleton {

	public class Activity {

        [Key]
		public int activityID {get; set;}
        public int userID { get; set; }
        public User user {get; set;}
        public DateTime sessionDateTime { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        
        public string title { get; set; }
        public string description { get; set; }

        public int duration { get; set; }
        public int durationAnnotation { get; set; }

        public List<AttendeesList> attendeesList { get; set; }
        public Activity()
        {
            attendeesList = new List<AttendeesList>();
        }
	}
}