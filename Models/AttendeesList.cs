using System.ComponentModel.DataAnnotations;

namespace skeleton {

	public class AttendeesList {

		[Key]
        public int attendeesListID { get; set; }
        public int activityID { get; set; }
        public Activity activity { get; set; }
        public int userID { get; set; }
        public User user { get; set; }
	}
}
