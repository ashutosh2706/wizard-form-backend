namespace WizardFormBackend
{
    public class Constants
    {
        public enum StatusCode
        {
            Pending = 1,
            Approved = 2,
            Rejected = 3,
        }

        public enum PriorityCode
        {
            High = 1,
            Normal = 2,
            Low = 3,
        }

        public enum Roles
        {
            User = 1,
            Admin = 2,
        }
    }
}
