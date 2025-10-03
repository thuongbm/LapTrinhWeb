﻿namespace Lab1.Models
{
    public class Learner
    {
        public Learner() 
        {
            Enrollments = new HashSet<Enrollment>();
        }

        public int LearnerID { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public int MajorID { get; set; }
        public virtual Major? Major { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public DateTime EnrollmentDate { get; set; }

    }
}
