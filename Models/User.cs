using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace skeleton {

	public class User {

		 [Key]
		public int userID {get; set;}
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public DateTime created_at { get; set; }
        
        public DateTime updated_at { get; set; }
        
        public List<AttendeesList> attendeesList { get; set; }
        public User()
        {
            attendeesList = new List<AttendeesList>();
        }

        
	}
}